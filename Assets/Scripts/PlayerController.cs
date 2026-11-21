using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float ThrustForce = 1.7f;
    public float MaxSpeed = 9.5f;
    public GameObject boosterFlame;
    Rigidbody2D rb;

    private Button restartButton;

    public UIDocument uiDocument;

    private Label scoreText;

    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMult = 10f;

    public GameObject explosionEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;

        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {
        
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMult);
        scoreText.text = "Score: " + score;
        //Debug.Log("score: "+ score);

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //Debug.Log("Mouse Position: " + mousePos);
            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;
            rb.AddForce(direction * ThrustForce);

            if (rb.linearVelocity.magnitude > MaxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * MaxSpeed;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                boosterFlame.SetActive(true);
            }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                boosterFlame.SetActive(false);
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
