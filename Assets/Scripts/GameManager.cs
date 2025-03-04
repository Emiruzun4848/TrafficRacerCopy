using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameOverScene gameOverScene;

    public TMP_Text pointText;
    public TMP_Text speedText;
    public TMP_Text moneyText;
    private float point = 0;
    private float money = 0;

    public Rigidbody playerRb;
    public bool isGameEnded;
    [SerializeField] Transform inputs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError("Two GameManager script cannot be in same scene.");
            Destroy(gameObject);
        }
        Money += MyAccount.Instance.Money;
        LoadInputSystem(MyAccount.Instance.SelectedInputType);
    }
    void LoadInputSystem(InputType type)
    {
        if (type == InputType.Keyboard)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            MyAccount.Instance.SelectedInputType = InputType.Button;
            LoadInputSystem(InputType.Button);
            return;
            #else
            inputs.GetChild(0).gameObject.SetActive(false);
            inputs.GetChild(1).gameObject.SetActive(false);
            inputs.GetChild(2).gameObject.SetActive(false);
            #endif
        }
        else if (type == InputType.JoyStick)
        {
            inputs.GetChild(0).gameObject.SetActive(true);
            inputs.GetChild(1).gameObject.SetActive(false);
            inputs.GetChild(2).gameObject.SetActive(false);
        }
        else if (type == InputType.Button)
        {
            inputs.GetChild(0).gameObject.SetActive(false);
            inputs.GetChild(1).gameObject.SetActive(true);
            inputs.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            inputs.GetChild(0).gameObject.SetActive(false);
            inputs.GetChild(1).gameObject.SetActive(false);
            inputs.GetChild(2).gameObject.SetActive(true);

            #else
            MyAccount.Instance.SelectedInputType = InputType.Keyboard;
            LoadInputSystem(InputType.Keyboard);
            return;
            #endif
        }
    }
    private void Start()
    {
        playerRb = GameObject.FindAnyObjectByType<PlayerMovement>().rb;
    }

    public float Point
    {
        get { return point; }
        set
        {
            point = value;
            pointText.text = $"Score : {(int)point}";
        }
    }
    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            moneyText.text = $"${money.ToString("N0", new System.Globalization.CultureInfo("de-DE"))}";

        }
    }

    private void Update()
    {
        if (isGameEnded)
            return;
        speedText.text = $"{((int)playerRb.velocity.z).ToString()} KM/H";
    }
    public void GameOver()
    {
        isGameEnded = true;
        PlayerMovement.Instance.CrashedCar();
        if ((int)point > MyAccount.Instance.HighScore)
        {
            MyAccount.Instance.HighScore = (int)point;
        }
        MyAccount.Instance.Money = (int)Money;
        SetInf.Instance.Set();
        
        gameOverScene.Open();
        gameOverScene.LoadText($"${money.ToString("N0", new System.Globalization.CultureInfo("de-DE"))}", ((int)point).ToString(), MyAccount.Instance.HighScore.ToString());

    }
}
