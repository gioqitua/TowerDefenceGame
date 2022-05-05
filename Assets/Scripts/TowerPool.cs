using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPool : MonoBehaviour
{
    public static TowerPool Instance;
    [SerializeField] Tower towerPrefab;
    [SerializeField] int maxTowerCount = 4;
    Queue<Tower> towerQueue = new Queue<Tower>();


    private void Awake()
    {
        Instance = this;
    }

    public void AddTower(Waypoint waypoint)
    {
        if (towerQueue.Count < maxTowerCount)
        {
            CreateTower(waypoint);
        }
        else
        {
            MoveTowerToNewPos(waypoint);
        }

    }

    private void MoveTowerToNewPos(Waypoint newWaypoint)
    {
        Tower oldTower = towerQueue.Dequeue();

        oldTower.transform.position = newWaypoint.transform.position;

        oldTower.currentWaypoint.canPlaceTower = true;

        newWaypoint.canPlaceTower = false;

        oldTower.currentWaypoint = newWaypoint;

        towerQueue.Enqueue(oldTower);
    }

    private void CreateTower(Waypoint waypoint)
    {
        var tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
        tower.transform.parent = transform;
        waypoint.canPlaceTower = false;
        tower.currentWaypoint = waypoint;
        towerQueue.Enqueue(tower);

    }
}