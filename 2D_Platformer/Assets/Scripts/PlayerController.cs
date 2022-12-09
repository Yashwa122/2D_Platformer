using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;

    private bool grounded;
    public LayerMask groundLayer;

    public Transform wallCheck;
    bool isWallTouch;
    bool isSliding;
    public float wallSlidingSpeed;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            HighJump();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);

        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.1f, 1), 0, groundLayer);
        if(isWallTouch && !grounded && horizontalInput != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void HighJump()
    {
        if(jumpTimeCounter > 0)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
