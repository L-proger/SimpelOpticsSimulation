using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class Geometry{
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> indices = new List<int>();
    public List<Vector3> normals = new List<Vector3>();

    public void StoreToMesh(Mesh mesh){
        mesh.subMeshCount = 1;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        mesh.RecalculateBounds();
    }
}

public abstract class GeometryProcessor {
    public abstract Geometry Execute (Geometry geometry);
}

public class GenCylinder : GeometryProcessor
{
    public float Radius = 1.0f;
    public float Height = 1.0f;
    public int Tesselation = 16;

    public static Geometry Execute(Geometry geometry, float radius, float height, int tesselation = 16)
    {
        var processor = new GenCylinder();
        processor.Radius = radius;
        processor.Height = height;
        processor.Tesselation = tesselation;
        return processor.Execute(geometry);
    }

    public override Geometry Execute(Geometry geometry)
    {
        if(geometry == null)
        {
            throw new System.Exception("Geometry is null");
        }

        var vertexOffset = geometry.vertices.Count;

        //vertices
        for (int i = 0; i < Tesselation; ++i)
        {
            float angle = 360.0f / Tesselation * i;

            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * Radius;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * Radius;

            float halfH = Height * 0.5f;

            geometry.vertices.Add(new Vector3(x, halfH, z));
            geometry.vertices.Add(new Vector3(x, -halfH, z));

            var normal = new Vector3(x, 0, z).normalized;
            geometry.normals.Add(normal);
            geometry.normals.Add(normal);
        }

        //indices
        for (int i = 0; i < Tesselation; ++i)
        {
            int t0 = i * 2 + 0;
            int b0 = i * 2 + 1;

            int t1 = ((i + 1) % Tesselation) * 2 + 0;
            int b1 = ((i + 1) % Tesselation) * 2 + 1;

            geometry.indices.Add(t0 + vertexOffset);
            geometry.indices.Add(b0 + vertexOffset);
            geometry.indices.Add(t1 + vertexOffset);

            geometry.indices.Add(t1 + vertexOffset);
            geometry.indices.Add(b0 + vertexOffset);
            geometry.indices.Add(b1 + vertexOffset);
        }

        return geometry;
    }
}

public static class SphereUtils
{
    public static float SphereCenterOffset(float sphereRadius, float visibleRadius)
    {
        return Mathf.Sqrt(sphereRadius * sphereRadius - visibleRadius * visibleRadius);
    }
}

public class GenSphericalCap : GeometryProcessor
{
    public float Radius = 1.0f;
    public float CapRadius = 1.0f;
    public int RTesselation = 16;
    public int CTesselation = 16;

    public static Geometry Execute(Geometry geometry, float radius, float capRadius, int tessR = 16, int tessC = 16)
    {
        GenSphericalCap processor = new GenSphericalCap();
        processor.Radius = radius;
        processor.CapRadius = capRadius;
        processor.RTesselation = tessR;
        processor.CTesselation = tessC;

        return processor.Execute(geometry);
    }

    public override Geometry Execute(Geometry geometry)
    {
        int vertexOffset = geometry.vertices.Count;
        for (int i = 0; i < CTesselation; ++i)
        {
            int pointsCount = i == 0 ? 1 : RTesselation;

            //generate circle
            for (int j = 0; j < pointsCount; ++j)
            {
                float angle = 360.0f / RTesselation * j;
                float rCircle = Radius / (CTesselation - 1) * i;
                float x = Mathf.Sin(angle * Mathf.Deg2Rad) * rCircle;
                float z = Mathf.Cos(angle * Mathf.Deg2Rad) * rCircle;


                float y = (SphereUtils.SphereCenterOffset(CapRadius, rCircle) - SphereUtils.SphereCenterOffset(CapRadius, Radius)) * Mathf.Sign(CapRadius);



                geometry.vertices.Add(new Vector3(x, y, z));

                geometry.normals.Add(Vector3.up);
            }
        }

        //indices
        for (int i = 0; i < CTesselation - 1; ++i)
        {
            if (i == 0)
            {
                for (int j = 0; j < RTesselation; ++j)
                {
                    geometry.indices.Add(0 + vertexOffset);
                    geometry.indices.Add(j + 1 + vertexOffset);
                    geometry.indices.Add((j + 1) % RTesselation + 1 + vertexOffset);
                }
                vertexOffset += 1;
            }
            else
            {
                for (int j = 0; j < RTesselation; ++j)
                {
                    int a0 = RTesselation * (i - 1) + j;
                    int a1 = RTesselation * (i - 1) + (j + 1) % RTesselation;

                    int b0 = RTesselation * (i - 0) + j;
                    int b1 = RTesselation * (i - 0) + (j + 1) % RTesselation;


                    geometry.indices.Add(a0 + vertexOffset);
                    geometry.indices.Add(b0 + vertexOffset);
                    geometry.indices.Add(a1 + vertexOffset);

                    geometry.indices.Add(b0 + vertexOffset);
                    geometry.indices.Add(b1 + vertexOffset);
                    geometry.indices.Add(a1 + vertexOffset);
                }

            }
        }

        return geometry;
    }
}


