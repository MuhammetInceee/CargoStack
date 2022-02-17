using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toys_Instantiater : MonoBehaviour
{
    [SerializeField] private List<GameObject> _toys = new List<GameObject>();
    [SerializeField] private GameObject _instantiatePos;

    [SerializeField] private bool isForAd = false;
    private void Start()
    {
        InvokeRepeating("InstantiateToys", 0.2f, 1f);
    }

    void InstantiateToys()
    {
        if (isForAd)
        {
            int rT = Random.Range(3, _toys.Count);

            Instantiate(_toys[rT], _instantiatePos.transform.position, _toys[rT].transform.rotation, gameObject.transform.GetChild(2));

        }
        else
        {
            int rT = Random.Range(0, 3);

            Instantiate(_toys[rT], _instantiatePos.transform.position, _toys[rT].transform.rotation, gameObject.transform.GetChild(2));
        }
    }
}