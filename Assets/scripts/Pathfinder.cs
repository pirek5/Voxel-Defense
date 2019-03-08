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
        PriorityQueue frontierWaypoints = new PriorityQueue();
        List<PathCube> exploredWaypoints = new List<PathCube>();

        startWaypoint.distanceTraveled = 0f;
        frontierWaypoints.Enqueue(startWaypoint);
        while(frontierWaypoints.Count > 0)
        {
            PathCube currentWaypoint = frontierWaypoints.Dequeue();
            if (currentWaypoint == endWaypoint)
            {
                return CreatePath(startWaypoint, currentWaypoint);
            }

            foreach (PathCube neighbor in currentWaypoint.neighbors) // check all neighbors of waypoint
            {
                if (!frontierWaypoints.Contains(neighbor) && !exploredWaypoints.Contains(neighbor) && !neighbor.isBlocked) //if neighbor is not already explored
                {
                    neighbor.exploredFrom = currentWaypoint;
                    float newDistanceTraveled = currentWaypoint.distanceTraveled + 1f;
                    neighbor.distanceTraveled = newDistanceTraveled;
                    neighbor.priority = newDistanceTraveled + neighbor.GetAvoidanceFactor();
                    frontierWaypoints.Enqueue(neighbor);
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

    //public static float GetDistance(PathCube source, PathCube target) - to A* algorithm
    //{
    //    int dx = Mathf.Abs(source.position.x - target.position.x);
    //    int dy = Mathf.Abs(source.position.y - target.position.y);

    //    return (dx + dy);
    //}
}
