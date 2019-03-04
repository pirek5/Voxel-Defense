using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelected : MonoBehaviour {
    //config
    [SerializeField] protected Color highlightedColor;
    [Range(0f,1f)][SerializeField]  private float flashingSpeed = 0.1f;

    //cached
    private Color defaultColor;
    private MeshRenderer meshRenderer;

    protected virtual void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
    }

    protected virtual void Update() {
        if (UserController.highlightedObject == this.name)
        {
            StartCoroutine(FlashingObject());
        }
        else
        {
            StopAllCoroutines();
            meshRenderer.material.color = defaultColor;
        }
    }

    protected virtual IEnumerator FlashingObject()
    {
        Color currentColor;
        float t = 1;
        float derivative = flashingSpeed;
        while (true)
        {
            currentColor = Vector4.Lerp(defaultColor, highlightedColor, t);
            meshRenderer.material.color = currentColor;

            t = t + derivative;
            if(t > 1)
            {
                t = 1;
                derivative = -derivative;
            }
            else if(t < 0)
            {
                t = 0;
                derivative = -derivative;
            }
            yield return null;
        }
    }
}
