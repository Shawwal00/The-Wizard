using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private AudioSource walkOutside;
    private AudioSource jumpClip;

    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 10f;
    int jumpUsed = 0;
    float jumpCooldown = 0f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        walkOutside = GetComponent<AudioSource>();
        jumpClip = GameObject.Find("Jump").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        jumpCooldown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            walkOutside.Play();
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) //Need to add for Jumping
        {
            walkOutside.Stop();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpUsed < 2)
            {
                jumpCooldown = 0f;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
                jumpClip.Play();
                jumpUsed++;
            }
        }

        if (jumpCooldown > 2f)
        {
            jumpCooldown = 0f;
            jumpUsed = 0;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
        }

    }
}
