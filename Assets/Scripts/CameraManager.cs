using System.Collections;
using System.Collections.Generic;
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
    }
    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }

}
