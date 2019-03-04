using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVisual : MonoBehaviour {

    //set in editor
    [SerializeField] private UiBar teleportBar;
    [SerializeField] private UiBar blockMoverBar;
    [SerializeField] private GameObject teleportFill, blockMoverFill;

    //config
    [SerializeField] private Color teleportReloadingColor, teleportLoadedColor, blockMoverReloadingColor, blockMoverLoadedColor, notReadyColor, readyColor; //TODO lista kolorów, teleport ładujacy się, teleport naładowany, block mover łądujący się, block mover naładowany, pasek nie gotowy, pasek gotowy
    [SerializeField] private float colorsBlendingTimeAfterReload, colorFlashTime;

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
        teleportFill.GetComponent<SmoothColorChange>().SwitchColors(teleportReloadingColor, readyColor, teleportLoadedColor, colorFlashTime, 0f, colorFlashTime);
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
        blockMoverFill.GetComponent<SmoothColorChange>().SwitchColors(blockMoverReloadingColor, readyColor, blockMoverLoadedColor, colorFlashTime, 0f, colorFlashTime);
    }
     
    public void TeleportNotReady()
    {
        teleportFill.GetComponent<SmoothColorChange>().SwitchColors(teleportReloadingColor, notReadyColor, teleportReloadingColor, colorFlashTime, 0f, colorFlashTime);
        print("teleport not ready"); //TODO zmiany kolorów na wzór highlighted;
    }

    public void BlockMoverNotReady()
    {
        blockMoverFill.GetComponent<SmoothColorChange>().SwitchColors(blockMoverReloadingColor, notReadyColor, blockMoverReloadingColor, colorFlashTime, 0f, colorFlashTime);
        print("block mover not ready");
    }
}
