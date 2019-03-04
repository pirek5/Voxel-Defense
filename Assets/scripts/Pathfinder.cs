using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    private PathCube endWaypoint;

    private Queue<PathCube> frontierWaypoints = new Queue<PathCube>();
    private List<PathCube> exploredWaypoints = new List<PathCube>();
    private List<PathCube> path = new List<PathCube>();

    private void Awake()
    {
        endWaypoint = GameManager.Instance.endWaypoint;
    }

    public List<PathCube> FindPath(PathCube startWaypoint)
    {
        path = FindPath(startWaypoint, endWaypoint);
        return path;
    }

    public List<PathCube> FindPath(PathCube startWaypoint, PathCube endWaypoint)
    {
        path.Clear();
        frontierWaypoints.Clear();
        exploredWaypoints.Clear();

        frontierWaypoints.Enqueue(startWaypoint);
        while(frontierWaypoints.Count > 0)
        {
            PathCube currentWaypoint = frontierWaypoints.Dequeue();
            foreach(PathCube waypoint in currentWaypoint.neighbors) // check all neighbors of waypoint
            {
                if(!frontierWaypoints.Contains(waypoint) && !exploredWaypoints.Contains(waypoint) && !waypoint.isBlocked)// && waypoint.occupupiedBy == null)
                {
                    waypoint.exploredFrom = currentWaypoint;
                    if(waypoint == endWaypoint)
                    {
                        List<PathCube> path = CreatePath(startWaypoint, waypoint);
                        return path;
                    }
                    frontierWaypoints.Enqueue(waypoint);
                }
            }
            exploredWaypoints.Add(currentWaypoint);
        }
        Debug.Log("Can't find path!");
        path.Add(endWaypoint);
        return path;
    }

    public List<PathCube> CreatePath(PathCube startWaypoint, PathCube endWaypoint)
    {
        path.Add(endWaypoint);
        PathCube currentWaypoint = endWaypoint;
        while(currentWaypoint != startWaypoint)
        {
            path.Add(currentWaypoint.exploredFrom);
            currentWaypoint = currentWaypoint.exploredFrom;
        }
        path.Reverse();
        return path;
    }
}
