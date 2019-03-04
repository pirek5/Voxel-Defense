using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothColorChange : MonoBehaviour {


    //two color switches
    public void SwitchColors(Color startColor, Color midColor, Color endColor, float firstSwitchTime, float midColorTime, float secondSwitchTime)
    {
        StopAllCoroutines();
        StartCoroutine(SwitchColorsCoroutine(startColor, midColor, endColor, firstSwitchTime, midColorTime, secondSwitchTime));
    }

    //one color switch
    public void SwitchColors(Color startColor, Color endColor, float switchTime)
    {
        StopAllCoroutines();
        StartCoroutine(SwitchColorsCoroutine(startColor, endColor, endColor, switchTime, 0f, 0f));
    }

    private IEnumerator SwitchColorsCoroutine(Color startColor, Color midColor ,Color endColor, float firstSwitchTime, float midColorTime, float secondSwitchTime)
    {
        Material material = GetComponent<MeshRenderer>().material;

        if(material == null) { yield break; }

        float t = 0f;
        while (t < firstSwitchTime)
        {
            material.color = Vector4.Lerp(startColor, midColor, t/firstSwitchTime);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(midColorTime);
        material.color = midColor; //used to color switch with 0f switches times

        t = 0f;
        while(t < secondSwitchTime)
        {
            material.color = Vector4.Lerp(midColor, endColor, t/secondSwitchTime);
            t += Time.deltaTime;
            yield return null;
        }

        material.color = endColor; //used to color switch with 0f switches times

    }

}
