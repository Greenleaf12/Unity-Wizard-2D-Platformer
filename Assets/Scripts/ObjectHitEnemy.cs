using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHitEnemy : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerMovement moveController;
    public ParticleSystem hit;
    public Rigidbody2D rb;

    private bool bFirepoint = false;

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Firepoint"))
        {
            if (!bFirepoint)
                bFirepoint = true;
        }

    }

        public void Update()
        {

        if (bFirepoint && moveController.attacking == true)
        {

            FindObjectOfType<AudioManager>().Play("Object_Hit_1");

            if (controller.m_FacingRight == true)
            {
                rb = GetComponent<Rigidbody2D>();
                //rb.mass = 1;
                rb.velocity = new Vector3(10f, 0, 0);
                bFirepoint = false;

            }
            else if (controller.m_FacingRight == false)
            {
                rb = GetComponent<Rigidbody2D>();
                ///rb.mass = 1;
                rb.velocity = new Vector3(-10f, 0, 0);
                bFirepoint = false;

            }

        }

    }

}
