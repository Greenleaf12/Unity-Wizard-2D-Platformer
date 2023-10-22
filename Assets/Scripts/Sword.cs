using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public CharacterController2D controller;
    public int damage = 10;
    public GameObject hitEffect;
    private AudioSource audioSource;
    public ParticleSystem swordParticles;

    void Start()
    {
            audioSource = this.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (controller.FacingRight == true)
        {
          swordParticles.transform.position = new Vector3(0, 0, 0);
        }

        if (controller.FacingRight == false)

        {
          swordParticles.transform.position = new Vector3(100, 0, -20);
        }
    }

    void OnTriggerEnter2D (Collider2D ouch)
    {
        EnemyHit enemy = ouch.GetComponent<EnemyHit>();

        if (ouch.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (ouch.gameObject.CompareTag("Crate"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);                    
        }
    }
}
