using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTextTrigger : MonoBehaviour
{

    public GameObject uiObject;
    public ParticleSystem hit;

    private void Start()
    {
        uiObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Ball")
        {

            hit.GetComponent<ParticleSystem>();
            hit.Play();
            uiObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Goalbleep");
            FindObjectOfType<AudioManager>().Play("Cheer");
            GetComponent<Collider2D>().enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Ball")
        {
            //uiObject.SetActive(false);
            //Destroy(uiObject);
            //Destroy(GameStartText);
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
