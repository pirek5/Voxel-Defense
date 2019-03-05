 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelectedPathCube : HighlightSelected {

    //config
    [SerializeField] Color standardHighlight = Color.yellow, blockedHighlight = Color.red;

    //cached
    private PathCube pathCube;


    protected override void Awake()
    {
        base.Awake();
        pathCube = GetComponent<PathCube>();
    }

    protected override void Update()
    {
        base.Update();
        if (pathCube.occupupiedBy)
        {
            highlightedColor = blockedHighlight;
        }
        else
        {
            highlightedColor = standardHighlight;
        }
    }
}
