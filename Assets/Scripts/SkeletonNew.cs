using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNew : MonoBehaviour
{
    public float leftCap = 0;
    public float rightCap= 0;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Player;

    public GameObject playerObject;

    public Collider2D colSkel;
    public Collider2D colAgro;
    public Rigidbody2D rbSkel;

    public Animator animator;
    public bool facingLeft = true;
    public bool facingRight = false;
    public bool walking = true;
    public bool running = false;
    public bool idle = false;
    public float size = 0.8f;

    // Attack Vars //

    public float attackDistance;
    public float moveSpeed = 4.0f;

    private GameObject target;
    private float distance;
    public bool attackMode = false;

    // Timer //
    public float cooldownTime = 1;
    private float nextFireTime = 0;

    // Random Sound Player
    private AudioSource audioSource;

    public AudioClip[] AggroSounds;
    private AudioClip shootClip;
    public AudioClip walkSound;

    private void Awake()
    {

    }

    void Start()
    {
        if (leftCap == 0)
        {
            leftCap = transform.position.x - 10;
        }
        if (rightCap == 0)
        {
            rightCap = transform.position.x + 10;
        }

        playerObject = GameObject.FindGameObjectWithTag("Player");
        Move();
        colSkel = GetComponent<Collider2D>();
        rbSkel = GetComponent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerObject == null)
        {
            attackMode = false;
            walking = true;
            animator.SetBool("Attack", false);
            animator.SetBool("canWalk", true);
            animator.SetBool("canRun", false);

        }
        if (walking == true && attackMode == false)
        {
             Move();
        }
    }

    // Trigger Agro // Sound & Target //

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;

            if (Time.time > nextFireTime)
            {
                int index = Random.Range(0, AggroSounds.Length);
                shootClip = AggroSounds[index];
                audioSource.clip = shootClip;
                audioSource.PlayOneShot(shootClip);
                nextFireTime = Time.time + cooldownTime / 5;
            }
        }
    }

    // Trigger Attacking //

    void OnTriggerStay2D(Collider2D skelTrigger)
    {
        target = skelTrigger.gameObject;
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (colAgro.IsTouchingLayers(Player) && playerObject != null)
        {
            if (skelTrigger.gameObject.tag == "Player" && facingLeft == true && distance > attackDistance)

            {
                attackMode = true;
                walking = false;
                running = true;
                animator.SetBool("Attack", false);
                animator.SetBool("canWalk", false);
                animator.SetBool("canRun", true);
                rbSkel.velocity = new Vector2(-8, -2);

                // flip sprite
                if (transform.localScale.x != size)
                {
                    transform.localScale = new Vector3(size, size);
                    facingLeft = false;
                    facingRight = true;
                }
            }
         
            if (skelTrigger.gameObject.tag == "Player" && facingRight == true && distance > attackDistance)

            {
                attackMode = true;
                walking = false;
                running = true;
                animator.SetBool("Attack", false);
                animator.SetBool("canWalk", false);
                animator.SetBool("canRun", true);
                rbSkel.velocity = new Vector2(8, -2);
                //GetComponent<Animator>().Play("Skeleton_Run");

                // flip sprite
                if (transform.localScale.x != -size)
                {
                    transform.localScale = new Vector3(-size, size);
                    facingLeft = true;
                    facingRight = false;
                }
            }

            if (colSkel.IsTouchingLayers(Player) && playerObject != null)
            {
                Attack();
                if (distance < attackDistance)
                {
                    rbSkel.velocity = new Vector2(0f, 0);

                }
                if (distance > attackDistance)
                {
                    if (facingLeft == true)
                        rbSkel.velocity = new Vector2(-0.2f, 0);
                    else
                        rbSkel.velocity = new Vector2(0.2f, 0);
                }
            }
        }
    }

    // Stop attack and Move //
    void OnTriggerExit2D(Collider2D triggerSkel)
    {
        attackMode = false;
        //StopAttack();
        if (triggerSkel.gameObject.tag == "Player")
        {
            if (distance > attackDistance)
            {
                animator.SetBool("canWalk", true);
                animator.SetBool("canRun", false);               
                animator.SetBool("Attack", false);
                
                Move();               
            }
        }
    }

    // Attack //
    void Attack()
    {
        if (playerObject != null)
        {
            attackMode = true;
            walking = false;
            animator.SetBool("canRun", false);
            animator.SetBool("canWalk", false);
            animator.SetBool("Attack", true);
        }     
    }
    void StopAttack()
    {
        {
            StartCoroutine(TimerCoroutinex());
        }
        IEnumerator TimerCoroutinex()
        {          
            // Wait 
            yield return new WaitForSeconds(0.2f);
           
            attackMode = false;            
        }
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
                animator.SetBool("canRun", false);
                animator.SetBool("canWalk", false);
                animator.SetBool("Attack", false);

                // Wait then Move again
                yield return new WaitForSeconds(Random.Range(4, 8));

                walking = true;
                idle = false;
                running = false;
                animator.SetBool("canWalk", true);
                Move();

            }
        }
    }

    public void walkPlay()
    {
        audioSource.clip = walkSound;
        audioSource.Play();
    }

    public void walkStop()
    {
        audioSource.clip = walkSound;
        audioSource.Stop();
    }

    public void Move()
    {
        running = false;
        // Check if facing left and above left cap
        if (facingLeft == true && transform.position.x > leftCap)
        {
            // flip sprite
            if (transform.localScale.x != size)
            {
                transform.localScale = new Vector3(size, size);
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
            }

            rbSkel.velocity = new Vector2(moveSpeed, -2);
            walking = true;

        }
        else if(attackMode == false)
        {
            facingRight = false;
            facingLeft = true;
        }

        if (walking == true)
        {
            animator.SetBool("canRun", false);
            animator.SetBool("canWalk", true);
            animator.SetBool("Attack", false);
        }
    }
}
