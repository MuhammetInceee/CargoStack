using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    [SerializeField] private GameObject _allCharacter;

    private void Start()
    {
        if (_allCharacter == null)
            _allCharacter = GameObject.Find("AllCharacters");
    }

    private void Update()
    {
        CalculateIndex();
    }


    private void OnTriggerExit(Collider other)
    {
        //GetChild(0) = Empty Box
        //GetChild(1) = Full Box
        //GetChild(2) = Close Box
        //GetChild(3) = Packed Box

        if (other.gameObject.CompareTag("Closer"))
        {
            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;

            BoxCloser(0, 2);
            BoxDoNothing(2, 1);
            BoxActiver(2);
            EffectActiver(other);
            BoingEffect();
        }

        else if (other.gameObject.CompareTag("Filler"))
        {
            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;

            BoxCloser(0, 1);
            if (!gameObject.transform.GetChild(2).gameObject.activeInHierarchy && !gameObject.transform.GetChild(3).gameObject.activeInHierarchy)
                BoxActiver(1);
            BoxDoNothing(2, 2);
            BoingEffect();
        }

        else if (other.gameObject.CompareTag("Packer"))
        {
            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;

            BoxCloser(0, 3);
            BoxActiver(3);
            BoingEffect();

        }



        else if (other.gameObject.CompareTag("Burner"))
        {

            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;

            TextureChanger();
            EffectActiver(other);
            BoingEffect();
        }

        else if (other.gameObject.CompareTag("Sell"))
        {
            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;


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
                return;

            if (CubeCollect.Instance.Cubes.Contains(gameObject))
            {
                Destroy(gameObject);
                CubeCollect.Instance.Cubes.Remove(gameObject);
            }

            EffectActiver(other);

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
            // If Player Collide, Do Nothing
            if (gameObject.name == "Player")
                return;

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
                {
                    gameObject.transform.GetChild(i).transform.GetChild(k).gameObject.GetComponent<MeshRenderer>().material = GameManager.Instance.steamMaterial;
                }
            }

            gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = GameManager.Instance.steamMaterial;
        }
    }

    //void Destroyer_Hand(Collider other)
    //{
    //    if (CubeCollect.Instance.Cubes.Contains(gameObject))
    //    {
    //        var index = CubeCollect.Instance.Cubes.IndexOf(gameObject);

    //        for (int i = 0; i < CubeCollect.Instance.Cubes.Count; i++)
    //        {
    //            if (CubeCollect.Instance.Cubes.IndexOf(CubeCollect.Instance.Cubes[i]) > index)
    //            {

    //                float randomX = Random.Range(-2.2f, 3.2f);
    //                float randomZ = Random.Range(5, 12);

    //                if (gameObject.CompareTag("Collected"))
    //                {
    //                    GameObject Cubei = CubeCollect.Instance.Cubes[i];
    //                    Cubei.gameObject.tag = "Collectable";

    //                    Cubei.gameObject.transform.DOLocalMove(new Vector3(transform.localPosition.x + randomX, 0, transform.localPosition.z + randomZ), 1f);
    //                    // other yerine modelin içine bir tane yer yap ona ata fonsionun içine attýðýn collider ý kaldýr
    //                    Cubei.gameObject.transform.parent = other.transform;
    //                    Cubei.transform.localPosition = new Vector3(0, 2, 0);

    //                    Cubei.gameObject.GetComponent<Rigidbody>().useGravity = true;
    //                    Cubei.gameObject.GetComponent<Rigidbody>().isKinematic = false;

    //                    Cubei.gameObject.GetComponent<Collider>().isTrigger = false;
    //                    CubeCollect.Instance.Cubes.RemoveAt(i);

    //                }
    //            }
    //        }

    //        CubeCollect.Instance.Cubes.Remove(gameObject);
    //        Destroy(gameObject);
    //    }
    //}

    void EffectActiver(Collider other)
    {
        if (!other.gameObject.transform.parent.transform.GetChild(0).gameObject.activeInHierarchy)
            other.gameObject.transform.parent.transform.GetChild(0).gameObject.SetActive(true);
    }

    void BoingEffect()
    {
        // When object pass through to gates just one time boing effect 
        if (gameObject == CubeCollect.Instance.Cubes[CubeCollect.Instance.Cubes.Count - 1].gameObject)
        {
            StartCoroutine(CubeCollect.Instance.MakeObjectsBigger());
        }
    }

    void BoxCloser(int box, int howmanytimerun)
    {
        for (int i = box; i < box + howmanytimerun; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void BoxDoNothing(int box, int howmanytimerun)
    {
        for (int i = box; i < box + howmanytimerun; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
                return;
        }
    }

    void BoxActiver(int box)
    {
        gameObject.transform.GetChild(box).gameObject.SetActive(true);
    }
}
