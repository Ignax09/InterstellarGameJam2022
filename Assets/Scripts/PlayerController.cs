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
        var movement = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(movement * movementSpeed, rigidBody.velocity.y);
        //rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);

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
            playerAudioFiles.clip = jumpSndArray[Random.Range(0, jumpSndArray.Length)];
            playerAudioFiles.PlayOneShot(playerAudioFiles.clip);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(0.25f, 0f, 0), -0.01f, Vector2.down, boxCollider.bounds.extents.y, layerMask);
        return raycastHit.collider.tag == "Ground" || raycastHit.collider.tag == "MovingPlatform";
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerAudioFiles.clip = landSndArray[Random.Range(0, landSndArray.Length)];
        playerAudioFiles.PlayOneShot(playerAudioFiles.clip);
        if (collision.gameObject.tag == "Spike")
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("lol you died");
    }
}
