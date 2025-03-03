using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "Game/CarData", order = 0)]
public class CarData : ScriptableObject
{
    [Space(8)]
    public long cost = 100000;
    public bool isBought = false;
    [Space(8)]
    [Header("Attribute")]
    public int[] maxSpeed;
    [Space(15)]
    public float[] speedIncreaseRate;

    [Space(15)]
    public int[] maxHorizontalSpeed;
    [Space(15)]
    public float[] horizontalSpeedIncreaseRate;
    [Space(15)]
    public float[] breakRate;
    [Space(15)]
    public int UpgradeLevel;
    public int[] UpgradeCost;
    [Space(8)]
    public GameObject carPrefab;
}