using UnityEngine;

public class CarAI : MonoBehaviour
{
    public float baseSpeed = 0;
    float speed = 0;
    private CarAIManager manager = CarAIManager.Instance;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        speed = baseSpeed;
    }
    private void FixedUpdate()
    {
        speed = Mathf.Clamp(((speed / baseSpeed) + 0.005f) * baseSpeed, 0, baseSpeed);
        rb.velocity = Vector3.forward * speed;
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 tras = transform.position;
        tras.y = 3;
        tras.z += 3;
        if (Physics.Raycast(tras, Vector3.forward, out hit, 20f))
        {
            GameObject obj = hit.collider.gameObject;

            if (obj.CompareTag("NPC"))
            {

                while (!obj.GetComponent<CarAI>())
                {
                    obj = obj.transform.parent.gameObject;
                }

            }
            if (obj.GetComponent<CarAI>())
            {
                float aBaseSpeed = obj.GetComponent<CarAI>().baseSpeed;
                if (aBaseSpeed < baseSpeed && obj.transform.position.z > transform.position.z)
                {
                    obj.GetComponent<CarAI>().baseSpeed = baseSpeed;
                    baseSpeed = aBaseSpeed;
                }
                if (obj.transform.position.z > transform.position.z)
                {
                    speed = Mathf.Clamp(((speed / baseSpeed) - (0.02f * Time.deltaTime * 50f)) * baseSpeed, 0, baseSpeed);
                }
            }
            else if (obj.GetComponent<PlayerCar>() && !GameManager.Instance.isGameEnded)
            {
                baseSpeed = obj.GetComponent<PlayerMovement>().rb.velocity.z;
            }
        }

        Debug.DrawRay(tras, Vector3.forward * 20, Color.red);
    }
    void OnCollisionStay(Collision collision)
    {

        GameObject obj = collision.gameObject;
        if (!obj.CompareTag("NPC"))
            return;
        if (obj.name == "Collider")
            obj = obj.transform.parent.parent.gameObject;
        else if (obj.name == "Body")
            obj = obj.transform.parent.gameObject;
        if (obj.transform.position.z > transform.position.z)
        {
            transform.position -= Vector3.forward * 1f;
        }
    }
    private void OnDisable()
    {
        if (!manager.passiveCars.Contains(gameObject))
        {
            manager.passiveCars.Add(gameObject);
            PlayerCar.triggeredCars.Remove(transform);
        }
    }
    private void OnEnable()
    {
        Component[] cols = GetComponents<Collider>();
        foreach (Collider item in cols)
        {
            item.enabled = true;
        }
        foreach (Transform item in transform)
        {
        item.gameObject.SetActive(true);
        }
    }
    public void Crash()
    {
        Invoke(nameof(Crashed), 0.3f);
    }
    void Crashed()
    {
        baseSpeed = 5f;
        Component[] cols = GetComponents<Collider>();
        foreach (Collider item in cols)
        {
            item.enabled = false;
        }
        int number =6;
        for (int i = 0; i < number; i++)
        {
            Invoke(nameof(CloseOpen), 0.2f * i);
        }
        Invoke(nameof(Disabled), 0.2f * number);
    }
    void CloseOpen()
    {
        foreach (Transform item in transform)
        {
        item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
    }
    void Disabled()
    {
        gameObject.SetActive(false);
    }

}