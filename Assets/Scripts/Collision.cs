using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject _allCharacter;
    private float _sellSpeed = 1;

    private void Update()
    {
        if (_allCharacter == null)
            _allCharacter = GameObject.Find("AllCharacters");

        CalculateIndex();
    }


    private void OnTriggerExit(Collider other)
    {
        for (int i = CubeCollect.Instance.Cubes.Count - 1; i > CubeCollect.Instance.Cubes.IndexOf(other.gameObject); i--)
        {

            //GetChild(0) = Empty Box
            //GetChild(1) = Full Box
            //GetChild(2) = Close Box
            //GetChild(3) = Packed Box


            if (other.gameObject.CompareTag("Obstacle"))
            {

                if (gameObject.name == "Player")
                {
                    return;
                }


                float randomX = Random.Range(-4, 5);
                float randomZ = Random.Range(7, 10);
                if (gameObject.CompareTag("Collected"))
                {
                    GameObject Cubei = CubeCollect.Instance.Cubes[i];
                    Cubei.gameObject.tag = "Collectable";

                    Cubei.gameObject.transform.DOLocalMove(new Vector3(transform.localPosition.x + randomX, 0, transform.localPosition.z + randomZ), 1f);
                    Cubei.gameObject.transform.parent = other.transform;
                    Cubei.transform.localPosition = new Vector3(0, 2, 0);



                    Cubei.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    Cubei.gameObject.GetComponent<Rigidbody>().isKinematic = true;




                    Cubei.gameObject.GetComponent<Collider>().isTrigger = false;
                    CubeCollect.Instance.Cubes.RemoveAt(i);

                }
            }


            else if (other.gameObject.CompareTag("Closer"))
            {
                if (gameObject.name == "Player")
                {
                    // If Player Collide, Do Nothing
                    return;
                }
                else
                {

                    // If Empty Box Come Closer Empty Box Set Active False
                    if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(0).gameObject.gameObject.SetActive(false);
                    }

                    // If Full Box Come Closer Full Box Set Active False
                    else if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    // If Packed Box Come Closer Do Nothing
                    else if (gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
                    {
                        return;
                    }

                    // Box Change to Close Box
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);

                    if (!other.gameObject.transform.parent.transform.GetChild(0).gameObject.activeInHierarchy)
                        other.gameObject.transform.parent.transform.GetChild(0).gameObject.SetActive(true);

                    Debug.Log("Dude hi, I am trapped inside the box");
                }

                // When object pass through to gates just one time boing effect 
                if (this.gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }

            else if (other.gameObject.CompareTag("Filler"))
            {
                // If Player Collide, Do Nothing
                if (gameObject.name == "Player")
                {
                    return;
                }
                else
                {
                    // If Empty Box Come Filler Empty Box Set Active False
                    if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    // If Packed Box or Close Box Come Filler, Do Nothing
                    else if (gameObject.transform.GetChild(2).gameObject.activeInHierarchy || gameObject.transform.GetChild(3).gameObject.activeInHierarchy)
                    {
                        Debug.Log("Dude you are close or packed, how can i fill? Are you serious amk");
                        return;
                    }

                    gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    Debug.Log("Dudeee, I am Full :)");
                }

                // When object pass through to gates just one time boing effect 
                if (gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }

            else if (other.gameObject.CompareTag("Packer"))
            {
                // If Player Collide, Do Nothing
                if (gameObject.name == "Player")
                {
                    return;
                }
                else
                {
                    // If Empty Box Come Packer Empty Box Set Active False
                    if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    // If Full Box Come Packer Full Box Set Active False
                    else if (gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    // If Close Box Come Packer Close Box Set Active False
                    else if (gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
                    {
                        gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                }

                // When object pass through to gates just one time boing effect 
                if (gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }



            else if (other.gameObject.CompareTag("Burner"))
            {


                // If Player Collide, Do Nothing
                if (gameObject.name == "Player")
                {
                    return;
                }

                TextureChanger();

                if (!other.gameObject.transform.parent.transform.GetChild(0).gameObject.activeInHierarchy)
                    other.gameObject.transform.parent.transform.GetChild(0).gameObject.SetActive(true);

                // When object pass through to gates just one time boing effect 
                if (gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }

            else if (other.gameObject.CompareTag("Sell"))
            {
                // If Player Collide, Do Nothing
                if (gameObject.name == "Player")
                {
                    return;
                }


                if (CubeCollect.Instance.Cubes.Contains(gameObject))
                {
                    GameObject obj = gameObject;
                    CubeCollect.Instance.Cubes.Remove(gameObject);
                    Destroy(gameObject);
                    obj.AddComponent<Move_Left_Box>();

                    Instantiate(obj, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.8f, other.gameObject.transform.position.z), Quaternion.identity);

                }

            }

            else if (other.gameObject.CompareTag("CargoCar"))
            {
                Destroy(gameObject);
            }

            else if (other.gameObject.CompareTag("Destroyer_Hand"))
            {
                // If Player Collide, Do Nothing
                if (gameObject.name == "Player")
                {
                    return;
                }



                if (CubeCollect.Instance.Cubes.Contains(gameObject))
                {
                    Destroy(gameObject);
                    CubeCollect.Instance.Cubes.Remove(gameObject);
                }
                //Destroyer_Hand(other);

                if (!other.gameObject.transform.parent.transform.parent.transform.GetChild(0).gameObject.activeInHierarchy)
                    other.gameObject.transform.parent.transform.parent.transform.GetChild(0).gameObject.SetActive(true);

            }

        }
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        // Collect and follow the player
        if (other.gameObject.CompareTag("Collectable"))
        {
            if (!CubeCollect.Instance.Cubes.Contains(other.gameObject))
            {
                other.gameObject.tag = "Collected";
                CubeCollect.Instance.StackCube(other.gameObject, CubeCollect.Instance.Cubes.Count - 1);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyable"))
        {
            if (gameObject.name == "Player")
            {
                // If Player Collide, Do Nothing
                return;
            }

            if (CubeCollect.Instance.Cubes.Contains(gameObject))
            {
                Destroy(gameObject);
                CubeCollect.Instance.Cubes.Remove(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Destroyable"))

        {
            if (gameObject.name == "Player")
            {
                // If Player Collide, Do Nothing
                return;
            }

            if (CubeCollect.Instance.Cubes.Contains(gameObject))
            {
                Destroy(gameObject);
                CubeCollect.Instance.Cubes.Remove(gameObject);
            }

            if (!other.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                other.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    void CalculateIndex()
    {
        for (int i = 0; i < CubeCollect.Instance.Cubes.Count; i++)
        {
            int index = CubeCollect.Instance.Cubes.IndexOf(CubeCollect.Instance.Cubes[i].gameObject);

            if (index == 0)
                CubeCollect.Instance.Cubes[i].transform.localPosition = new Vector3(CubeCollect.Instance.Cubes[i].transform.localPosition.x, CubeCollect.Instance.Cubes[i].transform.localPosition.y, _allCharacter.transform.GetChild(0).localPosition.z);
            else
                CubeCollect.Instance.Cubes[i].transform.localPosition = new Vector3(CubeCollect.Instance.Cubes[i].transform.localPosition.x, CubeCollect.Instance.Cubes[i].transform.localPosition.y, _allCharacter.transform.GetChild(0).localPosition.z + (index - 0.29f));

        }

    }

    void TextureChanger()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            for (int k = 0; k < gameObject.transform.GetChild(i).childCount; k++)
            {
                if (gameObject.transform.GetChild(i).transform.GetChild(k).gameObject.name == "Toys" || gameObject.transform.GetChild(i).transform.GetChild(k).gameObject.name == "Band")
                {
                    continue;
                }

                else
                    gameObject.transform.GetChild(i).transform.GetChild(k).gameObject.GetComponent<MeshRenderer>().material = InputController.Instance._steamMaterial;
            }

            gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = InputController.Instance._steamMaterial;
        }
    }

    void Destroyer_Hand(Collider other)
    {
        if (CubeCollect.Instance.Cubes.Contains(gameObject))
        {
            var index = CubeCollect.Instance.Cubes.IndexOf(gameObject);

            for (int i = 0; i < CubeCollect.Instance.Cubes.Count; i++)
            {
                if (CubeCollect.Instance.Cubes.IndexOf(CubeCollect.Instance.Cubes[i]) > index)
                {

                    float randomX = Random.Range(-2.2f, 3.2f);
                    float randomZ = Random.Range(5, 12);

                    if (gameObject.CompareTag("Collected"))
                    {
                        GameObject Cubei = CubeCollect.Instance.Cubes[i];
                        Cubei.gameObject.tag = "Collectable";

                        Cubei.gameObject.transform.DOLocalMove(new Vector3(transform.localPosition.x + randomX, 0, transform.localPosition.z + randomZ), 1f);
                        // other yerine modelin içine bir tane yer yap ona ata fonsionun içine attýðýn collider ý kaldýr
                        Cubei.gameObject.transform.parent = other.transform;
                        Cubei.transform.localPosition = new Vector3(0, 2, 0);

                        Cubei.gameObject.GetComponent<Rigidbody>().useGravity = true;
                        Cubei.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                        Cubei.gameObject.GetComponent<Collider>().isTrigger = false;
                        CubeCollect.Instance.Cubes.RemoveAt(i);

                    }
                }
            }

            CubeCollect.Instance.Cubes.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
