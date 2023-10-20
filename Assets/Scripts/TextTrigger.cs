using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    public GameObject uiObject;
    public ParticleSystem hit;

    private void Start()
    {
        uiObject.SetActive(false);
        
    }


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {

            hit.GetComponent<ParticleSystem>();
            hit.Play();
            uiObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("powerup1");
            
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Stop("powerup1");
            uiObject.SetActive(false);
            Destroy(uiObject);
            //Destroy(GameStartText);
        }
    }

}
