using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]

public class CubeEditor : MonoBehaviour
{
    //config
    private int gridSize = GameManager.GridSize;

    //state
    private int posX, posY, posZ;

    void Update()
    {
        if (Application.isPlaying) return;
        SnapToGrid();         // align cube to grid
        UpdateName();         // naming according to current position
    }

    private void SnapToGrid()
    {
        posX = Mathf.RoundToInt(transform.position.x / gridSize);
        posY = Mathf.RoundToInt(transform.position.y / gridSize);
        posZ = Mathf.RoundToInt(transform.position.z / gridSize);


        transform.position = new Vector3(posX * gridSize, posY * gridSize, posZ * gridSize);
    }

    private void UpdateName()
    {
        
        string height;
        if (posY != 0)
        {
            height = posY.ToString();
        }
        else
        {
            height = "";
        }

        string cubeCoordinates = " ("+ posX + " , " + posZ + ") " + height;


        if (GetComponent<TowerCube>())
        {
            gameObject.name = "Tower Cube" + cubeCoordinates; //update name (tower cubes)
        }
        else if (GetComponent<PathCube>())
        {
            gameObject.name = "PathCube" + cubeCoordinates; //update name (path cubes)
        }
        else
        {
            gameObject.name = "Environment Cube" + cubeCoordinates; //update name (neutral cubes)
        }
    }
}

