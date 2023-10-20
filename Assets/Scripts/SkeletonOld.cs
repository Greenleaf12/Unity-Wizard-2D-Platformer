using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonOld : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Player;

    public Collider2D colSkel;
    public Rigidbody2D rbSkel;

    public Animator animator;
    public bool facingLeft = true;
    public bool facingRight = false;
    public bool walking = true;
    public bool running = false;
    public bool idle = false;
    public float size = 0.8f;

    // Attack Vars //

    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;

    private RaycastHit2D hit;
    private GameObject target;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    // Timer //
    public float cooldownTime = 1;
    private float nextFireTime = 0;

    // Random Sound Player
    private AudioSource audioSource;
    public AudioClip[] AggroSounds;
    private AudioClip shootClip;

    void Start()
    {
        Move();
        colSkel = GetComponent<Collider2D>();
        rbSkel = GetComponent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (walking == true && attackMode == false)
        {
            Move();
        }

        if (inRange && facingLeft == true)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
        }
       
        if (inRange && facingRight == true)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
        }
       
        if (hit.collider != null)
        {
            EnemyLogic();        
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }
       
        if (inRange == false)
       
        {        
            // animator.SetBool("canWalk", false);
            //  StopAttack();
        }
    }

    // Trigger Agro //

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;

            if (Time.time > nextFireTime)
            {
                int index = Random.Range(0, AggroSounds.Length);
                shootClip = AggroSounds[index];
                audioSource.clip = shootClip;
                audioSource.Play();
                //  FindObjectOfType<AudioManager>().Play("Skeleton_Agro");
                nextFireTime = Time.time + cooldownTime / 3;
            }
        }
    }

      // Start or Stop attack //
    
      void EnemyLogic()
    
      {
          distance = Vector2.Distance(transform.position, target.transform.position);
    
        
          if (distance < attackDistance)
          {
    
              walking = false;
              attackMode = true;
              Attack();
          }
          else if (distance > attackDistance)
          {
              StopAttack();
          }
    
          if (cooling)
    
          {
             // animator.SetBool("Attack", false);
          }
    
          if (distance < attackDistance)
          {
              
          }
      }

    // Trigger Running //

    void OnTriggerStay2D(Collider2D triggerSkel)
    {
        target = triggerSkel.gameObject;
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (triggerSkel.gameObject.tag == "Player" && facingLeft == true && distance > attackDistance)

        {
            attackMode = true;
            
            walking = false;
            running = true;
            animator.SetBool("canRun", true);
            animator.SetBool("canWalk", false);
            rbSkel.velocity = new Vector2(-8, -2);
            GetComponent<Animator>().Play("Skeleton_Run");

            // flip sprite
            if (transform.localScale.x != size)
            {
                transform.localScale = new Vector3(size, size);
            }      
        }


        if (triggerSkel.gameObject.tag == "Player" && facingRight == true && distance > attackDistance)

        {
            attackMode = true;
            walking = false;
            running = true;
            animator.SetBool("canRun", true);
            animator.SetBool("canWalk", false);
            rbSkel.velocity = new Vector2(8, -2);
            GetComponent<Animator>().Play("Skeleton_Run");

            // flip sprite
            if (transform.localScale.x != -size)
            {
                transform.localScale = new Vector3(-size, size);
            }
        }

        // Turn around if hit crate //

        if (triggerSkel.gameObject.tag == "Crate" && attackMode == false)
        {
            if (facingLeft == true)
            {
                if (attackMode == false && idle == true)
                {
                    facingRight = true;
                    facingLeft = false;
                }
            }

            if (facingRight == true)
            {
                if (attackMode == false && idle == true)
                {
                    facingRight = false;
                    facingLeft = true;
                }
            }
        }
    }

    // Stop attack and Move //

    void OnTriggerExit2D(Collider2D triggerSkel)

    {
        StopAttack();
        if (distance > attackDistance && attackMode == false)
        {
            
            Move();
        }   
    }

    // Attack //

    void Attack()
    {
        // timer = intTimer;
        attackMode = true;
        walking = false;

        animator.SetBool("canWalk", false);
        animator.SetBool("Attack", true);

        if (facingLeft == true)
        {
            rbSkel.velocity = new Vector2(-1f, 0);
        }

        if (facingRight == true)
        {
            rbSkel.velocity = new Vector2(1f, 0);
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
            yield return new WaitForSeconds(1);
            animator.SetBool("Attack", false);
            //cooling = false;
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
                yield return new WaitForSeconds(Random.Range(2, 6));
                animator.SetBool("canWalk", false);
                walking = false;
                GetComponent<Animator>().Play("Skeleton_Idle");
                FindObjectOfType<AudioManager>().Stop("Skeleton_Footsteps");
                rbSkel.velocity = new Vector2(0, 0);
                idle = true;
                // Wait then Move again
                yield return new WaitForSeconds(Random.Range(3, 9));
                idle = false;
                walking = true;
                animator.SetBool("canWalk", true);

            }

        }

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
            
            rbSkel.velocity = new Vector2(-4, -2);
            walking = true;
        }

        else if (attackMode == false)
        {

            facingRight = true;
            facingLeft = false;

        }

        if (walking == true)
        {
            GetComponent<Animator>().Play("Skeleton_Walk");
        }

        // Check if facing left and above left cap
        if (facingRight == true && transform.position.x < rightCap)
        {
            // flip sprite
            if (transform.localScale.x != -size)
            {
                transform.localScale = new Vector3(-size, size);
            }

            rbSkel.velocity = new Vector2(4, -2);
            walking = true;

        }

        else if(attackMode == false)
        {
            facingRight = false;
            facingLeft = true;
        }

        if (walking == true)
        {
            GetComponent<Animator>().Play("Skeleton_Walk");
        }
    }
}
