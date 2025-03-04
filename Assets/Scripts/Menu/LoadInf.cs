using UnityEngine;

public class LoadInf : MonoBehaviour
{
    MyAccount myAccount;
    public CarData[] cars;
    private void Awake()
    {
        myAccount = MyAccount.Instance;
        load();

    }
    void load()
    {

        cars[0].UpgradeLevel = PlayerPrefs.GetInt($"TrafficHillerCarUpgradeLevel{0}", 0);
        cars[0].isBought = PlayerPrefs.GetInt($"TrafficHillerCarIsBought{0}", 1) != 0;
        for (int i = 1; i < cars.Length; i++)
        {
            cars[i].UpgradeLevel = PlayerPrefs.GetInt($"TrafficHillerCarUpgradeLevel{i}", 0);
            cars[i].isBought = PlayerPrefs.GetInt($"TrafficHillerCarIsBought{i}", 0) != 0;
        }

        myAccount.Money = PlayerPrefs.GetInt("TrafficHillerMoney", 500);
        myAccount.HighScore = PlayerPrefs.GetInt("TrafficHillerScore", 0);
        myAccount.SelectedCar = cars[PlayerPrefs.GetInt("TrafficHillerSelectedCar", 0)];
        myAccount.SelectedInputType = (InputType)PlayerPrefs.GetInt("TrafficHillerInputType", 0);
        myAccount.carVolume = PlayerPrefs.GetFloat("TrafficHillerCarFloat", 0.5f);
    }

}

