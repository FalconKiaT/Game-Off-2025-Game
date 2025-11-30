using System;
using UnityEngine;

public class ReactivateGravity : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            rb.gravityScale = 0.5f;
        }
    }
}
