using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float noMovementDrag;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D boxCollider;
    public Vector2 movement;
    Vector3 tempPosition;

    void Start()
    {
        noMovementDrag = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        rigidBody.position += new Vector2(movement, 0) * movementSpeed * Time.deltaTime;
        transform.position = rigidBody.position;
        //rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);

        //if (movement < -0.01f)
        //{
        //    spriteRenderer.flipX = true;
        //}

        //if (movement > 0.01f)
        //{
        //    spriteRenderer.flipX = false;
        //}

        //animator.SetBool("is_walking", Mathf.Abs(movement) > 0.01f);

        if (movement == 0)
        {
            if (rigidBody.velocity.x > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x - noMovementDrag, rigidBody.velocity.y);
            }
            if (rigidBody.velocity.x < 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x + noMovementDrag, rigidBody.velocity.y);
            }
        }

        if (rigidBody.velocity.x > 10)
        {
            rigidBody.velocity = new Vector2(10, rigidBody.velocity.y);
        }

        if (rigidBody.velocity.x < -10)
        {
            rigidBody.velocity = new Vector2(-10, rigidBody.velocity.y);
        }

        if (Input.GetKeyDown("space"))
        {
            jump();
        }
    }

    void jump()
    {
        if (isGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log("jump");
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(0.25f, 0f, 0), -0.01f, Vector2.down, boxCollider.bounds.extents.y, layerMask);
        return raycastHit.collider.tag == "Ground";
        
    }
}
