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
    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    bool isGrounded;

    public AudioSource playerAudioFiles;
    public AudioClip[] jumpSndArray;
    public AudioClip[] landSndArray;

    void Awake()
    {
        playerAudioFiles = GetComponent<AudioSource>();
    }

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
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(0.25f, 0f, 0), -0.01f, Vector2.down, boxCollider.bounds.extents.y, layerMask);
        if (raycastHit.collider != null)
        {
            isGrounded = true;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            isGrounded = false;
            coyoteTimeCounter -= Time.deltaTime;
        }


        var movement = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(movement * movementSpeed, rigidBody.velocity.y);
        //rigidBody.position += movement * movementSpeed * Time.deltaTime;
        //rigidBody.MovePosition(new Vector2(rigidBody.position.x + movement.x * movementSpeed * Time.fixedDeltaTime, rigidBody.position.y));
        //rigidBody.AddForce(movement * movementSpeed);
        //rigidBody.position.y - rigidBody.gravityScale * Time.deltaTime)
        transform.position = rigidBody.position;

        if (isGrounded == true)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        

        if (movement < -0.01f)
        {
            spriteRenderer.flipX = true;
        }

        if (movement > 0.01f)
        {
            spriteRenderer.flipX = false;
        }

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

        if (rigidBody.velocity.x > movementSpeed)
        {
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
        }

        if (rigidBody.velocity.x < -movementSpeed)
        {
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
        }

        if (Input.GetKeyDown("space"))
        {
            jump();
        }
    }

    void jump()
    {
        if (coyoteTimeCounter > 0)
        {
            coyoteTimeCounter = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.velocity = new Vector2(0, jumpForce);
            Debug.Log("jump");
            playerAudioFiles.clip = jumpSndArray[Random.Range(0, jumpSndArray.Length)];
            playerAudioFiles.PlayOneShot(playerAudioFiles.clip);
        }
    }
}
