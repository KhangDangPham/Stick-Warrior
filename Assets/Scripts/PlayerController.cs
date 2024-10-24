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
        float hDirection = Input.GetAxis("Horizontal");
        movementSpeed = 0;

        // Right movement
        if (hDirection > 0)
        {
            movementSpeed = speedForce;
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("Running", true);
        }

        // Left movement
        else if (hDirection < 0)
        {
            movementSpeed = -speedForce;
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("Running", true);
        }

        else
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Running", false);
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Running", false);
            if (onLand)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                anim.SetBool("Jumping", true);
            } else
            {
                anim.SetBool("Jumping", false);
            }
        }

        rb.linearVelocity = new Vector2(movementSpeed, rb.linearVelocity.y);
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