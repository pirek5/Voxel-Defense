﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVisual : MonoBehaviour {

    //set in editor
    [SerializeField] private UiBar teleportBar;
    [SerializeField] private UiBar blockMoverBar;
    [SerializeField] private GameObject teleportFill, blockMoverFill;

    //config
    #pragma warning disable 0649
    [SerializeField] private Color teleportReloadingColor, teleportLoadedColor, blockMoverReloadingColor, blockMoverLoadedColor, notReadyColor, readyColor;
    #pragma warning restore 0649
    [SerializeField] private float colorFlashTime;

    //cached
    private UserController userController;
    private float teleportLoadingTime, blockMoverLoadingTime, blockGoingUpTime;

    void Start ()
    {
        userController = GetComponent<UserController>();
        LoadConfigData();
    }

    private void LoadConfigData()
    {
        if (GameManager.Instance != null)
        {
            teleportLoadingTime = GameManager.Instance.towerTeleportReloadTime;
            blockMoverLoadingTime = GameManager.Instance.blockMoverReloadTime;
            blockGoingUpTime = GameManager.Instance.pathCubeGoingUpTime;
        }
    }

    public void ReloadTeleport()
    {
        StartCoroutine(ReloadTeleportCoroutine());
    }
    
    private IEnumerator ReloadTeleportCoroutine()
    {
        userController.isTeleportReady = false;
        float t = 0f;
        while (t < teleportLoadingTime)
        {
            teleportBar.value = (t) / teleportLoadingTime;
            t += Time.deltaTime;
            yield return null;
        }
        userController.isTeleportReady = true;
        teleportFill.GetComponent<SmoothColorChange>().SwitchColors(teleportReloadingColor, readyColor, teleportLoadedColor, colorFlashTime);
    }

    public void UseBlockMoverBar()
    {
        StartCoroutine(UseBlockMoverBarCoroutine());
    }

    private IEnumerator UseBlockMoverBarCoroutine()
    {
        blockMoverFill.GetComponent<SmoothColorChange>().SwitchColors(blockMoverLoadedColor, blockMoverReloadingColor, 0f);
        float t = blockGoingUpTime;
        while (Input.GetMouseButton(0)) 
        {
            blockMoverBar.value = t / blockGoingUpTime;
            t -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ReloadBlockMoverCoroutine(Mathf.Clamp(t, 0, Mathf.Infinity) / blockGoingUpTime));
    }

    private IEnumerator ReloadBlockMoverCoroutine(float t)
    {
        userController.isBlockMoverReady = false;
        t = blockMoverLoadingTime * t;
        while (t < blockMoverLoadingTime)
        {
            blockMoverBar.value = t / blockMoverLoadingTime;
            t += Time.deltaTime;
            yield return null;
        }
        userController.isBlockMoverReady = true;
        blockMoverFill.GetComponent<SmoothColorChange>().SwitchColors(blockMoverReloadingColor, readyColor, blockMoverLoadedColor, colorFlashTime);
    }
     
    public void TeleportNotReady()
    {
        teleportFill.GetComponent<SmoothColorChange>().SwitchColors(teleportReloadingColor, notReadyColor, teleportReloadingColor, colorFlashTime);
        print("teleport not ready");
    }

    public void BlockMoverNotReady()
    {
        blockMoverFill.GetComponent<SmoothColorChange>().SwitchColors(blockMoverReloadingColor, notReadyColor, blockMoverReloadingColor, colorFlashTime);
        print("block mover not ready");
    }
}
