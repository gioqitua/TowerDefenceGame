using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] GameObject enemyPathBlockPrefab;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    List<Waypoint> path = new List<Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isPathFinderRunning = true;
    [SerializeField] Waypoint startPoint, finishPoint;
    Waypoint searchPoint;
    [SerializeField] float enemyPathYThreshhold = 0.9f;

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

            PathFind();

            CreatePath();

            ChangePathPrefab();
        }
        return path;
    }
    void ChangePathPrefab()
    {
        foreach (var waypoint in path)
        {
            if (waypoint != finishPoint)
            {
                var pointToInstantiate = waypoint.transform.position;

                pointToInstantiate = new Vector3(pointToInstantiate.x, pointToInstantiate.y - enemyPathYThreshhold, pointToInstantiate.z);

                var newPrefab = Instantiate(enemyPathBlockPrefab, pointToInstantiate, Quaternion.identity);

                newPrefab.transform.parent = gameObject.transform;

                waypoint.oldrenderer.SetActive(false);
            }

        }
    }

    void CreatePath()
    {
        path.Add(finishPoint);
        finishPoint.canPlaceTower = false;
        Waypoint previousPoint = finishPoint.exploredFrom;

        while (previousPoint != startPoint)
        {
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

    }
}
