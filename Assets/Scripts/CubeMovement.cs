using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{

    [SerializeField] private float _playerSpeed;
    private float _playerborder = 1.5f;

    

    void Update()
    {
        if(!InputController.Instance.isGameEnd)
            transform.Translate(0f, 0f, _playerSpeed * Time.deltaTime);

        PlayerGetBorder();
    }

    void PlayerGetBorder()
    {
        if (transform.position.x < -_playerborder)
            transform.position = new Vector3(-_playerborder, transform.position.y, transform.position.z);

        if(transform.position.x > _playerborder)
            transform.position = new Vector3(_playerborder, transform.position.y, transform.position.z);
    }


}
