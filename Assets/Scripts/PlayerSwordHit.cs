using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordHit : MonoBehaviour
{

    public float cooldownTime = 1f;
    private float nextFireTime = 0f;

    private AudioSource audioSource;
    public AudioClip[] shoot;
    private AudioClip shootClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D trigs)
    {

        if (Time.time > nextFireTime && trigs.gameObject.tag == "EnemySFXHitBox")
        {

            int index = Random.Range(0, shoot.Length);
            shootClip = shoot[index];
            audioSource.clip = shootClip;
            audioSource.Play();
            nextFireTime = Time.time + cooldownTime / 3;
            
        }

    }
}
