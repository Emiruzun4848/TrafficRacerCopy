using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private CarData carData;
    public PlayerMovement playerMovement;
    public GameObject pointUpgrade;
    public static List<Transform> triggeredCars = new List<Transform>();
    public float pointKatsayisi = 1f;
    public float moneyRate = 1f;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        carData = MyAccount.Instance.SelectedCar;
        UpdateCarPrefab();

    }
    private void Update()
    {
        if (GameManager.Instance.isGameEnded)
            return;
        if (playerMovement.rb.velocity.z >= 60)
        {
            #region PointSystem
            GameManager.Instance.Point += Time.deltaTime * pointKatsayisi * playerMovement.rb.velocity.z * 0.1f;
            #endregion
            GameManager.Instance.Money += Time.deltaTime * playerMovement.rb.velocity.z * moneyRate * 0.003f;
        }
    }
    void UpdateCarPrefab()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        Instantiate(carData.carPrefab, transform.position, carData.carPrefab.transform.rotation, transform);

        playerMovement.maxSpeed = carData.maxSpeed[Mathf.Clamp(carData.UpgradeLevel, 0, carData.maxSpeed.Length - 1)];
        playerMovement.speedIncreaseRate = carData.speedIncreaseRate[Mathf.Clamp(carData.UpgradeLevel, 0, carData.speedIncreaseRate.Length - 1)];
        playerMovement.horizontalSpeedIncreaseRate = carData.horizontalSpeedIncreaseRate[Mathf.Clamp(carData.UpgradeLevel, 0, carData.horizontalSpeedIncreaseRate.Length - 1)];
        playerMovement.maxHorizontalSpeed = carData.maxHorizontalSpeed[Mathf.Clamp(carData.UpgradeLevel, 0, carData.maxHorizontalSpeed.Length - 1)];
        playerMovement.breakRate = carData.breakRate[Mathf.Clamp(carData.UpgradeLevel, 0, carData.breakRate.Length - 1)];

    }
    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isGameEnded)
            return;
        if (triggeredCars.Contains(other.transform) || playerMovement.rb.velocity.z < 100f)
            return;
        triggeredCars.Add(other.transform);
        GameManager.Instance.Point += 125f;
        GameManager.Instance.Money += 25f;
        Vector3 iPos = (other.transform.position + transform.position) / 2;
        iPos.y = 10f;
        GameObject iDelete = Instantiate(pointUpgrade, iPos, pointUpgrade.transform.rotation);
        Destroy(iDelete, 0.5f);
    }
    void OnCollisionEnter(Collision col)
    {
        if (GameManager.Instance.isGameEnded)
            return;
        Debug.Log($"{col.gameObject.name} Çarptı. {playerMovement.rbVelocity.z}");
        if (col.gameObject.CompareTag("NPC"))
        {
            col.gameObject.GetComponent<CarAI>().Crash();
            if (playerMovement.rbVelocity.z > 90f)
            {
                GameManager.Instance.GameOver();
            }



        }
    }

}
