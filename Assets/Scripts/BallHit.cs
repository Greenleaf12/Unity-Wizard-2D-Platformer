using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{

    public ParticleSystem hit;
    public Rigidbody2D rb;


    private bool bGoal = false;
    private bool bLandscape = false;
    private bool bPlayer = false;

    private Vector3 tempPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            if (!bGoal)
   
                bGoal = true;
        }

        if (other.gameObject.CompareTag("Landscape"))
        {
            if (!bLandscape)
                bLandscape = true;
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            if (!bPlayer)
                bPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        FindObjectOfType<AudioManager>().Stop("Object_Hit_1");

    }

        private void Update()
    {
        if (bGoal)
        {     
            
            TimerController.instance.EndTimer();
            FindObjectOfType<AudioManager>().Stop("Music2");
            //FindObjectOfType<AudioManager>().Play("Object_Hit_1");
            hit.GetComponent<ParticleSystem>();
            hit.Play();

            
            rb = GetComponent<Rigidbody2D>();
            rb.mass = 10;
            rb.velocity = new Vector3(0.5f, 2, 0);


            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().isTrigger = false;

            bGoal = false;
        }

        if (bLandscape)
        {
            FindObjectOfType<AudioManager>().Play("Object_Hit_1");

            bLandscape = false;

        }

        else if (bPlayer)
        {
            bPlayer = false;
        }
    }

}
