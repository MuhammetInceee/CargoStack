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

    [SerializeField]
    private GameObject _score;

    [SerializeField]
    private GlobalScoreSettings globalScore;

    private GameScoreManager scoreManager;

    private void Start()
    {
        _carBoxes = GameObject.FindGameObjectsWithTag("CarTransform");

        _score = GameObject.Find("ScoreManager");

        scoreManager = _score.GetComponent<GameScoreManager>();

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

            if (CubeCollect.Instance.Cubes.Count == 1)
            {
                GameManager.Instance._carAnim.SetBool("happy", true);
                StartCoroutine(finalScene());
            }
        }
    }

    IEnumerator finalScene()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance._LevelEnded.SetActive(true);
        CalculateFinalScore();
    }

    void CalculateFinalScore()
    {
        Debug.Log(scoreManager.Score);
        if(gameObject.name == "P1")
        {
            globalScore.GLOBALCOIN += scoreManager.Score;
        }
        else if(gameObject.name == "P2")
        {
            scoreManager.Score *= 2;
            globalScore.GLOBALCOIN += scoreManager.Score;
        }
        else if (gameObject.name == "P3")
        {
            scoreManager.Score *= 3;
            globalScore.GLOBALCOIN += scoreManager.Score;
        }
        else if (gameObject.name == "P4")
        {
            scoreManager.Score *= 4;
            globalScore.GLOBALCOIN += scoreManager.Score;
        }
        else if (gameObject.name == "P5")
        {
            scoreManager.Score *= 5;
            globalScore.GLOBALCOIN += scoreManager.Score;
        }
        Debug.Log(scoreManager.Score);
    }
}
