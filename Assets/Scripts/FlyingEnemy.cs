using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for flying enemys 
public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;

    public bool chase = false;
    private Vector3 startingPoint;

    [SerializeField] private LayerMask Player;
    public Animator animator;

    public Collider2D col;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    private AudioSource audioSource;
    Vector3 addV;

    void Start()
    {
        addV = new Vector3 (Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-1f, 1f));
        startingPoint = transform.position + addV;
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
        if (gameObject != null)
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
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            chase = false;
        }
    }

    private void attacking()
    {
        animator.SetBool("IsAttacking", true);
        if (Time.time > nextFireTime)
        {
            FindObjectOfType<AudioManager>().Play("Bat_Attack");
            nextFireTime = Time.time + cooldownTime / 2;
        }
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position + new Vector3(1.0f , 2.6f , 0.0f), speed * Time.deltaTime);           
    }

    private void ReturnHome()
    {
        animator.SetBool("IsAttacking", false);
        transform.position = Vector2.MoveTowards(transform.position, startingPoint, speed/4 * Time.deltaTime);
    }
}
