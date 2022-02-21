using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeCollect : MonoBehaviour
{
    public static CubeCollect Instance;
    public List<GameObject> Cubes = new List<GameObject>();
    public Transform _boxContainerPrefab;
    public List<GameObject> _boxContainer;
    private float delay = 0.25f;
    [SerializeField] private float _playerSpeed;

    public List<GameObject> boxes = new List<GameObject>();


    private void Awake()
    {
        Cubes.Add(gameObject.transform.GetChild(0).gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _boxContainerPrefab.transform.childCount; i++)
        {
            _boxContainer.Add(_boxContainerPrefab.transform.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        MoveListElements();
        MoveOrigin();

        transform.Translate(0f, 0f, _playerSpeed * Time.deltaTime);

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

    public IEnumerator MakeObjectsBigger()
    {
        for (int i = Cubes.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);
            Cubes[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            Cubes[index].transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));
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

            if (InputController.Instance.isMoved)
            {
                if (index == 1)
                    Cubes[index].transform.DOLocalMove(pos, 0);

            }
            else
                Cubes[index].transform.DOLocalMove(pos, 0.3f);

        }
    }

}
