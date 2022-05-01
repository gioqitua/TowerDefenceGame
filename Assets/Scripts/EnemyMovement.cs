using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] Pathfinder pathfinder;
    private void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(Move(path));
    }
    IEnumerator Move(List<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

}
