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

    void Awake()
    {
/*        foreach (AudioClip s in shoot)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }*/
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.pitch = pitch;
        audioSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {

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
