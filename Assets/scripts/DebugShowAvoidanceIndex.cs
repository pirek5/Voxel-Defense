using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShowAvoidanceIndex : MonoBehaviour {

    Material material;
    Color defaultColor;
    PathCube pathCube;

	// Use this for initialization
	void Start () {
        material = GetComponentInChildren<MeshRenderer>().material;
        defaultColor = material.color;
        pathCube = GetComponent<PathCube>();
	}
	
	// Update is called once per frame
	void Update () {
        float factor = pathCube.GetAvoidanceFactor() / 20f;
        material.color = Vector4.Lerp(defaultColor, Color.black, factor);
	}
}
