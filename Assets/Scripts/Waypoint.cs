using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Waypoint : MonoBehaviour
{
    Color exploredColor = Color.yellow;
    public Waypoint exploredFrom;
    [SerializeField] public bool isChecked = false;
    const int gridSize = 1;
    Vector2Int gridPos;
    [SerializeField] GameObject towerPrefab;
    public bool canPlaceTower = true;

    public Vector2Int GetGridPos()
    {
        int x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        int y = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        return new Vector2Int(x, y);
    }
    public int GetGridSize()
    {
        return gridSize;
    }

    internal void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;

    }

    private void OnMouseDown()
    {
        if (canPlaceTower)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            canPlaceTower = false;
        }
        else
        {
            Debug.Log("Cant place tower");
        }
    }
}