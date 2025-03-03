using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;

    private void Awake()
    {
        playerTransform = GameObject.FindAnyObjectByType<PlayerCar>().transform;
    }
    private void Start()
    {
        offset = transform.position - playerTransform.position;
        offset.x = 0;
    }
    private void LateUpdate()
    {
        transform.position = (Vector3.forward * playerTransform.position.z) + offset;
    }
}
