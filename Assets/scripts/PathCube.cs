using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCube : MonoBehaviour {

    //cached
    private int gridSize = GameManager.GridSize;
    private float fallingDownTime, movingUpTime;

    //state
    [HideInInspector] public GameObject occupupiedBy = null;
    [HideInInspector] public Vector2Int position;
    [HideInInspector] public PathCube exploredFrom;
    [HideInInspector] public bool isBlocked;
    [HideInInspector] public List<PathCube> neighbors = new List<PathCube>();

    private void Awake()
    {
        int xPos = (int)transform.position.x / gridSize;
        int yPos = (int)transform.position.z / gridSize;
        position = new Vector2Int(xPos, yPos);
    }

    private void Start()
    {
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

    private void Update()
    {
        if (occupupiedBy != null)
        {
            int distanceBetweenPathCubeAndCurrentEnemy = (int)Vector3.Distance(this.transform.position, occupupiedBy.transform.position);
            if (distanceBetweenPathCubeAndCurrentEnemy >= gridSize)
            {
                occupupiedBy = null;
            }
        }
    }

    public void MoveUpBlock() //Funkcja wywoływana przez klasę UserController w momencie kliknięcia na dany blok
    {
        if (occupupiedBy == null)
        {
            StartCoroutine(MovingUpCoroutine()); // płynne podnoszenie do góry
            EnemiesController.Instance.FindNewPaths(this); // wyszukiwania nowych sciezek
            isBlocked = true;  //zablokowany dla wyszukiwania ścieżek
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
        isBlocked = false;
        EnemiesController.Instance.FindNewPaths();
    }

}
