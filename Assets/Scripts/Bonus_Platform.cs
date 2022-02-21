using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus_Platform : MonoBehaviour
{
    public bool isRight;
    public bool isEnd;
    public int ActiveBox;

    public GameObject[] _carBoxes;


    private int activeBoxCount;


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
                    
                    GameObject obj = new GameObject();
                    obj = other.gameObject;
                    CubeCollect.Instance.Cubes.Remove(other.gameObject);
                    Destroy(other.gameObject);
                    ActiveBox++;

                    Instantiate(obj, _carBoxes[activeBoxCount].transform.position, Quaternion.identity);
                    activeBoxCount++;



                }
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<CubeCollect>().enabled = false;

            Debug.Log(CubeCollect.Instance.Cubes.Count);
            if (CubeCollect.Instance.Cubes.Count == 1)
            {
                GameManager.Instance._carAnim.SetBool("isEnd", true);
                StartCoroutine(finalScene());
            }
        }
    }

    IEnumerator finalScene()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance._LevelEnded.SetActive(true);
    }
}
