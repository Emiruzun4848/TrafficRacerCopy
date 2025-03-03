using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CarAIManager : MonoBehaviour
{
    public static CarAIManager Instance;
    public uint carCount = 10;
    public uint maxActiveCar = 4;

    public List<GameObject> carList = new List<GameObject>();
    public List<GameObject> passiveCars = new List<GameObject>();
    public Transform[] SpawnPoints = new Transform[4];


    private List<GameObject> carAiList = new List<GameObject>();

    private PlayerCar playerCar;
    int place;
    private void Awake()
    {

        #region Singleton 
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError("Two CarAIManager script cannot be in same scene.");
            Destroy(gameObject);
        }
        #endregion


        #region Creating Car
        GameObject temp;
        do
        {

            //temp = new GameObject($"Bot Car {carAiList.Count + 1}");
            temp = Instantiate(carList[Random.Range(0, carList.Count)], Vector3.zero, Quaternion.identity);
            temp.name = $"Bot Car {carAiList.Count + 1}";
            temp.SetActive(false);
            temp.AddComponent<CarAI>();
            temp.transform.SetParent(transform);
            carAiList.Add(temp);
            passiveCars.Add(temp);
        } while (carAiList.Count < carCount);


        #endregion

    }

    private void Start()
    {
        playerCar = GameObject.FindAnyObjectByType<PlayerCar>();
        place = 150;
        UpdateCars();
    }
    private void Update()
    {
        foreach (GameObject item in carAiList)
        {
            if (item.activeInHierarchy)
            {
                if ((item.transform.position.z + 100) < playerCar.transform.position.z || item.transform.position.z > playerCar.transform.position.z + 1000)
                {
                    item.SetActive(false);
                }
            }
        }
        UpdateCars();
    }

    public void UpdateCars()
    {

        CarAI tempAI;
        maxActiveCar = carAiList.Count < maxActiveCar ? (uint)carAiList.Count : maxActiveCar;
        while (carAiList.Count - passiveCars.Count < maxActiveCar)
        {
            tempAI = passiveCars[0].GetComponent<CarAI>();
            tempAI.baseSpeed = Random.Range(30, 80);
            Vector3 pos = SpawnPoints[Random.Range(0, 4)].position + (Vector3.forward * (playerCar.transform.position.z + Random.Range(place, place + 300)));
            if (Physics.BoxCast(pos, Vector3.one, Vector3.up, out RaycastHit hit, passiveCars[0].transform.rotation, passiveCars[0].transform.localScale.y))
            {
                {
                    if (hit.transform.CompareTag("NPC"))
                    {
                        continue;
                    }
                }
            }
            passiveCars[0].transform.position = pos;
            passiveCars[0].SetActive(true);
            passiveCars.Remove(passiveCars[0]);

        }
    }
    public void ChangeStateCars(bool _state)
    {
        Rigidbody rb;
        foreach (GameObject item in carAiList)
        {
            rb=item.GetComponent<Rigidbody>();
            rb.isKinematic = _state;
        }
    }
}