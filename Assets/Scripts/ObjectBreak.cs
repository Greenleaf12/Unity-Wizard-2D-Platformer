using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreak : MonoBehaviour
{
    private AudioSource audioSource;
    public int health = 100;
    public GameObject deathEffect;
    public ParticleSystem hitEffect;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioSource.Play();
        hitEffect.Play();

        if (health <= 0)
        {

            Instantiate(deathEffect, transform.position, Quaternion.identity);

            //FindObjectOfType<AudioManager>().Play("Skeleton_Die");
            Die();
            
        }     
    }

    void Die()
    {
        
        Destroy(gameObject);
    }  

}
