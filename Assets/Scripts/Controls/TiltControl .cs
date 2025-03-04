using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TiltControl : MonoBehaviour
{
    public float tiltValue = 0f; // -1, 0, 1 değerlerini alacak
    public float b = 1f;
    public void BreakClick(bool y)
    {
        b = y ? -1 : 1;
    }
    InputsSystem accelerationInput;
    private void Awake()
    {
        //accelerationInput = new InputsSystem();
        //accelerationInput.AccelerometerInput.Enable();
        //AssignPlayerMovement();
    }
    void AssignPlayerMovement()
    {
        #region PlayerMovement
        //accelerationInput.AccelerometerInput.Accelerometer.performed += ctx => InputUpdate(ctx.ReadValue<Vector3>());
        #endregion
    }
    void Update()
    {

        float tiltValue = Accelerometer.current.acceleration.ReadValue().x;

        PlayerMovement.Instance.input = new Vector2(tiltValue, b);

    }


    #region Enabled - Disabled
    private void OnEnable()
    {
#if UNITY_ANDROID
        InputSystem.EnableDevice(Accelerometer.current);
#endif
        if (InputManager.Instance != null)
            InputManager.Instance.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
#if UNITY_ANDROID
        InputSystem.DisableDevice(Accelerometer.current);
#endif
        if (InputManager.Instance != null)
            InputManager.Instance.gameObject.SetActive(true);
        PlayerMovement.Instance.input = Vector2.zero;
    }
    private void OnDestroy()
    {
        {
            if (accelerationInput != null)
            {
                accelerationInput.AccelerometerInput.Disable();
                accelerationInput.Disable();
            }
        }
    }
    #endregion
}