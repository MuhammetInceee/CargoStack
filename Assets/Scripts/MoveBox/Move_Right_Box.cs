using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Right_Box : MonoBehaviour
{
    private int _speed = 15;


    void Update()
    {
        gameObject.transform.Translate(-(1 * _speed * Time.deltaTime), 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy_Toys"))
        {
            Destroy(gameObject);
        }
    }
}
