using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Frog NPC
public class Frog : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpLength;
    [SerializeField] private LayerMask Ground;
    private Vector3 frogSize;

    private Collider2D col;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingLeft = true;
    private bool facingRight = false;
    private AudioSource audioSource;
    private SpriteRenderer spriteR;
    bool isGrounded;

    private void Start()
    {
        float randomSize = (Random.Range(0.2f, 0.6f));
        frogSize = new Vector3(randomSize, randomSize, randomSize);
        animator = GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = frogSize;
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Stop()
    {
        if (col.IsTouchingLayers(Ground))

        {
            rb.velocity = new Vector2(0, 0);
        }               
    }

    public void Grounded()
    {
        if (col.IsTouchingLayers(Ground))
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
        }
    }

    private void jumpingLeft() 
    
            {
            audioSource.Play();
            spriteR.flipX = false;
            animator.SetFloat("Timer", 0);
            GetComponent<Animator>().Play("Frog_Jump");
            rb.velocity = new Vector2(-jumpLength, jumpHeight);
            }

    private void jumpingRight() 

    {
        audioSource.Play();
        spriteR.flipX = true;
        animator.SetFloat("Timer", 0);
        GetComponent<Animator>().Play("Frog_Jump");
        rb.velocity = new Vector2(jumpLength, jumpHeight);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crate")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    private void Move()
        {

        // Check if facing left and above left cap
        if (facingLeft == true && transform.position.x > leftCap)
        {

            // jump if touching ground
            if (col.IsTouchingLayers(Ground))
            {
                StartCoroutine(TimerCoroutine());
            }

            IEnumerator TimerCoroutine()
            {
                yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
                jumpingLeft();
            }
        }

        // else change direction
        else
        {
            // Jump right
            facingRight = true;
            facingLeft = false;
        }

        // Check if facing right and below right cap
        if (facingRight == true && transform.position.x < rightCap)
        {
            jumpingRight();
        }

        // else change direction
        else
        {
            // Jump Left
            facingLeft = true;
            facingRight = false;
            jumpingLeft();
           
        }
    }
}