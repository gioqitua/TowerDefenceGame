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
    [SerializeField] public GameObject oldrenderer;
    [SerializeField] List<GameObject> cubePrefabs = new List<GameObject>();

    [SerializeField] private float yThreshhold = -0.6f;

    public bool canPlaceTower = true;

    // private void Start()
    // {
    //     if (cubePrefabs.Count > 1) ChangePrefabWithRandom();
    // }

    // private void ChangePrefabWithRandom()
    // {
    //     var currentPrefabIndex = UnityEngine.Random.Range(0, cubePrefabs.Count);
    //     var newPos = new Vector3(transform.position.x, transform.position.y + yThreshhold, transform.position.z);
    //     Instantiate(cubePrefabs[currentPrefabIndex], newPos, Quaternion.identity);

    //     Debug.Log("Prefabs Changed");
    // }

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
    private void Update()
    {

        if (Input.touchCount == 1)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);

            if (GetComponent<Collider>() == Physics2D.OverlapPoint(touchPos))
            {
                //Do stuff with it here like check gameObject tags and such.
                ChooseWaypoint();
            }
        }
    }
    void ChooseWaypoint()
    {
        if (canPlaceTower)
        {
            TowerPool.Instance.AddTower(this);
        }
        else
        {
            Debug.Log("Cant place tower");
        }
    }

    private void OnMouseDown()
    {
        ChooseWaypoint();
    }
}
