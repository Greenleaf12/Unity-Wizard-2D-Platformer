using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public int ballValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<AudioManager>().Stop("Music2");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<Collider2D>().isTrigger = false;
            GetComponent<EdgeCollider2D>().isTrigger = false;
            
            ScoreManager.instance.ChangeScore(ballValue);
            


        }     

    }

}
