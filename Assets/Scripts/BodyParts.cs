using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour
{
    public AudioClip impact;
    private AudioSource audioSource;
    private ParticleSystem bloodEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bloodEffect = GetComponent<ParticleSystem>();
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 6.0f), Random.Range(0.0f, 0.0f));

        //Rigidbody2D.velocity = new Vector3(Random.Range(-12.0f, 12.0f), Random.Range(5.0f, 10.0f), Random.Range(5.0f, 10.0f)); 

        {
            StartCoroutine(TimerCoroutinex());
        }

        IEnumerator TimerCoroutinex()
        {
            // Wait
            yield return new WaitForSeconds(Random.Range(6, 12));

            Destroy(gameObject);

    }
    }

    void OnTriggerEnter2D(Collider2D trig)

    {
        bloodEffect.Play();
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
