using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy_Manager : MonoBehaviour
{
    private float _speed = 1;

    void Start()
    {
        
    }

    void Update()
    {
        
        transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
