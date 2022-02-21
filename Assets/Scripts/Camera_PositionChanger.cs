using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Camera_PositionChanger : MonoBehaviour
{
    [SerializeField] private Transform _cameraPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Camera_Changer"))
        {
            Vector3 rot = new Vector3(21.876f, 0, 0);
            Vector3 pos = new Vector3(0.02391304f, 13.4f, -14.3f);

            _cameraPos.DOLocalRotate(rot, 0.5f);
            _cameraPos.DOLocalMove(pos, 0.5f);
            
        }
    }
}
