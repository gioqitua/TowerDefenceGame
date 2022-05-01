using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    public List<Waypoint> path = new List<Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isPathFinderRunning = true;
    [SerializeField] Waypoint startPoint, finishPoint;
    Waypoint searchPoint;
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            SetStartEndColor();
            PathFind();
            CreatePath();
        }
        return path;
    }

    void CreatePath()
    {
        path.Add(finishPoint);
        finishPoint.canPlaceTower = false;
        Waypoint previousPoint = finishPoint.exploredFrom;

        while (previousPoint != startPoint)
        {
            previousPoint.SetTopColor(Color.grey);
            path.Add(previousPoint);
            previousPoint.canPlaceTower = false;
            previousPoint = previousPoint.exploredFrom;  // ;)))))
        }
        path.Add(startPoint);
        startPoint.canPlaceTower = false;
        path.Reverse();
    }
    private void PathFind()
    {
        queue.Enqueue(startPoint);
        while (queue.Count > 0 && isPathFinderRunning == true)
        {
            searchPoint = queue.Dequeue();

            searchPoint.isChecked = true;

            CheckForEndPoint();
            LookNearestWaypoints();
        }
    }

    private void CheckForEndPoint()
    {
        if (searchPoint == finishPoint)
        {
            isPathFinderRunning = false;
        }
    }

    private void LookNearestWaypoints()
    {
        if (!isPathFinderRunning) return;

        foreach (var direction in directions)
        {
            Vector2Int nearWaypointCoordinates = searchPoint.GetGridPos() + direction;
            if (grid.ContainsKey(nearWaypointCoordinates))
            {
                var nearPoint = grid[nearWaypointCoordinates];
                AddPointToQueue(nearPoint);
            }
        }
    }

    private void AddPointToQueue(Waypoint nearPoint)
    {

        if (nearPoint.isChecked || queue.Contains(nearPoint))
        {
            return;
        }
        else
        {
            queue.Enqueue(nearPoint);
            nearPoint.exploredFrom = searchPoint;
        }
    }

    private void SetStartEndColor()
    {
        startPoint.SetTopColor(Color.red);
        finishPoint.SetTopColor(Color.green);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();

            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Repeating Block : " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
        Debug.Log("Block Count : " + grid.Count);
    }
}
