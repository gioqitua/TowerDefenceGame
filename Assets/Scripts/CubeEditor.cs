using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] TMP_Text coordinates;
    [SerializeField] Waypoint waypoint;
    Vector3 gridPos;
    [SerializeField] bool setText = false; //turn off onRelease (High Gc Alloc)
    void Update()
    {
        SnapToGrid();
        if (setText) SetText(); 
    }

    private void SetText()
    {
        string cubeCoordinates = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;

        gameObject.name = cubeCoordinates;

        coordinates.SetText(cubeCoordinates);
    }

    private void SnapToGrid()
    {
        var gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(waypoint.GetGridPos().x, 0f, waypoint.GetGridPos().y);
    }
}
