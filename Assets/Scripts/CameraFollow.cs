using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    // Target
    [SerializeField] private GameObject player;
    private Vector3 Distance;
    private void Start()
    {
        // Set distace for camera
        Distance = transform.position - player.transform.position;

    }
    void Update()
    {
        // Follow target
        transform.position = player.transform.position + Distance;
    }
}
