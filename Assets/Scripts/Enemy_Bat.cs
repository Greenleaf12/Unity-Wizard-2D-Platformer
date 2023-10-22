using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : MonoBehaviour
{
    private AudioSource audioSource;
    public int health ;
    public GameObject deathEffect;
    public GameObject bonesHead;
    public GameObject bonesRib1;
    public GameObject bonesArm1;

    public ParticleSystem hitEffect;
    public ParticleSystem otherEffect;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioSource.Play();
        hitEffect.Play();

        if (health <= 0)
        {
            otherEffect.Play();
            Instantiate(deathEffect, transform.position, Quaternion.identity);        
            FindObjectOfType<AudioManager>().Play("Skeleton_Die");
            Bones();
            Die(); 
        }     
    }

    void Bones()
    {
        GameObject boneInstance1;
        GameObject boneInstance2;
        GameObject boneInstance3;

        boneInstance1 = Instantiate(bonesHead, transform.position + new Vector3(1.5f, 1.5f, 1.5f), Quaternion.identity);
        boneInstance2 = Instantiate(bonesRib1, transform.position + new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity);
        boneInstance3 = Instantiate(bonesArm1, transform.position + new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity);  
    }

    void Die()
    {
        Destroy(gameObject);
    }  
}
