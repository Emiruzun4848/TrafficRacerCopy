using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//using UnityEngine.UIElements;

public class StoreMenu : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] RectTransform selectedCar;
    [SerializeField] float rotateSpeed = 1f;

    public CarData[] cars;
    int index = 0;

    [Header("Information")]
    public TextMeshProUGUI moneyText;
    [Header("Stats")]
    [SerializeField] Transform carParent;
    [Space(20)]
    [SerializeField] Transform statMaxSpeed;
    [SerializeField] Transform statSpeedIncreaseRate;
    [SerializeField] Transform statBreakeRate;
    [SerializeField] Transform statHorizontalSpeed;
    [SerializeField] Transform statHorizontalSpeedIncreaseRate;
    [Space(20)]
    [SerializeField] Transform statUpgradeCost;
    [Space(20)]
    [SerializeField] Transform select;
    void Awake()
    {
        moneyText.text = $"${MyAccount.Instance.Money.ToString("N0", new System.Globalization.CultureInfo("de-DE"))}";
        index = Array.IndexOf(cars, MyAccount.Instance.SelectedCar);
        Load();
    }

    private void Update()
    {
        RotateSelectedCar();
    }

    void RotateSelectedCar()
    {
        if (selectedCar != null)
            selectedCar.Rotate(Vector3.up * Time.deltaTime * 10 * rotateSpeed);
    }

    void Load()
    {
        foreach (Transform transform in carParent)
        {
            Destroy(transform.gameObject);
        }
        Instantiate(cars[index].carPrefab, carParent.position, carParent.rotation, carParent);

        #region  Stats

        if (!cars[index].isBought)
        {
            loadStats(MyAccount.Instance.SelectedCar, MyAccount.Instance.SelectedCar.UpgradeLevel, 2);
        }
        else
        {
            loadStats(cars[index], cars[index].UpgradeLevel, 2);
        }

        if (cars[index].isBought && cars[index].UpgradeLevel <= cars[index].UpgradeCost.Length - 1)
        {
            loadStats(cars[index], cars[index].UpgradeLevel + 1, 1);
        }
        else
        {
            loadStats(cars[index], cars[index].UpgradeLevel, 1);
        }

        loadStats(MyAccount.Instance.SelectedCar, MyAccount.Instance.SelectedCar.UpgradeLevel, 0);

        #endregion

        select.GetComponent<Button>().onClick.RemoveAllListeners();
        statUpgradeCost.GetComponent<Button>().onClick.RemoveAllListeners();
        foreach (Transform item in select)
        {
            item.gameObject.SetActive(false);
        }
        foreach (Transform item in statUpgradeCost)
        {
            item.gameObject.SetActive(false);
        }
        statUpgradeCost.GetComponent<Button>().interactable = false;
        select.GetComponent<Button>().interactable = true;

        if (cars[index].UpgradeLevel <= cars[index].UpgradeCost.Length - 1)
        {
            if (cars[index] != MyAccount.Instance.SelectedCar)
            {
                statUpgradeCost.GetComponent<Button>().interactable = false;
            }
            statUpgradeCost.GetChild(0).gameObject.SetActive(true);
            statUpgradeCost.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"${cars[index].UpgradeCost[cars[index].UpgradeLevel].ToString("N0", new System.Globalization.CultureInfo("de-DE"))}";
            statUpgradeCost.GetComponent<Button>().interactable = true;
            statUpgradeCost.GetComponent<Button>().onClick.AddListener(() => UpgradeCar(cars[index]));
        }
        else
        {
            statUpgradeCost.GetChild(1).gameObject.SetActive(true);
        }

        if (cars[index].isBought)
        {
            select.GetChild(0).gameObject.SetActive(true);
            if (MyAccount.Instance.SelectedCar == cars[index])
            {
                select.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
                select.GetComponent<Button>().interactable = false;
            }
            else
            {
                select.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Select";
                select.GetComponent<Button>().onClick.AddListener(() =>
                {
                    MyAccount.Instance.SelectedCar = cars[index];
                    Load();
                    return;
                });
            }




        }
        else
        {
            select.GetChild(1).gameObject.SetActive(true);
            select.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"${cars[index].cost.ToString("N0", new System.Globalization.CultureInfo("de-DE"))}";
            select.GetComponent<Button>().onClick.AddListener(() => BuyCar(cars[index]));
            statUpgradeCost.GetComponent<Button>().interactable = false;
        }


    }
    void loadStats(CarData _cardata, int level, int _index)
    {
        if (_cardata == null)
        {
            Debug.LogError("CarData null!");
            return;
        }

        int control;

        if (_cardata.maxSpeed != null && _cardata.maxSpeed.Length > 0)
        {
            control = Mathf.Clamp(level, 0, _cardata.maxSpeed.Length - 1);
            statMaxSpeed.GetChild(_index).GetComponent<Slider>().value = CalculateStats(_cardata.maxSpeed[control], 100f, 1000f);
        }

        if (_cardata.speedIncreaseRate != null && _cardata.speedIncreaseRate.Length > 0)
        {
            control = Mathf.Clamp(level, 0, _cardata.speedIncreaseRate.Length - 1);
            statSpeedIncreaseRate.GetChild(_index).GetComponent<Slider>().value = CalculateStats(_cardata.speedIncreaseRate[control], 0.1f, 50f);
        }

        if (_cardata.breakRate != null && _cardata.breakRate.Length > 0)
        {
            control = Mathf.Clamp(level, 0, _cardata.breakRate.Length - 1);
            statBreakeRate.GetChild(_index).GetComponent<Slider>().value = CalculateStats(_cardata.breakRate[control], 1f, 50f);
        }

        if (_cardata.maxHorizontalSpeed != null && _cardata.maxHorizontalSpeed.Length > 0)
        {
            control = Mathf.Clamp(level, 0, _cardata.maxHorizontalSpeed.Length - 1);
            statHorizontalSpeed.GetChild(_index).GetComponent<Slider>().value = CalculateStats(_cardata.maxHorizontalSpeed[control], 30f, 120f);
        }

        if (_cardata.horizontalSpeedIncreaseRate != null && _cardata.horizontalSpeedIncreaseRate.Length > 0)
        {
            control = Mathf.Clamp(level, 0, _cardata.horizontalSpeedIncreaseRate.Length - 1);
            statHorizontalSpeedIncreaseRate.GetChild(_index).GetComponent<Slider>().value = CalculateStats(_cardata.horizontalSpeedIncreaseRate[control], 1f, 30f);
        }
    }
    float CalculateStats(float value, float min, float max)
    {
        float result = (value - min) / (max - min);
        return Mathf.Clamp(result, 0f, 1f);

    }
    void UpgradeCar(CarData _carData)
    {
        int a = Mathf.Clamp(_carData.UpgradeLevel, 0, _carData.UpgradeCost.Length - 1);
        if (_carData.UpgradeCost[a] <= MyAccount.Instance.Money)
        {
            MyAccount.Instance.Money -= _carData.UpgradeCost[_carData.UpgradeLevel];
            _carData.UpgradeLevel++;
        }
        UpdateMoney();
        Load();
    }
    public void ChangeCar(bool right)
    {
        if (right)
        {
            index += 1;
            if (index >= cars.Length)
                index = 0;
        }
        else
        {
            index -= 1;
            if (index < 0)
                index = cars.Length - 1;
        }
        Load();
    }
    public void BuyCar(CarData _car)
    {
        if (MyAccount.Instance.Money >= _car.cost)
        {
            MyAccount.Instance.Money -= (int)_car.cost;
            _car.isBought = true;

            Load();
        }
        UpdateMoney();
    }
    void UpdateMoney()
    {
        moneyText.text = $"${MyAccount.Instance.Money.ToString("N0", new System.Globalization.CultureInfo("de-DE"))}";
    }
}
