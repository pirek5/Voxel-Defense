using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour {

    // set in editor
    [SerializeField] private Vector2Int[] resolutions;


	void Awake () {
        var widht = Display.main.systemWidth;
        var height = Display.main.systemHeight;

        foreach(Vector2Int resolution in resolutions)
        {
            if(resolution.x <= widht && resolution.y <= height)
            {
                Screen.SetResolution(resolution.x, resolution.y, true);
                return;
            }
        }

        Debug.LogError("Screen resolution not supported");
    }

}
