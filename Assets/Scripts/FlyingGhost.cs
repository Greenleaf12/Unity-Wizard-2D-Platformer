using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGhost : MonoBehaviour
{
    public float speed;
    private GameObject player;

    private bool chase = false;
    public Transform startingPoint;

    [SerializeField] private LayerMask Player;
    public Animator animator;

    public Collider2D col;
    private float cooldownTime = 1f;
    private float nextFireTime = 0f;
    private AudioSource audioSource;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = this.GetComponent<AudioSource>();

        if (this == null)
        {
            audioSource.Stop();
        }      
    }

    void Update()
    {
        if (player == null)
            ReturnHome();
        if (chase == true)
            Chase();
        else
            ReturnHome();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            chase = true;
        }

        if (col.IsTouchingLayers(Player))
        {
            attacking();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            chase = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    private void attacking()
    
    {
            animator.SetBool("IsAttacking", true);
            if (Time.time > nextFireTime)
            {
                FindObjectOfType<AudioManager>().Play("Bat_Attack");
                nextFireTime = Time.time + cooldownTime;
            }
    }

    private void Chase()

    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position + new Vector3(3.2f , 1.0f , 0.0f), speed * Time.deltaTime);          
    }

    private void ReturnHome()

    {
        animator.SetBool("IsAttacking", false);
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.transform.position, speed/4 * Time.deltaTime);
    }
}
