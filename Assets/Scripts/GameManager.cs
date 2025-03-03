using System;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
            inputs.GetChild(0).gameObject.SetActive(false);
            inputs.GetChild(1).gameObject.SetActive(false);
        }
        else if (type == InputType.JoyStick)
        {
            inputs.GetChild(0).gameObject.SetActive(true);
            inputs.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            inputs.GetChild(0).gameObject.SetActive(false);
            inputs.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void Start()
    {
        playerRb = GameObject.FindAnyObjectByType<PlayerMovement>().rb;
        InvokeRepeating(nameof(SetRepeatMoney), 1f, 3f);
    }

    public void SetRepeatMoney()
    {
        if (!isGameEnded)
        {
            MyAccount.Instance.Money = (int)Money;
        }
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
        if(isGameEnded)
            return;
        speedText.text = $"{((int)playerRb.velocity.z).ToString()} KM/H";
    }
    public void GameOver()
    {
        MyAccount.Instance.Money = (int)Money;
        isGameEnded = true;
        PlayerMovement.Instance.CrashedCar();

    }
}
