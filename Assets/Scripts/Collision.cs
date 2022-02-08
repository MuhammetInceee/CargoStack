using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{

    [SerializeField] private GameObject _allCharacter;

    private void Update()
    {
        if (_allCharacter == null)
            _allCharacter = GameObject.Find("AllCharacters");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collect and follow the player
        if (other.gameObject.tag.Equals("Collectable"))
        {
            if (!CubeCollect.Instance.Cubes.Contains(other.gameObject))
            {
                other.tag = "Collected";
                CubeCollect.Instance.StackCube(other.gameObject, CubeCollect.Instance.Cubes.Count - 1);
                other.gameObject.AddComponent<Collision>();

                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
            }

        }



    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = CubeCollect.Instance.Cubes.Count - 1; i > CubeCollect.Instance.Cubes.IndexOf(other.gameObject); i--)
        {
            if (other.gameObject.tag.Equals("Obstacle"))
            {
                float randomX = Random.Range(-1, 1);
                float randomZ = Random.Range(1, 3);
                if (gameObject.tag.Equals("Collected"))
                {
                    GameObject Cubei = CubeCollect.Instance.Cubes[i];
                    CubeCollect.Instance.Cubes[i].gameObject.tag = "Collectable";
                    Destroy(CubeCollect.Instance.Cubes[i].GetComponent<Collision>());
                    Vector3 cube = CubeCollect.Instance.Cubes[i].transform.position;

                    //CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3, 10), ForceMode.Impulse);
                    CubeCollect.Instance.Cubes[i].gameObject.transform.DOLocalMove(new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ), 1f);
                    Cubei.gameObject.transform.parent = other.transform;
                    CubeCollect.Instance.Cubes[i].transform.localPosition = new Vector3(0, 2, 0);



                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;




                    CubeCollect.Instance.Cubes[i].gameObject.GetComponent<Collider>().isTrigger = false;
                    CubeCollect.Instance.Cubes.RemoveAt(i);

                }
            }

            else if (other.gameObject.tag.Equals("Destroyable"))
            {
                if (i == 0)
                {
                    Debug.Log("Dude come onnn, I am player. I can't change");
                    return;
                }

                else
                {
                    Debug.Log("Dude what the fuck, I was destroy amk");
                    Destroy(CubeCollect.Instance.Cubes[i]);
                    CubeCollect.Instance.Cubes.RemoveAt(i);
                }
            }

        }
    }
    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        // Collect and follow the player
        if (other.gameObject.tag.Equals("Collectable"))
        {
            if (!CubeCollect.Instance.Cubes.Contains(other.gameObject))
            {
                other.gameObject.tag = "Collected";
                CubeCollect.Instance.StackCube(other.gameObject, CubeCollect.Instance.Cubes.Count - 1);
                other.gameObject.AddComponent<Collision>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
