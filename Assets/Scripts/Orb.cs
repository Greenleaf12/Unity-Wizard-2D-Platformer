using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player weapon projectile
public class Orb : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject orbEffect;
    private AudioSource audioSource;
    public AudioClip hitEffect;

    public ParticleSystem playerBlood;
    void Start()
    {
        rb.velocity = transform.right * speed * 5;
        audioSource = this.GetComponent<AudioSource>();
        {
            StartCoroutine(TimerCoroutinex());
        }
        IEnumerator TimerCoroutinex()
        {
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D ouch)
    {        
        EnemyHit enemy = ouch.GetComponent<EnemyHit>();

        if (ouch.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Orb_Hit");
            audioSource.Play();                      
            Instantiate(orbEffect, transform.position, Quaternion.identity);
            if (ouch != null)
            {
                //Remove Player Health
                FindObjectOfType<HealthBar>().RemoveHealth(damage);

                //Play Blood Effect
                Instantiate(playerBlood, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (ouch.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("Orb_Hit");
            FindObjectOfType<AudioManager>().Play("Elec_Hit");
            Instantiate(orbEffect, transform.position, Quaternion.identity);
            if (enemy!=null)
            {
                enemy.TakeDamage(damage);
            }          
            Destroy(gameObject);
        }

        if (ouch.gameObject.CompareTag("Crate"))
        {
            FindObjectOfType<AudioManager>().Play("Orb_Hit");
            FindObjectOfType<AudioManager>().Play("Elec_Hit");
            Instantiate(orbEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);                    
        }

        if (ouch.gameObject.CompareTag("Landscape"))
        {
            FindObjectOfType<AudioManager>().Play("Orb_Hit");
            FindObjectOfType<AudioManager>().Play("Elec_Hit");
            Instantiate(orbEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (ouch.gameObject.CompareTag("Platform"))
        {
            FindObjectOfType<AudioManager>().Play("Orb_Hit");
            FindObjectOfType<AudioManager>().Play("Elec_Hit");
            Instantiate(orbEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
