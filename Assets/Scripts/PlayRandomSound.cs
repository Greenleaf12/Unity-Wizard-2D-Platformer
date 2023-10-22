using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayRandomSound : MonoBehaviour
{
    public float cooldownTime = 0.5f;
    public float pitch = 1.0f;
    public float volume = 1.0f;
    private float nextFireTime = 0f;

    private AudioSource audioSource;
    public AudioClip[] shoot;
    private AudioClip shootClip;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.pitch = pitch;
        audioSource.volume = volume;
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
