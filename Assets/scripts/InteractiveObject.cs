using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour // NOT IN USE ANYMORE
{
    private TowerCube towerCube;
    private PathCube pathCube;

    private void Awake()
    {
        towerCube = GetComponent<TowerCube>();
        pathCube = GetComponent<PathCube>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (towerCube)
            {
                //PlaceTower();
            }
            else if(pathCube)
            {
                //MoveUpBlock();
            }
        }
    }

    //void PlaceTower()
    //{
    //    if (towerCube.isPlaceable)
    //    {
    //        TowerSpawner.Instance.SpawnTower(this.towerCube);
    //        towerCube.isPlaceable = false;
    //    }
    //}

    //private void MoveUpBlock()
    //{
    //    if(pathCube.occupupiedBy == null)
    //    {
    //        StartCoroutine(MovingUpCorutine());
    //        EnemiesController.Instance.FindNewPaths(this.pathCube);
    //        pathCube.isBlocked = true;
    //    }
    //}

    IEnumerator MovingUpCorutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, GameManager.GridSize, transform.position.z);
        var t = 0f;
        var timeToMove = 0.5f; // going up duration
        while (Input.GetMouseButton(0))
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        StartCoroutine(FallingDownCorutine()); // falling down works fine
    }

    IEnumerator FallingDownCorutine()
    {

        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, 0f, transform.position.z);
        var t = 0f;
        var timeToMove = 0.1f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        pathCube.isBlocked = false;
        EnemiesController.Instance.FindNewPaths();
    }
}

