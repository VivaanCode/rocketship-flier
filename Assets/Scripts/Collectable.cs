using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float minSpeed = 150f;
    public float maxSpeed = 200f;
    public float maxSpinSpeed = 10f;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        Vector2 randomDirection = Random.insideUnitCircle;

        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
