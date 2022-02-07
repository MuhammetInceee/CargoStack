using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;
    private FloatingJoystick joystick;
    public float zposition = 0;
    private void Start()
    {
        joystick = GetComponent<FloatingJoystick>();
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Update()
    {
        if (joystick.Horizontal > 0.5f)
        {
            zposition = -1;
            


        }
        else if (joystick.Horizontal < -0.5f)
        {
            zposition = 1;
            

        }
        else
        {
            zposition = 0;
            
        }
    }
}
