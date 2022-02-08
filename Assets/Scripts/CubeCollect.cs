using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeCollect : MonoBehaviour
{
    public static CubeCollect Instance;
    public List<GameObject> Cubes = new List<GameObject>();
    private float delay = 0.25f;

    

    private void Awake()
    {
        Cubes.Add(gameObject.transform.GetChild(0).gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (InputController.instance.zposition!=0)
        {
            MoveListElements();
        }
        else
        {
            MoveOrigin();
        }
        transform.Translate(0f, 0f, 6 * Time.deltaTime);
       
    }
   
    public void StackCube(GameObject cube, int index)
    {
        Cubes.Add(cube);
        cube.transform.parent = transform;
        Vector3 NewPosition = Cubes[index].transform.localPosition;
        NewPosition.z += 1f;
        cube.transform.localPosition = NewPosition;
        StartCoroutine(MakeObjectsBigger());


    }

    private IEnumerator MakeObjectsBigger()
    {
        Debug.Log("dwadawdaw");
        for (int i = Cubes.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);
            Cubes[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            Cubes[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.05f);

        }
    }
    public void MoveListElements()
    {
        for (int i = 1; i < Cubes.Count; i++)
        {
           
            Vector3 pos = Cubes[i].transform.localPosition;
            pos.x = Cubes[i - 1].transform.localPosition.x;
            Cubes[i].transform.DOLocalMove(pos, delay);
            //Cubes[i].transform.position = Vector3.Lerp(Cubes[i].transform.position, pos, delay * Time.deltaTime);
        }
    }
    public void MoveOrigin()
    {
        for (int i = 1; i < Cubes.Count; i++)
        {
            int index = i;
            Vector3 pos = Cubes[index].transform.localPosition;
            pos.x = Cubes[0].transform.position.x;
            Cubes[index].transform.DOLocalMove(pos, 0.70f);
        }
    }


}
