using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private InputController Inputcontroller;
    Vector3 goright;
    Vector3 goleft;
    private int zPos = 1;
    private float speed = 7;

    void Update()
    {
        PlayerMovement();
    }


    void PlayerMovement()
    {
        goright = new Vector3(-2f, transform.position.y, transform.position.z);
        goleft = new Vector3(2f, transform.position.y, transform.position.z);


        //transform.position += Vector3.forward * speed * Time.deltaTime;
        if (Inputcontroller.zposition == -zPos)//goleft2
        {
            transform.position = Vector3.Lerp(transform.position, goleft, speed * Time.deltaTime);
        }
        else if (Inputcontroller.zposition == zPos)//goright-2
        {
            transform.position = Vector3.Lerp(transform.position, goright, speed * Time.deltaTime);
        }
    }
    
    
}
