using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] float maxYRotationSpeed = 5f;
    [SerializeField] float randomSpeed;
    private void Start()
    {
        randomSpeed = Random.Range(-maxYRotationSpeed, maxYRotationSpeed);
    }
    void Update()
    {
        transform.Rotate(0, randomSpeed * Time.deltaTime, 0, Space.Self);
    }
}
