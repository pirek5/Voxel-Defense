using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour {

    //Singleton
    private static UserController instance;
    public static UserController Instance { get{return instance;} set{instance = value;}}

    //set in editor
    [SerializeField] private LayerMask interactiveObjects;
    public GameObject crosshair;

    // cached
    private GameObject clickedGameObject;
    private UIVisual uiVisual;
    private CursorControler cursorController;

    //state
    public static string highlightedObject;
    public bool isTeleportReady = true;
    public bool isBlockMoverReady = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            uiVisual = GetComponent<UIVisual>();
            cursorController = GetComponentInChildren<CursorControler>();
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void OnDisable()
    {
        highlightedObject = null;
        clickedGameObject = null;
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(1) && TowerSpawner.Instance != null) //manual aiming
        {
            TowerSpawner.Instance.SwitchAutomaticManual(true);
            cursorController.ChangeCursorToCrosshair(true);
        }

        if (Input.GetMouseButtonUp(1) && TowerSpawner.Instance != null) //automatic aiming
        {
            TowerSpawner.Instance.SwitchAutomaticManual(false);
            cursorController.ChangeCursorToCrosshair(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactiveObjects))
        {
           highlightedObject = hit.transform.gameObject.name;
           if (Input.GetMouseButtonDown(0))
           {
               clickedGameObject = hit.transform.gameObject;
               LeftMouseButtonAction();
           }
        }
        else
        {
            //cursorController.SetCursor(CursorState.normalCursor);
            highlightedObject = null;
        }

    }

    private void LeftMouseButtonAction()
    {
        if (clickedGameObject.GetComponentInChildren<TowerCube>())
        {
            if(!clickedGameObject.GetComponentInChildren<TowerCube>().isPlaceable) { return; }

            if (isTeleportReady)
            {
                clickedGameObject.GetComponentInChildren<TowerCube>().PlaceTower();
                uiVisual.ReloadTeleport();
            }
            else
            {
                uiVisual.TeleportNotReady();
            }

        }
        else if (clickedGameObject.GetComponentInChildren<PathCube>())
        {
            if (isBlockMoverReady)
            {
                clickedGameObject.GetComponentInChildren<PathCube>().MoveUpBlock();
                uiVisual.UseBlockMoverBar();
            }
            else
            {
                uiVisual.BlockMoverNotReady();
            }
            
        }
    }

    private void RightMouseButtonAction(bool enabled)
    {
        
    }
}
