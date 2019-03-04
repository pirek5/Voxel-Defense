using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    [SerializeField] protected float solidAlpha = 1f;
    [SerializeField] protected float clearAlpha = 0f;
    [SerializeField] private float fadeOnDuration = 2f;
    public float FadeOnDuration { get { return fadeOnDuration; } }
    [SerializeField] private float fadeOffDuration = 2f;
    public float FadeOffDuration {  get { return fadeOffDuration; } }


    [SerializeField] private MaskableGraphic[] graphicsToFade;

    //private void Start()
    //{
    //    FadeOff();
    //}

    protected void SetAlpha(float alpha)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if(graphic != null)
            {
                graphic.canvasRenderer.SetAlpha(alpha);
            }
        }
    }

    private void Fade(float targetAlpha, float duration)
    {
        foreach (MaskableGraphic graphic in graphicsToFade)
        {
            if(graphic != null)
            {
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }
    }

    public void FadeOff()
    {
        SetAlpha(solidAlpha);
        Fade(clearAlpha, fadeOffDuration);
    }

    public void FadeOn()
    {
        SetAlpha(clearAlpha);
        Fade(solidAlpha, fadeOnDuration);
    }

    public static void PlayTransition(TransitionFader transitionPrefab)
    {
        if(transitionPrefab != null)
        {
            TransitionFader instance = Instantiate(transitionPrefab);
            instance.Play();
        }
    }
}
