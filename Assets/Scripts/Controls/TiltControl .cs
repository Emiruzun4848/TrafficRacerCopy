using UnityEngine;
using UnityEngine.InputSystem;

public class TiltControl : MonoBehaviour
{
    public float tiltValue = 0f; // -1, 0, 1 değerlerini alacak
    public float b = 0f;
    public void BreakClick(bool y)
    {
        b = y ? -1 : 1;
    }

    void Update()
    {
        if (Accelerometer.current != null) // Cihazda jiroskop varsa
        {
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
            float tiltAngle = Mathf.Atan2(acceleration.x, -acceleration.z) * Mathf.Rad2Deg;

            tiltValue = Mathf.Clamp(tiltAngle / 45, -45f, 45f);

            PlayerMovement.Instance.input = new Vector2(tiltValue, b);
        }
        else
        {
            Debug.Log("Yokk!!! ");

        }
    }
    private void OnEnable()
    {
        if(InputManager.Instance != null)
        InputManager.Instance.gameObject.SetActive(false);
    }
    void OnDisable()
    {
        if(InputManager.Instance != null)
        InputManager.Instance.gameObject.SetActive(true);
        PlayerMovement.Instance.input = Vector2.zero;
    }

}