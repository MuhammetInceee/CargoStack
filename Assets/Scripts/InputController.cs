using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public bool isMoved;

    [SerializeField] private float _horizontalSpeed;

    public Material _steamMaterial;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
            
    }
    void Update()
    {
        PlayerMovement();

    }


    void PlayerMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + _touch.deltaPosition.x * (_horizontalSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                isMoved = true;
            }
        }
        else
            isMoved = false;
    }
}
