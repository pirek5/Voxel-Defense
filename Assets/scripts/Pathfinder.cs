using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    public List<PathCube> FindPath(PathCube startWaypoint)
    {
        return FindPath(startWaypoint, GameManager.Instance.endWaypoint);
    }

    public List<PathCube> FindPath(PathCube startWaypoint, PathCube endWaypoint)
    {
        Queue<PathCube> frontierWaypoints = new Queue<PathCube>();
        List<PathCube> exploredWaypoints = new List<PathCube>();

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
                        return CreatePath(startWaypoint, waypoint);
                    }
                    frontierWaypoints.Enqueue(waypoint);
                }
            }
            exploredWaypoints.Add(currentWaypoint);
        }

        //shouldnt happen
        Debug.LogError("Can't find path!"); 
        List<PathCube> path = new List<PathCube>();
        path.Add(endWaypoint);
        return path;
    }

    public List<PathCube> CreatePath(PathCube startWaypoint, PathCube endWaypoint)
    {
        List<PathCube> path = new List<PathCube>();
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
