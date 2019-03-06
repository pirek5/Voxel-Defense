using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCubesData : MonoBehaviour {

    //cached
    private PathCube[] allPathCubes;
    private Dictionary<Vector2Int, PathCube> grid = new Dictionary<Vector2Int, PathCube>();

    //possible enemy movement directions
    public static readonly Vector2Int[] possibleDirections =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    private static PathCubesData instance;
    public static PathCubesData Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetWaypointsNeighbors();
    }

    public void SetWaypointsNeighbors()
    {
        allPathCubes = FindObjectsOfType<PathCube>(); // find all waypoints in scene

        foreach (PathCube waypoint in allPathCubes)  // fill dictionary with pairs Vector2Int (position) and PathCube class (sort of waypoint)
        {
            Vector2Int waypointPos = waypoint.position;
            if (!grid.ContainsKey(waypointPos))
            {
                grid.Add(waypointPos, waypoint);
            }
            else
            {
                Debug.Log("overlaping waypoint at " + waypoint.position); // two waypoints at the same position
            }
        }
        foreach(PathCube waypoint in allPathCubes) // set all possible neighbors of waypoints
        {
            foreach(Vector2Int direction in possibleDirections)
            {
                Vector2Int neighborPos = waypoint.position + direction;
                if (grid.ContainsKey(neighborPos)) // if there is waypoint at possible direction of move...
                {
                    waypoint.neighbors.Add(grid[neighborPos]); // ... add waypoint with that position to the list of neighbors
                }
            }
        }
    }

    public PathCube FindNearestPathCube(Vector3 position) //nearest path cube to actual position of enemy, used when finding new path
    {
        var closestPathCube = allPathCubes[0];
        foreach (PathCube pathCube in allPathCubes)
        {
            float currentClosestDistance = Vector3.Distance(position, closestPathCube.transform.position);
            float testDistance = Vector3.Distance(position, pathCube.transform.position);
            if (currentClosestDistance <= testDistance) { continue; }
            else { closestPathCube = pathCube; }
        }
        return closestPathCube;
    }
}

