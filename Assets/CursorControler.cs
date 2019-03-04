using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CursorState { normalCursor, forbidden, interactive };

public class CursorControler : MonoBehaviour {

    ////set in editor
    [SerializeField] private LayerMask crosshairPlane;
    //public Sprite[] cursors;

    //config
    [SerializeField] private float crosshairHeight;

    ////cached
    //SpriteRenderer spriteRenderer;

    //private void Awake()
    //{
    //    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    //}

    //private void OnEnable()
    //{
    //    Cursor.visible = false;
    //}

    //private void OnDisable()
    //{
    //    Cursor.visible = true;
    //}

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, crosshairPlane))
        {
            transform.position = hit.point;
        }
    }

    //public void SetCursor(CursorState cursorState)
    //{
    //    spriteRenderer.sprite = cursors[(int)cursorState];
    //}


}
