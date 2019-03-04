using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTeleporter : MonoBehaviour
{

    [SerializeField] private GameObject teleporter;
    [SerializeField] private float teleportEffectInSeconds;
    private MeshRenderer[] meshRenderers;
    private Color[] defaultAlbedoColors;
    private Color[] defaultEmissionColors;

    [SerializeField] private Color albedoColor;
    [SerializeField] private Color emissionColor;

    public void TeleporterInit()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        defaultAlbedoColors = new Color[meshRenderers.Length];
        defaultEmissionColors = new Color[meshRenderers.Length];
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        teleporter.SetActive(true);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            defaultAlbedoColors[i] = meshRenderers[i].material.color;
            defaultEmissionColors[i] = meshRenderers[i].material.GetColor("_EmissionColor");
            meshRenderers[i].material.color = albedoColor;
            meshRenderers[i].material.SetColor("_EmissionColor", emissionColor);
        }

        float t1 = Time.time; // first time measurement
        float t2 = Time.time;

        while (t2 - t1 < teleportEffectInSeconds)
        {
            t2 = Time.time; // second time measurement
            yield return null;
        }

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = defaultAlbedoColors[i];
            meshRenderers[i].material.SetColor("_EmissionColor", defaultEmissionColors[i]);
        }

        teleporter.SetActive(false);
    }
}




