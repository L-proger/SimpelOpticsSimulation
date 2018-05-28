using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LSystem : MonoBehaviour {
    public string Premise = "FFFA";

    public string[] Rules = new string[] { "A=!\"[B]////[B]////B", "B=&FFFA" };

    public string ContextIgnore = "F+-";

    public float StepSize = 0.1f;
    public float StepSizeScale = 0.9f;
    public float Angle = 28.0f;
    public float AngleScale = 0.7f;

    public uint Generations = 7;

    private List<List<Vector3>> _branches = new List<List<Vector3>>();



    // Use this for initialization
    void Start () {
		
	}

    private void OnValidate()
    {
       // RebuildTree();
    }

    public void RebuildTree(){

       /* MyParser parser = new MyParser();
        parser.Setup();

        MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(@"F(x)=F(x/2)"));
        StreamReader sr = new StreamReader(ms);

        parser.Parse(sr);*/


        Debug.Log("Rebuild tree");

        string result = Premise;
        for(uint g = 0; g < Generations; ++g)
        {
            string tmp = "";

            for(int j = 0; j < result.Length; ++j)
            {
                string replacement = result[j].ToString();
                foreach (var rule in Rules)
                {
                    if(result[j] == rule[0])
                    {
                        replacement = rule.Substring(2);
                        break;
                    }
                }
                tmp += replacement;
            }

            result = tmp;
        }

        Debug.Log("Final result: " + result);
        RunTurtle(result);
    }

    struct TurtleState
    {
        public Vector3 position;
        public Matrix4x4 mat;
        public List<Vector3> points;
    }

    void RunTurtle(string commandLine)
    {
        Stack<TurtleState> states = new Stack<TurtleState>();


        TurtleState state = new TurtleState();
        state.mat = Matrix4x4.identity;
        state.position = Vector3.zero;

        _branches.Clear();
        state.points = new List<Vector3>();
        state.points.Add(state.position);

        for(int i = 0; i < commandLine.Length; ++i)
        {
            char cmd = commandLine[i];

            if(cmd == 'F')
            {
                state.position = state.position + state.mat.MultiplyVector(Vector3.up);
                state.points.Add(state.position);
            }else if(cmd == '+')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(0, 0, -Angle)) ;
            }
            else if (cmd == '-')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(0, 0, Angle));
            }
            else if (cmd == '[')
            {
                states.Push(state);
                state.points = new List<Vector3>();
                state.points.Add(state.position);
            }
            else if (cmd == ']')
            {
                state = states.Pop();
                _branches.Add(state.points);
            }
            else if (cmd == '/')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(0, -Angle, 0));
            }
            else if (cmd == '\\')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(0, Angle, 0));
            }
            else if (cmd == '&')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(Angle, 0, 0));
            }
            else if (cmd == '^')
            {
                state.mat = state.mat * Matrix4x4.Rotate(Quaternion.Euler(-Angle, 0, 0));
            }
        }
        _branches.Add(state.points);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.matrix = transform.localToWorldMatrix;

        foreach(var branch in _branches)
        {
            if(branch.Count > 1)
            {
                for (int i = 0; i < branch.Count - 1; ++i)
                {
                    Gizmos.DrawLine(branch[i], branch[i + 1]);
                }
            }
            
        }

            
        
       
    }


    // Update is called once per frame
    void Update () {
		
	}
}
