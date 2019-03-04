using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothColorChange : MonoBehaviour {


    //two color switches
    public void SwitchColors(Color startColor, Color midColor, Color endColor, float switchTime)
    {
        StopAllCoroutines();
        StartCoroutine(SwitchColorsCoroutine(startColor, midColor, endColor, switchTime));
    }

    //one color switch
    public void SwitchColors(Color startColor, Color endColor, float switchTime)
    {
        StopAllCoroutines();
        StartCoroutine(SwitchColorsCoroutine(startColor, endColor, endColor, switchTime));
    }

    private IEnumerator SwitchColorsCoroutine(Color startColor, Color midColor ,Color endColor, float switchTime)
    {
        Material material = GetComponent<MeshRenderer>().material;

        if(material == null) { yield break; }

        float t = 0f;
        while (t < switchTime)
        {
            material.color = Vector4.Lerp(startColor, midColor, t/switchTime);
            t += Time.deltaTime;
            yield return null;
        }

        yield return null;
        material.color = midColor; //used to color switch with 0f switches times

        t = 0f;
        while(t < switchTime)
        {
            material.color = Vector4.Lerp(midColor, endColor, t/switchTime);
            t += Time.deltaTime;
            yield return null;
        }

        material.color = endColor; //used to color switch with 0f switches times

    }

}
