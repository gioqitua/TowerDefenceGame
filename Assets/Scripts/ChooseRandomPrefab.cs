using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomPrefab : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();

    private void Start()
    {
        foreach (var prefab in prefabs)
        {
            prefab.SetActive(false);
        }
        var randomPrefabIndex = Random.Range(0, prefabs.Count);
        prefabs[randomPrefabIndex].SetActive(true);
    }

}
