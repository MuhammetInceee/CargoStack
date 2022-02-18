using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Platform : MonoBehaviour
{
    public bool isRight;
    public bool isEnd;
    public int ActiveBox;

    public GameObject[] _carBoxes;


    private void Start()
    {
        _carBoxes = GameObject.FindGameObjectsWithTag("CarTransform");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collected"))
        {
            if (isRight)
            {
                GameObject obj = other.gameObject;
                CubeCollect.Instance.Cubes.Remove(other.gameObject);
                Destroy(other.gameObject);
                obj.AddComponent<Move_Left_Box>();
                obj.GetComponent<Collision>().enabled = true;
                Destroy(gameObject.GetComponent<BoxCollider>());
                Instantiate(obj, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z), Quaternion.identity);
            }
            else if (!isRight && !isEnd)
            {
                GameObject obj = other.gameObject;
                CubeCollect.Instance.Cubes.Remove(other.gameObject);
                Destroy(other.gameObject);
                obj.AddComponent<Move_Right_Box>();
                obj.GetComponent<Collision>().enabled = true;
                Destroy(gameObject.GetComponent<BoxCollider>());
                Instantiate(obj, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z), Quaternion.identity);
            }


            if (isEnd && !isRight)
            {
                if (CubeCollect.Instance.Cubes.Contains(other.gameObject))
                {

                    GameObject obj = other.gameObject;
                    CubeCollect.Instance.Cubes.Remove(other.gameObject);
                    Destroy(other.gameObject);
                    ActiveBox++;

                    for (int i = 0; i < ActiveBox; i++)
                    {
                        Instantiate(obj, _carBoxes[i].transform.position, Quaternion.identity);
                    }

                }
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            InputController.Instance.isGameEnd = true;
        }
    }
}
