using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public Rigidbody rb;
    public Vector2 input;
    public int maxSpeed;
    public float speedIncreaseRate;
    public float horizontalSpeedIncreaseRate;
    public int maxHorizontalSpeed;
    public float breakRate=1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void FixedUpdate()
    {
        Vector3 rbVelocity = rb.velocity;
        rbVelocity.y = 0;

        if (rbVelocity.x * input.x < 0)
            rbVelocity.x += input.x * horizontalSpeedIncreaseRate * 2f;
        else
            rbVelocity.x += input.x * horizontalSpeedIncreaseRate;

        if (input.x == 0f)
            rbVelocity.x *= 0.80f;

        switch (input.y)
        {
            case > 0f:
                rbVelocity.z += input.y * speedIncreaseRate;
                break;
            case < 0f:
                rbVelocity.z *= 0.995f;
                rbVelocity.z += input.y * breakRate;
                break;
            default:
                rbVelocity.z *= 0.999f;
                break;
        }
        rbVelocity.z = Mathf.Clamp(rbVelocity.z, 10f, maxSpeed);


        float mXVelo = maxHorizontalSpeed * Mathf.Clamp(rbVelocity.z / maxSpeed, 0f, 1f) + 5.5f;
        rbVelocity.x = Mathf.Clamp(rbVelocity.x, -mXVelo, mXVelo);

        rb.velocity = rbVelocity;
    }
}