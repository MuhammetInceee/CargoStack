using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private InputController Inputcontroller;
    Vector3 goright;
    Vector3 goleft;

    void Update()
    {
        PlayerMovement();
    }


    void PlayerMovement()
    {
        goright = new Vector3(-2f, transform.position.y, transform.position.z);
        goleft = new Vector3(2f, transform.position.y, transform.position.z);


        //transform.position += Vector3.forward * speed * Time.deltaTime;
        if (Inputcontroller.zposition == -1)//goleft2
        {
            transform.position = Vector3.Lerp(transform.position, goleft, 3 * Time.deltaTime);
        }
        else if (Inputcontroller.zposition == 1)//goright-2
        {
            transform.position = Vector3.Lerp(transform.position, goright, 3 * Time.deltaTime);
        }
    }
    
    
}
