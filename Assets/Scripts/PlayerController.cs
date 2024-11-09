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
    public bool onLand;
    public bool isMoving;
    public bool isAttacking;
    public bool isJumping;
    public float movementSpeed;
    private Vector3 checkpoint;
    public GameObject fallDetector;
    private IEnumerator attackTime;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isAttacking = false;
        isJumping = false;
        checkpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        isMoving = hDirection != 0;

        // Moving
        if (hDirection > 0 && isMoving == true && isAttacking != true)
        {
            PlayerMove(speedForce, 1);
        }

        // Left movement
        else if (hDirection < 0 && isMoving == true && isAttacking != true)
        {
            PlayerMove(-speedForce, -1);
        }

        else if (hDirection == 0) {
            PlayerStop();
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && onLand == true)
        {
            PlayerJump();
        }

        // Attacking
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlayerAttack());
            isMoving = false;
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

        if (direction > 0)
        {
            transform.Translate(movementVector * movementSpeed * Time.deltaTime * direction);
        } else if (direction < 0)
        {
            transform.Translate(movementVector * movementSpeed * Time.deltaTime * direction);
        }
    }
    void PlayerStop()
    {
        movementSpeed = 0;
        anim.SetBool("Running", false);
    }
    void PlayerJump()
    {
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
    IEnumerator PlayerAttack()
    {
        anim.SetBool("Running", false);
        anim.SetTrigger("Attacking");
        isMoving = false;
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        isMoving = true;
    }
}