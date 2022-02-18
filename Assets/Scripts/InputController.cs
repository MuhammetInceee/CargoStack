using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public bool isMoved;

    [SerializeField] private float _horizontalSpeed;

    public Material _steamMaterial;

    public bool isGameEnd;

    private float _playerborder = 1.5f;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
            
    }
    void Update()
    {

        if (!isGameEnd)
        {
            PlayerMovement();
            PlayerGetBorder();
        }
        
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

    void PlayerGetBorder()
    {

        if (transform.position.x < -_playerborder)
            transform.position = new Vector3(-_playerborder, transform.position.y, transform.position.z);

        if (transform.position.x > _playerborder)
            transform.position = new Vector3(_playerborder, transform.position.y, transform.position.z);
    }
}
