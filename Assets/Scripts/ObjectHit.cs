using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private CharacterController2D controller;
    private PlayerMovement moveController;
    public ParticleSystem hitEffect;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public GameObject[] gibs;

    private TeleKi script;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;

    private bool bFirepoint = false;

    public int health = 5;

    public int gibAmount = 8;
    void Start()
    {
        script = GetComponent<TeleKi>();
        controller = GameObject.FindObjectOfType<CharacterController2D>();
        moveController = GameObject.FindObjectOfType<PlayerMovement>();
        audioSource = this.GetComponent<AudioSource>();
        rb =this.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Firepoint"))
        {
            if (!bFirepoint)
                bFirepoint = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Firepoint"))
        {
            if (bFirepoint)
                bFirepoint = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)

    {      
        if (Time.time > nextFireTime)
        {
            
            audioSource.Play();          
            nextFireTime = Time.time + cooldownTime / 2;  
        }

        if (other.gameObject.CompareTag("WizardOrb"))
        {
            hitEffect.Play();
            health -= 1;
        }
        if (other.gameObject.CompareTag("Sword"))
        {
            hitEffect.Play();
            health -= 1;
        }
        if (other.gameObject.CompareTag("MageOrb"))
        {
            hitEffect.Play();
            health -= 1;
        }

    }
      
    public void Update()
    {
        if (bFirepoint && moveController.attacking == true)
        {
            FindObjectOfType<AudioManager>().Play("Object_Hit_1");

            if (controller.m_FacingRight == true)
            {
                rb = GetComponent<Rigidbody2D>();
                //rb.mass = 1;
                rb.velocity = new Vector3(30f, 5, 0);
                bFirepoint = false;

            }
            else if (controller.m_FacingRight == false)
            {
                rb = GetComponent<Rigidbody2D>();
                ///rb.mass = 1;
                rb.velocity = new Vector3(-30f, 5, 0);
                bFirepoint = false;

            }
        }

        if (health <= 0)
        {
            Gibs();
            script.Deactivate();
            Destroy(gameObject);
        }
    }

    public void Gibs()

    {        
        for (int i = 0; i < gibAmount; i++)
        {
            int randomIndex = Random.Range(0, gibs.Length);
            Vector3 randomPos = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-0.4f, 0.4f));
            Instantiate(gibs[randomIndex], transform.position + randomPos, Quaternion.identity);

        }
    }
}