using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedForce = 2.5f;
    public float jumpForce = 10f;
    public Rigidbody2D rigidBody;
    public Animator anim;
    bool onLand;
    float movementSpeed;
    private Vector3 checkpoint;
    public GameObject fallDetector;

    // Start is called before the first frame update
    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        checkpoint = transform.position;
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

        // Attacking
        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }

        // Falling
        PlayerFall();
        
        anim.SetBool("Onland", onLand);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    void PlayerMove(float speed, int direction)
    {
        Vector2 movementVector = new Vector2(direction, Input.GetAxis("Vertical"));
        movementSpeed = speed;
        transform.localScale = new Vector2(direction, 1);
        anim.SetBool("Running", true);
        //rigidBody.linearVelocity = new Vector2(movementSpeed * direction * Time.fixedDeltaTime, rigidBody.linearVelocity.y);

        if (direction > 0)
        {
            //rigidBody.MovePosition(transform.position + movementVector * Time.fixedDeltaTime * movementSpeed);
            //rigidBody.MovePosition(transform.position + Vector3.right * movementSpeed * Time.fixedDeltaTime);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime * direction);
        } else
        {
            //rigidBody.MovePosition(transform.position - movementVector * Time.fixedDeltaTime * movementSpeed);
            //rigidBody.MovePosition(transform.position - Vector3.left * movementSpeed * Time.fixedDeltaTime);
            transform.Translate(movementVector * movementSpeed * Time.deltaTime * direction);
        }
    }
    void PlayerStop()
    {
        movementSpeed = 0;
        anim.SetBool("Running", false);
        //rigidBody.linearVelocity = new Vector2(movementSpeed, rigidBody.linearVelocity.y);
    }

    void PlayerJump()
    {
        //rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jumping");
        onLand = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onLand = true;
        }

        if (collision.gameObject.CompareTag("FallingDetector"))
        {
            transform.position = checkpoint;
        }
    }

    void PlayerFall()
    {
        anim.SetFloat("FallingBound", rigidBody.linearVelocity.y);
    }

    void PlayerAttack()
    {
        anim.SetTrigger("Attacking");
    }
}