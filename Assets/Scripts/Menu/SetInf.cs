using System;
using UnityEngine;

public class SetInf : MonoBehaviour
{
    public static SetInf Instance;

    MyAccount myAccount;

    public CarData[] cars;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        myAccount = MyAccount.Instance;
    }
    public void Set()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            PlayerPrefs.SetInt($"TrafficHillerCarUpgradeLevel{i}", cars[i].UpgradeLevel);
            PlayerPrefs.SetInt($"TrafficHillerCarIsBought{i}", (cars[i].isBought ? 1 : 0));
        }

        PlayerPrefs.SetInt("TrafficHillerMoney", myAccount.Money);
        PlayerPrefs.SetInt("TrafficHillerScore", myAccount.HighScore);
        int  selectedCarIndex  =Array.IndexOf(cars,myAccount.SelectedCar);
        PlayerPrefs.SetInt("TrafficHillerSelectedCar",selectedCarIndex  ==-1?0:selectedCarIndex );
        PlayerPrefs.SetInt("TrafficHillerInputType",(int)myAccount.SelectedInputType);
        PlayerPrefs.SetFloat("TrafficHillerCarFloat",myAccount.carVolume);

        PlayerPrefs.Save();
    }
}