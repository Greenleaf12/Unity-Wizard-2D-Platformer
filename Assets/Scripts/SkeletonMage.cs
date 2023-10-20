using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : MonoBehaviour
{
    private float leftCap;
    private float rightCap;
    [SerializeField] private LayerMask Ground;
    private LayerMask Player;

    private GameObject playerObject;
    private Rigidbody2D rbSkel;
    private Animator animator;

    private bool facingLeft = true;
    private bool facingRight = false;
    private bool walking = true;

    private bool idle = false;
    public float size = 0.8f;

    // Attack Vars //
    public ParticleSystem particleprefab;
    public Transform firePoint;
    public float moveSpeed = 4.0f;
    private bool attackMode = false;

    // Timer //
    public float cooldownTime = 1;
    private float nextFireTime = 0;

    // Random Sound Player
    private AudioSource audioSource;
    public AudioClip[] AggroSounds;
    private AudioClip shootClip;
    public AudioClip footSteps;

    void Start()
    {
        leftCap = transform.position.x - 10;
        rightCap = transform.position.x + 10;
        transform.localScale = new Vector3(size, size);
        Player = LayerMask.NameToLayer("Player");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rbSkel = GetComponent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();

        Move();
    }

    // Trigger Agro // Sound & Target //
    void OnTriggerEnter2D(Collider2D trig)
    {     
        if (trig.gameObject.tag == "Player")
        {

            if (Time.time > nextFireTime)
            {
                int index = Random.Range(0, AggroSounds.Length);
                shootClip = AggroSounds[index];
                audioSource.clip = shootClip;
                audioSource.Play();
                nextFireTime = Time.time + cooldownTime / 5;
            }
        }
    }

    // Trigger Attacking //
    void OnTriggerStay2D(Collider2D trig)
    {
            if (trig.gameObject.tag == "Player")
            {
                rbSkel.velocity = new Vector2(0, 0);
                attackMode = true;
                walking = false;
                animator.SetBool("canWalk", false);
                animator.SetBool("Attack", true);
            }  
    }

    // Stop attack and Move //
    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {

                attackMode = false;
                walking = true;
                animator.SetBool("canWalk", true);
                animator.SetBool("Attack", false);
        }
    }

    // Attack //
    void Attack()
    {
        audioSource.Stop();
        Instantiate(particleprefab, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("Staff_Fire");
    }

    private void Wait()
    {
        if (attackMode == false)
        {
            {
                StartCoroutine(TimerCoroutinex());
            }
            IEnumerator TimerCoroutinex()
            {
                // Wait then Idle Skeleton
                yield return new WaitForSeconds(Random.Range(6, 12));

                rbSkel.velocity = new Vector2(0, 0);
                walking = false;
                idle = true;
                animator.SetBool("canWalk", false);
                GetComponent<Animator>().Play("Skeleton_Idle");
                audioSource.Stop();

                // Wait then Move again
                yield return new WaitForSeconds(Random.Range(4, 8));

                walking = true;
                idle = false;
                animator.SetBool("canWalk", true);

                Move();
            }
        }
    }

    public void Flip ()
    {
        if (facingLeft == true && facingRight == false)
        {
            facingLeft = false;
            facingRight = true;
            transform.localScale = new Vector3(size, size);
            firePoint.localRotation = Quaternion.Euler(0, 0, 180);

        }
        if (facingRight == true && facingLeft == false)
        {
            facingLeft = true;
            facingRight = false;
            transform.localScale = new Vector3(-size, size);
            firePoint.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Move()
    {
        audioSource.clip = footSteps;
        audioSource.Play();

        // Check if facing left and above left cap
        if (facingLeft == true && transform.position.x > leftCap)
        {
            // flip sprite
            if (transform.localScale.x != size)
            {
                transform.localScale = new Vector3(size, size);
                firePoint.localRotation = Quaternion.Euler(0, 0, 180);
            }

            rbSkel.velocity = new Vector2(-moveSpeed, -2);
            walking = true;
        }
        else if (attackMode == false)
        {
            facingRight = true;
            facingLeft = false;
        }

        // Check if facing Right and above Right cap
        if (facingRight == true && transform.position.x < rightCap)
        {
            // flip sprite
            if (transform.localScale.x != -size)
            {
                transform.localScale = new Vector3(-size, size);
                firePoint.localRotation = Quaternion.Euler(0, 0, 0);
            }

            rbSkel.velocity = new Vector2(moveSpeed, -2);
            walking = true;

        }
        else if (attackMode == false)
        {
            facingRight = false;
            facingLeft = true;
        }
    }
}