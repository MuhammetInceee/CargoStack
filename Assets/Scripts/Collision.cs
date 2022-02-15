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


            // it have some bug, so we can use begin of the level.
            if (other.gameObject.CompareTag("Obstacle"))
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


            else if (other.gameObject.CompareTag("Destroyable"))
            {
                if (i == 0)
                {
                    // If Player Collide, Do Nothing
                    return;
                }
                else
                {
                    Debug.Log("Dude what the fuck, I was destroy amk");
                    Destroy(CubeCollect.Instance.Cubes[i]);
                    CubeCollect.Instance.Cubes.RemoveAt(i);
                }
            }

            else if (other.gameObject.CompareTag("Closer"))
            {
                if (this.gameObject.name == "Player")
                {
                    // If Player Collide, Do Nothing
                    return;
                }
                else
                {
                    Debug.Log(this.gameObject.transform.GetChild(0).gameObject.name);

                    // If Empty Box Come Closer Empty Box Set Active False
                    if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(0).gameObject.gameObject.SetActive(false);
                    }

                    // If Full Box Come Closer Full Box Set Active False
                    else if (this.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    // If Packed Box Come Closer Do Nothing
                    else if (this.gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
                    {
                        return;
                    }

                    // Box Change to Close Box
                    this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

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
                if (this.gameObject.name == "Player")
                {
                    return;
                }
                else
                {
                    // If Empty Box Come Filler Empty Box Set Active False
                    if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    // If Packed Box or Close Box Come Filler, Do Nothing
                    else if (this.gameObject.transform.GetChild(2).gameObject.activeInHierarchy || this.gameObject.transform.GetChild(3).gameObject.activeInHierarchy)
                    {
                        Debug.Log("Dude you are close or packed, how can i fill? Are you serious amk");
                        return;
                    }

                    this.gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    Debug.Log("Dudeee, I am Full :)");
                }

                // When object pass through to gates just one time boing effect 
                if (this.gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }

            else if (other.gameObject.CompareTag("Packer"))
            {
                // If Player Collide, Do Nothing
                if (this.gameObject.name == "Player")
                {
                    return;
                }
                else
                {
                    // If Empty Box Come Packer Empty Box Set Active False
                    if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    // If Full Box Come Packer Full Box Set Active False
                    else if (this.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }

                    // If Close Box Come Packer Close Box Set Active False
                    else if (this.gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
                    {
                        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                }

                // When object pass through to gates just one time boing effect 
                if (this.gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }



            else if (other.gameObject.CompareTag("Burner"))
            {
                // This Code Blocks Box Texture Changer


                // If Player Collide, Do Nothing
                if (this.gameObject.name == "Player")
                {
                    return;
                }
                else
                {

                    // If Open Box Collide Burner
                    if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        SetSteamMaterial(0);
                    }

                    // If Full Box Collide Burner
                    else if (this.gameObject.transform.GetChild(1).gameObject.activeInHierarchy)
                    {
                        SetSteamMaterial(1);
                    }

                    // If Close Box Collide Burner
                    else if (this.gameObject.transform.GetChild(2).gameObject.activeInHierarchy)
                    {
                        SetSteamMaterial(2);
                    }

                    // If Packed Box Collide Burner
                    else if (this.gameObject.transform.GetChild(3).gameObject.activeInHierarchy)
                    {
                        SetSteamMaterial(3);
                    }

                }

                // When object pass through to gates just one time boing effect 
                if (this.gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
                {
                    StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
                }
            }

            else if (other.gameObject.CompareTag("Sell"))
            {

                // If Player Collide, Do Nothing
                if (this.gameObject.name == "Player")
                {
                    return;
                }


                if (CubeCollect.Instance.Cubes.Contains(gameObject))
                {
                    CubeCollect.Instance.Cubes.Remove(gameObject);
                    Destroy(gameObject);

                }

            }
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {

        // Collect and follow the player
        if (other.gameObject.CompareTag("Collectable"))
        {
            if (!CubeCollect.Instance.Cubes.Contains(other.gameObject))
            {
                other.gameObject.tag = "Collected";
                CubeCollect.Instance.StackCube(other.gameObject, CubeCollect.Instance.Cubes.Count - 1);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
    }

    void SetSteamMaterial(int whichChild)
    {
        this.gameObject.transform.GetChild(whichChild).gameObject.GetComponent<MeshRenderer>().material = InputController.Instance._steamMaterial;
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (this.gameObject.transform.GetChild(whichChild).gameObject.transform.GetChild(i).gameObject.name == "Toys" ||
                this.gameObject.transform.GetChild(whichChild).gameObject.transform.GetChild(i).gameObject.name == "Band")

                this.gameObject.transform.GetChild(whichChild).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = InputController.Instance._defaultMaterial;

            else
                this.gameObject.transform.GetChild(whichChild).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = InputController.Instance._steamMaterial;
        }
    }

    void CalculateIndex()
    {
        for (int i = 0; i < CubeCollect.Instance.Cubes.Count; i++)
        {
            var index = CubeCollect.Instance.Cubes.IndexOf(CubeCollect.Instance.Cubes[i].gameObject);

            CubeCollect.Instance.Cubes[i].transform.localPosition = new Vector3(CubeCollect.Instance.Cubes[i].transform.localPosition.x, CubeCollect.Instance.Cubes[i].transform.localPosition.y, _allCharacter.transform.GetChild(0).localPosition.z + index);
        }

    }
}
