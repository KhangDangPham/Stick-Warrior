using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedForce = 5.0f;
    public float jumpForce = 10f;
    public Rigidbody2D rigidBody;
    public Animator anim;
    bool onLand;
    float movementSpeed;

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
            anim.SetBool("Running", false);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && onLand)
        {
            PlayerJump();
        }

        //anim.SetBool("Running", hDirection != 0);
        anim.SetBool("Onland", onLand);
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
}