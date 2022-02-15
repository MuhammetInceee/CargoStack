using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys_Instantiater : MonoBehaviour
{
    [SerializeField] private List<GameObject> _toys = new List<GameObject>();
    [SerializeField] private GameObject _instantiatePos;
    private void Start()
    {
        InvokeRepeating("InstantiateToys",0.3f,1.5f);
    }

    void InstantiateToys()
    {
        int rT = Random.Range(0, _toys.Count);

        Instantiate(_toys[rT], _instantiatePos.transform.position, _toys[rT].transform.rotation);
    }
}
