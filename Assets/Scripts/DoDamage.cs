using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Does damage to a an NPC (the bats usually)
public class DoDamage : MonoBehaviour
{
    public int health = 20;
    public int damage = 10;
    public int scoreValue = 25;
    public ParticleSystem deathEffect;
    public ParticleSystem hitEffect;
    private AudioSource audioSource;
    public AudioSource audioSourceOther;
    public AudioSource audioSourceDeath;

    public GameObject enemyThis;
    public GameObject bodyPartCentre;
    public GameObject bodyPartLeft;
    public GameObject bodyPartRight;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (health <= 0)
        {           
            audioSourceDeath.Play();
            audioSourceOther.Stop();
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("WizardOrb"))
        {
            audioSource.Play();
            hitEffect.Play();
            health -= damage;

        }
        if (hit.gameObject.CompareTag("Sword"))
        {
            audioSource.Play();
            hitEffect.Play();
            health -= damage;

        }

        if (hit.gameObject.CompareTag("Smasher"))
        {
            Die();

        }
    }
    void Bones()
    {
        GameObject boneInstance1;
        GameObject boneInstance2;
        GameObject boneInstance3;

        boneInstance1 = Instantiate(bodyPartCentre, transform.position + new Vector3(-0.2f, -0.2f, -0.2f), Quaternion.identity);
        boneInstance2 = Instantiate(bodyPartLeft, transform.position + new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity);
        boneInstance3 = Instantiate(bodyPartRight, transform.position + new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity);
    }

    void Die()
    {
        if (bodyPartCentre != null && bodyPartLeft != null && bodyPartRight != null)
        {
            Bones();
        }
        ScoreManager.instance.ChangeScore(scoreValue);
        FindObjectOfType<AudioManager>().Play("Coin");
        Destroy(gameObject);
        Destroy(enemyThis);
    }
}