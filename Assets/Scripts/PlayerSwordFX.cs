using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player sword hit audio
public class PlayerSwordFX : MonoBehaviour
{
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;

    private AudioSource audioSource;
    public AudioClip[] shoot;
    private AudioClip shootClip;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayClip()
    { 
        if (Time.time > nextFireTime)
        {
            int index = Random.Range(0, shoot.Length);
            shootClip = shoot[index];
            audioSource.clip = shootClip;
            audioSource.Play();
            nextFireTime = Time.time + cooldownTime / 3;           
        }
    }
}
