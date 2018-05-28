using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polynom : MonoBehaviour {

    public float[] Coeffs = new float[] { 0, 0, 0.01f, 0, 0.01f };

    public PlotSampler Sampler;
    public TraceRay PlotTraceRay;

	void Start () {
		
	}
	
	void Update () {
		
	}

    float Sagitta(float r) {
        float y = 0;
        int i = 0;
        for (i = 4; i >= (int) 2; i--) {
            
            y = y * r + Coeffs[i];
          
        }
        y *= Mathf.Pow(r, i + 1);
        return y;
    }

    float Derivative(float r){
        float y = 0;
        int i = 0;
        for (i = 4; i >= 2; i--) {
            y = y * r + i * Coeffs[i];
        }
        y *= Mathf.Pow(r, i);
        return y;
    }

    public float Fx(float x){
        return Mathf.Pow(x, 4) * Coeffs[4] + Mathf.Pow(x, 2) * Coeffs[2];
    }

    Vector3 Normal(Vector3 point){
        float r = Mathf.Sqrt(point.x * point.x + point.z * point.z);
        if(r == 0) {
            return Vector3.up;
        } else {
            var d = Derivative(r);
            return new Vector3(point.x * d / r, -1, point.z * d / r).normalized * -1.0f;
        }
    }

    float RayPlaneDistance(Ray plane, Ray ray)
    {
        return (Vector3.Dot(plane.origin, plane.direction) - Vector3.Dot(plane.direction, ray.origin)) / (Vector3.Dot(ray.direction, plane.direction));
    }

    bool Intersect(Ray ray, ref Vector3 point)
    {
        Ray p = new Ray();

        // initial intersection with z=0 plane
        {
            float s = ray.direction.y;

            if (s == 0)
                return false;

            float a = -ray.origin.y / s;

            if (a < 0)
                return false;

            p.origin = ray.origin + ray.direction * a;
        }

        uint n = 32;      // avoid infinite loop

        while (n-- != 0) {
            float new_sag = Sagitta(new Vector2(p.origin.x, p.origin.z).magnitude);
            float old_sag = p.origin.y;

            // project previous intersection point on curve
            p.origin = new Vector3(p.origin.x, new_sag, p.origin.z);


            // stop if close enough
            if (Mathf.Abs(old_sag - new_sag) < 1e-10)
                break;

            // get curve tangeante plane at intersection point
            p.direction = Normal(p.origin);

            // intersect again with new tangeante plane
            float a = RayPlaneDistance(p, ray);

            if (a < 0)
                return false;

            p.origin = ray.origin + ray.direction * a;
        }

        point = p.origin;

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Vector3 last = Vector3.zero;
        for(int i = 0; i <= 128; ++i) {
            float x = (i / 128.0f - 0.5f) * 2.0f * 4.0f;
            float val = Fx(x);

            Vector3 current = new Vector3(x, val, 0);

            if( i != 0) {
                Gizmos.DrawLine(last, current);
            }
            last = current;
        }

        if(Sampler != null) {
            float sagitta = Sagitta(Sampler.transform.position.x);
            float derivative = Derivative(Sampler.transform.position.x);
            Gizmos.DrawWireSphere(new Vector3(Sampler.transform.position.x, sagitta, 0), 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(Sampler.transform.position.x, sagitta, 0), new Vector3(Sampler.transform.position.x, sagitta, 0) + new Vector3(1, derivative, 0) * 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(Sampler.transform.position.x, sagitta, 0), new Vector3(Sampler.transform.position.x, sagitta, 0) + Normal(new Vector3(Sampler.transform.position.x, sagitta, 0)));
            //float sagitta = Sagitta(Sampler.transform.position.x);
        }


        if(PlotTraceRay != null) {
            Vector3 intersection = Vector3.zero;
            if(Intersect(new Ray(PlotTraceRay.transform.position, PlotTraceRay.transform.forward), ref intersection)) {
                Gizmos.DrawWireCube(intersection, Vector3.one * 0.1f);
            }
        }

    }
}
