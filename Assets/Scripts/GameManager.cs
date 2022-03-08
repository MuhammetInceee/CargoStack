using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Materials")]

    public Material steamMaterial;


    [Header("Objects")]
    [SerializeField] private GameObject _tapToStart;
    public GameObject _LevelEnded;

    [Header("Animators")]
    public Animator _carAnim;


    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _tapToStart = GameObject.Find("TapToPlay");
        
        _carAnim = GameObject.Find("CargoCar_End").gameObject.GetComponent<Animator>();


    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (_tapToStart.activeInHierarchy)
        {
            InputController.Instance.isGameEnd = true;
        }
        else
            InputController.Instance.isGameEnd = false;


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _tapToStart.SetActive(false);
            }
        }
    }
}
