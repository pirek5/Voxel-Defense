using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControler : MonoBehaviour {

    //set in editor
    [SerializeField] private LayerMask crosshairPlane;

    //cached
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, crosshairPlane))
        {
            transform.position = hit.point;
        }
    }

    public void ChangeCursorToCrosshair(bool enabled)
    {
        spriteRenderer.enabled = enabled;
        Cursor.visible = !enabled;
    }


}
