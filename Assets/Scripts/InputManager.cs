using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance;
    public InputsSystem playerInput;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.Log("2 den fazla InputManger var!");
            return;
        }
        Instance = this;
        playerInput = new InputsSystem();

        playerInput.PlayerMovement.Enable();
    }
    void Start()
    {
        AssignPlayerMovement();
    }
    void AssignPlayerMovement()
    {
        #region PlayerMovement
        playerInput.PlayerMovement.Movement.started += ctx => PlayerMovement.Instance.input = ctx.ReadValue<Vector2>();
        playerInput.PlayerMovement.Movement.performed += ctx => PlayerMovement.Instance.input = ctx.ReadValue<Vector2>();
        playerInput.PlayerMovement.Movement.canceled += ctx => PlayerMovement.Instance.input = ctx.ReadValue<Vector2>();
        #endregion
    }


    #region Enabled - Disabled
    private void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.PlayerMovement.Enable();
            playerInput.Enable();
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.PlayerMovement.Disable();
            playerInput.Disable();
        }
    }
    private void OnDestroy()
    {
        {
            if (playerInput != null)
            {
                playerInput.PlayerMovement.Disable();
                playerInput.Disable();
            }
        }
    }
    #endregion


}