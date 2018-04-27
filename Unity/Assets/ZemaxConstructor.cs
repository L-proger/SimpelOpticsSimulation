using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZemaxConstructor : MonoBehaviour {

    public enum SurfaceType
    {
        Standard
    }

    [Serializable]
    public class Surface
    {
        public SurfaceType Type;
        public float Radius;
        public float Thickness;
        public float SemiDiameter;
    }

    public Surface[] surfaces;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
