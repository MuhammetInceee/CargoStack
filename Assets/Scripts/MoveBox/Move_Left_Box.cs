using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Left_Box : MonoBehaviour
{
    private int _speed = 15;
    private bool isGoing = true;
    void Update()
    {
        if (!isGoing) return;
        gameObject.transform.Translate(1 * _speed * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy_Toys"))
        {
            isGoing = false;
            other.gameObject.transform.parent.GetComponent<Animator>().SetBool("happy", true);
        }
    }
}
