using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves.
    public float speed = 0;

    // Rigidbody of the player.
    private Rigidbody rb;

    // Score count and display.
    private int count;
    public TextMeshProUGUI countText;

    // Win text.
    public GameObject winTextObject;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Initialize score to 0.
        count = 0;

        // Display score text.
        SetCountText();

        // Hide win text at start.
        winTextObject.SetActive(false);

    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the score.
            count = count + 1;

            // Update score text.
            SetCountText();
        }
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Convert the input value into a Vector2 for movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // This functions updates the score text.
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // Display win text when all items are picked up.
        if (count >= 8)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}
