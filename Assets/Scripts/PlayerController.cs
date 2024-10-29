using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedForce = 5.0f;
    public float jumpForce = 10f;
    public Rigidbody2D rigidBody;
    public Animator anim;
    bool onLand;
    float movementSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        // Right movement
        if (hDirection > 0)
        {
            PlayerMove(speedForce, 1);
        }

        // Left movement
        else if (hDirection < 0)
        {
            PlayerMove(-speedForce, -1);
        }

        else if (hDirection == 0) {
            PlayerStop();
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && onLand)
        {
            PlayerJump();
        }

        // Falling
        PlayerFall();
        
        anim.SetBool("Onland", onLand);
    }

    void PlayerMove(float speed, int direction)
    {
        movementSpeed = speed;
        transform.localScale = new Vector2(direction, 1);
        anim.SetBool("Running", true);
        rigidBody.linearVelocity = new Vector2(movementSpeed, rigidBody.linearVelocity.y);
    }
    void PlayerStop()
    {
        movementSpeed = 0;
        anim.SetBool("Running", false);
        rigidBody.linearVelocity = new Vector2(movementSpeed, rigidBody.linearVelocity.y);
    }

    void PlayerJump()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        anim.SetTrigger("Jumping");
        onLand = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onLand = true;
        }
    }

    void PlayerFall()
    {
        anim.SetFloat("FallingBound", rigidBody.linearVelocity.y);
    }
}