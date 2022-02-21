using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys_Manager : MonoBehaviour
{
    private float _speed = 1;
    [SerializeField] bool isFalling = false;




    void Update()
    {
        if (!isFalling)
        {
            if (!Toys_Instantiater.goRight)
            {
                transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
                Debug.Log("aga sol");
            }
            else
            {
                transform.Translate(1 * _speed * Time.deltaTime, 0, 0);
                Debug.Log("aga sag");
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Down_Collider"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            isFalling = true;
        }

        if (other.gameObject.CompareTag("Destroy_Toys"))
        {
            Destroy(gameObject);
        }
    }
}