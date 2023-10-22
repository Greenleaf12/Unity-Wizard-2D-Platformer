using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Does damage to enemy (Skeletons)
// Uses health and adds score
// Spawns gibs on death

public class EnemyHit : MonoBehaviour
{
    public int health = 100;
    public int scoreValue = 100;
    public GameObject[] gibs;
    public GameObject deathEffect;

    public ParticleSystem hitEffect;

    private AudioSource audioSource;
    public AudioClip[] HitSounds;
    private AudioClip shootClip;

    private void Update()
    {
        if (health <= 0 && gameObject != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Stop("Skeleton_Footsteps");
            FindObjectOfType<AudioManager>().Play("Skeleton_Die");
            Die();
        }
    }
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (gameObject != null)
        {
            int index = Random.Range(0, HitSounds.Length);
            shootClip = HitSounds[index];
            audioSource.clip = shootClip;
            health -= damage;
            audioSource.Play();
            hitEffect.Play();          
        }
    }

    void Die()
    {
        ScoreManager.instance.ChangeScore(scoreValue);
        FindObjectOfType<AudioManager>().Play("Coin");
        Destroy(gameObject);
        Bones();
    }

    void Bones()
    {
        for (int i = 0; i < gibs.Length; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(0.0f, 2.0f), Random.Range(-4.0f, 4.0f), Random.Range(-0.4f, 0.4f));
            Instantiate(gibs[i], transform.position + randomPos, Quaternion.identity);
        }
    }
}