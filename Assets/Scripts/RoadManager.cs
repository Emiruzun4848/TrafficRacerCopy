using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public int roadCount;
    public GameObject Road;
    public Transform playerTranform;
    int lastPos;
    [SerializeField] private List<Transform> roads = new List<Transform>();

    float PlayerPos
    {
        set
        {
            int a = RoundUpToNextHundred(value);
            if (a > lastPos)
            {
                lastPos = a;
                Transform selectedRoad = roads[0];
                Vector3 newpos = roads[roads.Count - 1].position + Vector3.forward * 100;
                roads.Remove(selectedRoad);
                selectedRoad.position = newpos;
                roads.Add(selectedRoad);
            }
        }
    }
    private void Awake()
    {
        CreateRoad();
    }
    private void Update()
    {
        PlayerPos = playerTranform.position.z;
    }
    private void CreateRoad()
    {
        for (int i = -3; i <= roadCount-4; i++)
        {
            Transform temp;
            Vector3 pos = new Vector3(0, -0.2f, i * 100);
            temp = GameObject.Instantiate(Road, pos, Quaternion.identity, transform).transform;
            roads.Add(temp);
        }
    }
    int RoundUpToNextHundred(float number)
    {
        return Mathf.CeilToInt(number / 100f) * 100;
    }
}
