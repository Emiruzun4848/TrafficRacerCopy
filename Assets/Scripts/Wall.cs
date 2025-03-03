using UnityEngine;

public class Wall : MonoBehaviour
{
    public Transform playerTransform;
    private void Update()
    {
        transform.position = Vector3.zero + (Vector3.forward * playerTransform.position.z);
    }
}
