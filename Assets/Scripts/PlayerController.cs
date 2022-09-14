using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speedForce = 5;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public Animator anim;
    float movementSpeed;

    bool onLand = true;

    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("Running", true);
        }

        // Left movement
        else if (Input.GetKey(KeyCode.A))
        {
            movementSpeed = -speedForce;
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("Running", true);
        }

        else
        {
            anim.SetBool("Running", false);
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
