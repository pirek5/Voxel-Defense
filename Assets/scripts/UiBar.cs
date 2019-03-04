using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBar : MonoBehaviour {

    //set in editor
    [SerializeField] private Transform barFill;

    [Range(0.01f, 0.99f)] public float value; // percentage
	
	void Update ()
    {
        if(value < 0)
        {
            value = 0;
        }
        barFill.localScale = new Vector3(value, barFill.localScale.y, barFill.localScale.z);
	}
}
