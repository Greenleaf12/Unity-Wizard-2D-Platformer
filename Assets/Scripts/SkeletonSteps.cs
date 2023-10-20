using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSteps : MonoBehaviour
{
    private AudioSource audioSource;
    public SkeletonOld SkeletonScript;

    public float cooldownTime = 1;
    private float nextFireTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         Step();
    }

    void Step()

    {

        if (Time.time > nextFireTime)
        {
            if (SkeletonScript.walking == true || SkeletonScript.running == true)
            {
                audioSource.Play();
                nextFireTime = Time.time + cooldownTime;
            }
        }

        if (SkeletonScript.idle == true)
        {
            audioSource.Stop();
        }
    }
}
