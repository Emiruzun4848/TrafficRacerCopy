using UnityEngine;

public class PlayerCar : MonoBehaviour
{

    bool carCrash;
    Rigidbody rb;
    Vector2 input;
    public Vector3 rbVelocity;
    public int maxSpeed;
    public float speedIncreaseRate;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        carCrash = false;
    }

    void Update()
    {
        if (carCrash)
            return;
        input.x = Input.GetAxisRaw("Horizontal") * speedIncreaseRate * 5;
        input.y = Input.GetAxis("Vertical") * Mathf.Pow(speedIncreaseRate+1, 2)  - 1f;
    }
    private void FixedUpdate()
    {
        Vector3 vel = rb.velocity;
        vel.x += input.x;
        vel.z += input.y;
        if (vel.magnitude > maxSpeed)
        {
            vel = vel.normalized * maxSpeed;
        }
        vel.z = Mathf.Clamp(vel.z, 0, Mathf.Infinity);
        vel.x = Mathf.Lerp(vel.x, 0, 0.2f);
        rb.velocity = vel;
        rbVelocity = rb.velocity;
    }
}
