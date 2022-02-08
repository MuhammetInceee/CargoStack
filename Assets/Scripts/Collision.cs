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
            // it have some bug, so we can use begin of the level.
            if (other.gameObject.tag.Equals("Obstacle"))
            {
                float randomX = Random.Range(-1, 1);
                float randomZ = Random.Range(5, 15);
                if (gameObject.tag.Equals("Collected"))
                {
                    GameObject Cubei = CubeCollect.Instance.Cubes[i];
                    Cubei.gameObject.tag = "Collectable";
                    Destroy(Cubei.GetComponent<Collision>());

                    Cubei.gameObject.transform.DOLocalMove(new Vector3(transform.position.x + randomX, 0, transform.position.z - randomZ), 1f);
                    Cubei.gameObject.transform.parent = other.transform;
                    Cubei.transform.localPosition = new Vector3(0, 2, 0);



                    Cubei.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    Cubei.gameObject.GetComponent<Rigidbody>().isKinematic = true;




                    Cubei.gameObject.GetComponent<Collider>().isTrigger = false;
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

            else if (other.gameObject.tag.Equals("Closer"))
            {
                if (i == 0)
                {
                    Debug.Log("Dude come onnn, I am player. I can't change");
                    return;
                }
                else
                {
                    Debug.Log("Dude hi, we meet new tag again :)");
                    CubeCollect.Instance.Cubes[i].transform.GetChild(0).gameObject.SetActive(false);
                    CubeCollect.Instance.Cubes[i].transform.GetChild(1).gameObject.SetActive(true);
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
