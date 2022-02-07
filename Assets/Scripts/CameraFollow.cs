using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 Distance;
    private void Start()
    {
        Distance = transform.position - player.transform.position;

    }
    void Update()
    {
        transform.position = player.transform.position + Distance;
    }
}
