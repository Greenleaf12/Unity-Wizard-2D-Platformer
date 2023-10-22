using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private AudioSource audioSource;

    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    public float velocityX = 12.0f;
    public float velocityY = 6.0f;
    public float randomScaleMin = 0.6f;
    public float randomScaleMax = 0.8f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Random.Range(-velocityX, velocityX), Random.Range(velocityY, velocityY+4), Random.Range(0.0f, 0.0f));
        gameObject.transform.localScale = new Vector3(Random.Range(randomScaleMin, randomScaleMax), Random.Range(randomScaleMin, randomScaleMax), Random.Range(randomScaleMin, randomScaleMax));

        {
            StartCoroutine(TimerCoroutinex());
        }

        IEnumerator TimerCoroutinex()
        {
            yield return new WaitForSeconds(Random.Range(6, 12));
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (Time.time > nextFireTime)
        {
            audioSource.Play();
            nextFireTime = Time.time + cooldownTime / 2;
        }
    }
}
