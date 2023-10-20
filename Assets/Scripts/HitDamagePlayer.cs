using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamagePlayer : MonoBehaviour
{

    public float cooldownTime = 1f;
    public int damage;
    private float nextFireTime = 0f;

    private AudioSource audioSource;
    private AudioSource audioSource2;
    public AudioClip[] SwordSounds;
    private AudioClip shootClip;
    
    public AudioClip[] WizardHurtSounds;
    private AudioClip shootClip2;


    public ParticleSystem playerBlood;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D trigs)
    {

        if (Time.time > nextFireTime && trigs.gameObject.tag == "Player")
        {          
            //Remove Player Health
            FindObjectOfType<HealthBar>().RemoveHealth(damage);

            //Play Blood Effect
            playerBlood.Play();

            //Play Sword hit Sound
            int index = Random.Range(0, SwordSounds.Length);
            shootClip = SwordSounds[index];
            audioSource.clip = shootClip;
            audioSource.Play();

            //Play hurt Sound
            //int index2 = Random.Range(0, WizardHurtSounds.Length);
            //shootClip2 = WizardHurtSounds[index2];
            //audioSource.clip = shootClip2;
            //audioSource.Play();

            //Set next fire time
            nextFireTime = Time.time + cooldownTime / 3;
            
        }
    }
}
