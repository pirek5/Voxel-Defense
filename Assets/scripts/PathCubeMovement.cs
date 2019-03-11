using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCubeMovement : MonoBehaviour {
    
    //cached
    private float fallingDownTime, movingUpTime;
    private int gridSize = GameManager.GridSize;
    PathCube thisPathCube;
    
    private void Start()
    {
        thisPathCube = GetComponent<PathCube>();
        LoadConfigData();
    }

    private void LoadConfigData()
    {
        if (GameManager.Instance != null)
        {
            fallingDownTime = GameManager.Instance.pathCubeGoingDownTime;
            movingUpTime = GameManager.Instance.pathCubeGoingUpTime;
        }
    }

    public void MoveUpBlock() //Funkcja wywoływana przez klasę UserController w momencie kliknięcia na dany blok
    {
        if (thisPathCube.occupupiedBy == null)
        {
            StartCoroutine(MovingUpCoroutine()); // płynne podnoszenie do góry
            EnemiesController.Instance.FindNewPaths(thisPathCube); // wyszukiwania nowych sciezek
            thisPathCube.isBlocked = true;  //zablokowany dla wyszukiwania ścieżek
        }
    }

    IEnumerator MovingUpCoroutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, GameManager.GridSize, transform.position.z); // wyżej niż aktualna pozycja o  1 jednostkę rozmieszczenia bloków
        var t = 0f;

        while (Input.GetMouseButton(0))
        {
            t += Time.deltaTime / movingUpTime; //stosunek dotychczasowego podoszenia bloku do całkowitego czasu podnoszenia bloku
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        StartCoroutine(FallingDownCoroutine(Mathf.Clamp(t, 0f, 1f))); //opadanie bloku, jeżeli blok nie był podniesiony do końca (opisuje to parametr "t") to wtedy opadanie będzie proporcjonalnie któtsze
    }

    IEnumerator FallingDownCoroutine(float timeFactor) //analogicznie do MovingUpCoroutine
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, 0f, transform.position.z);
        var currentFallingDownTime = fallingDownTime * timeFactor;
        var t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / currentFallingDownTime;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        thisPathCube.isBlocked = false;
        EnemiesController.Instance.FindNewPaths();
    }
}
