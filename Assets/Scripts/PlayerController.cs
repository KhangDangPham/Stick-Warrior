using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedForce = 5;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    float movementSpeed;

    bool onLand = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed = 0;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onLand)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        // Right movement
        if (Input.GetKey(KeyCode.D))
        {
            movementSpeed = speedForce;
        }

        // Left movement
        if (Input.GetKey(KeyCode.A))
        {
            movementSpeed = -speedForce;
        }

        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    void OnTriggerEnter2D()
    {
        onLand = true;
    }

    void OnTriggerExit2D()
    {
        onLand = false;
    }
}
