using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateWhell : MonoBehaviour
{
    [SerializeField] Transform[] target = new Transform[4];
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float speed = 0f;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        if (GameManager.Instance.isGameEnded)
            return;
        speed = Mathf.Clamp((PlayerMovement.Instance.rb.velocity.z / 100) + 0.1f, 0, maxSpeed);
        foreach (Transform item in target)
        {
            item.Rotate(Vector3.right * Time.deltaTime * -1000 * speed);

        }
    }
}