public class AttachGeometry : GeometryProcessor
{
    public List<Geometry> Others = new List<Geometry>();

    public static Geometry Execute(Geometry geometry, IEnumerable<Geometry> others)
    {
        var processor = new AttachGeometry();
        processor.Others.AddRange(others);
        return processor.Execute(geometry);
    }

    public override Geometry Execute(Geometry geometry)
    {
        foreach(var other in Others)
        {
            var indexOffset = geometry.vertices.Count;
            geometry.vertices.AddRange(other.vertices);
            geometry.normals.AddRange(other.normals);
            geometry.indices.AddRange(other.indices.Select(v => v + indexOffset));
        }

        return geometry;
    }
}

public class VectorOffset : GeometryProcessor
{
    public Vector3 Offset = Vector3.zero;

    public static Geometry Execute(Geometry geometry, Vector3 offset) {
        var processor = new VectorOffset();
        processor.Offset = offset;
        return processor.Execute(geometry);
    }

    public override Geometry Execute(Geometry geometry){
        for(int i = 0; i < geometry.vertices.Count; ++i) {
            geometry.vertices[i] += Offset;
        }
        return geometry;
    }
}

public class FlipFaces : GeometryProcessor
{

    public static Geometry ExecuteStatic(Geometry geometry)
    {
        var processor = new FlipFaces();
        return processor.Execute(geometry);
    }

    public override Geometry Execute(Geometry geometry)
    {
        for (int i = 0; i < geometry.indices.Count; i+=3)
        {
            var tmp = geometry.indices[i];
            geometry.indices[i] = geometry.indices[i + 1];
            geometry.indices[i + 1] = tmp;
        }

        for (int i = 0; i < geometry.normals.Count; ++i)
        {
            geometry.normals[i] = -geometry.normals[i];
        }
        return geometry;
    }
}


public class ThinLense : MonoBehaviour {

    public float R1;
    public float R2;

    public float LensIOR = 1.5f;

    public float EnvironmentIOR = 1.0f;

    public float RLense;

    public float Thickness = 0.1f;

    [Range(3, 128)]
    public int tessR = 32;
    [Range(2, 128)]
    public int tessC = 32;

    private void OnValidate()
    {
       

        if(RLense < 0)
        {
            RLense = 0;
        }
       

        /*if(R1 < RLense)
        {
            R1 = RLense;
        }*/

        if(Thickness < 0.0f)
        {
            Thickness = 0.0f;
        }

        UpdateMesh();

    }

    float FocalLength()
    {
        float rcpF = (LensIOR - EnvironmentIOR) * ((1.0f / R1) - (1.0f / R2));
        return 1.0f / rcpF;
    }

   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMesh()
    {
        var mr = GetComponent<MeshRenderer>();
        if(mr == null)
        {
            mr = gameObject.AddComponent<MeshRenderer>();
        }

        var mf = GetComponent<MeshFilter>();
        if(mf == null)
        {
            mf = gameObject.AddComponent<MeshFilter>();
        }

        var m = mf.sharedMesh;
        if(m == null)
        {
            m = new Mesh();
            mf.sharedMesh = m;
        }


        if(Mathf.Abs(R1) < RLense)
        {
            return;
        }


        //generate cylinder
        var cyl = GenCylinder.Execute(new Geometry(), RLense, Thickness, tessR);
        var cap1 = GenSphericalCap.Execute(new Geometry(), RLense, R1, tessR, tessC);
        cap1 = VectorOffset.Execute(cap1, new Vector3(0, Thickness * 0.5f, 0));

        var cap2 = GenSphericalCap.Execute(new Geometry(), RLense, R2, tessR, tessC);
        cap2 = VectorOffset.Execute(cap2, new Vector3(0, -Thickness * 0.5f, 0));
        cap2 = FlipFaces.ExecuteStatic(cap2);

        AttachGeometry.Execute(new Geometry(), new []{cyl, cap1, cap2 }).StoreToMesh(m);
    }

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;


        Handles.DrawWireDisc(Vector3.zero, Vector3.up, RLense);

        Gizmos.DrawWireSphere(Vector3.up * - SphereUtils.SphereCenterOffset(R1, RLense), R1);

        Handles.matrix = Matrix4x4.identity;
    }
}
