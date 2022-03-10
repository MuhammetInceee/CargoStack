using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Access")]

    [SerializeField]
    private GameObject _score;
    private GameScoreManager _scoreManager;

    [SerializeField] private GlobalScoreSettings globalScore;


    [Header("Materials")]
    public Material steamMaterial;


    [Header("Animators")]
    public Animator _carAnim;

    [Header("UI")]

    public GameObject _LevelEnded;

    [SerializeField]
    private GameObject _tapToStart;

    [SerializeField]
    private GameObject _playerCoinCanvas;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private Text finalScreenCoin;

    [SerializeField]
    private Text GlobalCoin;




    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _tapToStart = GameObject.Find("TapToPlay");
        
        _carAnim = GameObject.Find("CargoCar_End").gameObject.GetComponent<Animator>();

        _score = GameObject.Find("ScoreManager");
        _scoreManager = _score.GetComponent<GameScoreManager>();

        
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        GameStart();
        UI();
    }

    void GameStart()
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
                _playerCoinCanvas.SetActive(true);
            }
        }
    }

    void UI()
    {
        coinText.text = _scoreManager.Score.ToString();
        finalScreenCoin.text = _scoreManager.Score.ToString();
        GlobalCoin.text = globalScore.GLOBALCOIN.ToString();
    }
}
