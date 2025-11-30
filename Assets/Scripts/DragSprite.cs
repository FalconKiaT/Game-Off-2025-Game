using UnityEngine;

public class DragSprite : MonoBehaviour
{
    public float speed = 5f; // Movement speed in units per second

    void Update()
    {
        // Get input from arrow keys (or WASD automatically)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Build movement vector
        Vector3 movement = new Vector3(moveX, moveY, 0f).normalized;

        // Apply movement
        transform.position += movement * speed * Time.deltaTime;
    }
}