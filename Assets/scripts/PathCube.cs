using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCube : MonoBehaviour { //TODO magic numbers
    //config
    [SerializeField] float towerIncreaseFactor = 70f;
    [SerializeField] float deathIncreaseFactor = 15f;
    [SerializeField] float neighborDeathIncreaseFactor = 7f;

    //cached
    private int gridSize = GameManager.GridSize;

    //state
    [HideInInspector] public Vector2Int position;
    [HideInInspector] public PathCube exploredFrom;
    [HideInInspector] public bool isBlocked;
     public GameObject occupupiedBy = null;
    [HideInInspector] public List<PathCube> neighbors = new List<PathCube>();

    [HideInInspector] public float distanceTraveled = Mathf.Infinity;
    [HideInInspector] public float priority;

    //higher factors means enemies will try to avoid that pathcubes
    private float deathAvoidanceFactor = 0f; // every enemy death in certain pathCube increase factor in this particular pathcube
    private float towerAvoidanceFactor = 0f; // tower presence in particular area increase factor

    private void Awake()
    {
        int xPos = (int)transform.position.x / gridSize;
        int yPos = (int)transform.position.z / gridSize;
        position = new Vector2Int(xPos, yPos);
    }

    private void Update() //check if enemy left this pathCube
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

    public void IncreaseAvoidanceFactorByEnemies()
    {
        deathAvoidanceFactor = deathAvoidanceFactor + deathIncreaseFactor;
        foreach(PathCube neighbor in neighbors)
        {
            neighbor.deathAvoidanceFactor = neighbor.deathAvoidanceFactor + neighborDeathIncreaseFactor;
        }
    }

    public void DecreaseDeathAvoidanceFactor()
    {
        deathAvoidanceFactor = deathAvoidanceFactor * 0.8f;
    }

    public float GetAvoidanceFactor()
    {
        towerAvoidanceFactor = 0;
        GameObject[] towers = TowerSpawner.Instance.towers;

        foreach(GameObject tower in towers)
        {
            float distance = Vector3.Distance(tower.transform.position, transform.position);
            if(distance > 50) { continue; }

            towerAvoidanceFactor = towerAvoidanceFactor + (1f / distance * towerIncreaseFactor);
        }
        return towerAvoidanceFactor + deathAvoidanceFactor;
    }

    public int CompareTo(PathCube other)
    {
        if (this.priority < other.priority)
        {
            return -1;
        }
        else if (this.priority > other.priority)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
