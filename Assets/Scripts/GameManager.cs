using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ad")]
    public bool isAds = false;

    [Header("Materials")]
    public Material boxMaterial;
    public Material coverMaterial;
    public Material steamMaterial;
    public Material blackMaterial;

    [Header("TapToStart")]
    [SerializeField] private GameObject _tapToStart;


    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _tapToStart = GameObject.Find("TapToPlay");
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
