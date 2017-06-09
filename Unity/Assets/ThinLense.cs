using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        if(RLense > R1)
        {
            R1 = RLense;
        }

        if(R1 < RLense)
        {
            R1 = RLense;
        }

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

    float SphereCenterOffset(float sphereRadius, float visibleRadius)
    {
        return Mathf.Sqrt(sphereRadius * sphereRadius - visibleRadius * visibleRadius);
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


        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        //generate cylinder

        //vertices
        for(int i = 0; i < tessR; ++i)
        {
            float angle = 360.0f / tessR * i;

            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * RLense;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * RLense;

            float halfH = Thickness * 0.5f;

            vertices.Add(new Vector3(x, halfH, z));
            vertices.Add(new Vector3(x, -halfH, z));

            var normal = new Vector3(x, 0, z).normalized;
            normals.Add(normal);
            normals.Add(normal);
        }

        //indices
        for (int i = 0; i < tessR; ++i)
        {
            int t0 = i * 2 + 0;
            int b0 = i * 2 + 1;

            int t1 = ((i + 1) % tessR) * 2 + 0;
            int b1 = ((i + 1) % tessR) * 2 + 1;

            indices.Add(t0);
            indices.Add(b0);
            indices.Add(t1);

            indices.Add(t1);
            indices.Add(b0);
            indices.Add(b1);
        }

        //generate R1 mesh
        //circles
        int vertexOffset = vertices.Count;
        for(int i = 0; i < tessC; ++i)
        {
            int pointsCount = i == 0 ? 1 : tessR;

            //generate circle
            for(int j = 0; j < pointsCount; ++j)
            {
                float angle = 360.0f / tessR * j;
                float rCircle = RLense / (tessC - 1) * i;
                float x = Mathf.Sin(angle * Mathf.Deg2Rad) * rCircle;
                float z = Mathf.Cos(angle * Mathf.Deg2Rad) * rCircle;
         

                float y = Thickness * 0.5f + SphereCenterOffset(R1, rCircle) - SphereCenterOffset(R1, RLense);

               /* Vector3 dir = new Vector3(x, y, z) - sphereCenter;

                var normal = dir.normalized;
                var point = normal * R1;*/

                vertices.Add(new Vector3(x, y, z));

                normals.Add(Vector3.up);
            }
        }

        //indices
        for(int i = 0; i < tessC - 1; ++i)
        {
            if(i == 0)
            {
                for(int j = 0; j < tessR; ++j)
                {
                    indices.Add(0 + vertexOffset);
                    indices.Add(j + 1 + vertexOffset);
                    indices.Add((j + 1) % tessR + 1 + vertexOffset);
                }
                vertexOffset += 1;
            }
            else
            {
                for (int j = 0; j < tessR; ++j)
                {
                    int a0 = tessR * (i - 1) + j;
                    int a1 = tessR * (i - 1) + (j + 1) % tessR;

                    int b0 = tessR * (i - 0) + j;
                    int b1 = tessR * (i - 0) + (j + 1) % tessR;


                    indices.Add(a0 + vertexOffset);
                    indices.Add(b0 + vertexOffset);
                    indices.Add(a1 + vertexOffset);

                    indices.Add(b0 + vertexOffset);
                    indices.Add(b1 + vertexOffset);
                    indices.Add(a1 + vertexOffset);
                }
                   
            }
        }



        m.subMeshCount = 1;
        m.Clear();
        m.vertices = vertices.ToArray();
        m.normals = normals.ToArray();
        m.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
     
        m.RecalculateBounds();


        

    }


    

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;


        Handles.DrawWireDisc(Vector3.zero, Vector3.up, RLense);

        Gizmos.DrawWireSphere(Vector3.up * -SphereCenterOffset(R1, RLense), R1);

        Handles.matrix = Matrix4x4.identity;
    }
}
