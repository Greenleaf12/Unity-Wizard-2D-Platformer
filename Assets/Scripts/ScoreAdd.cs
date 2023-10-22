using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAdd : MonoBehaviour
{
    public int Value = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {         
            ScoreManager.instance.ChangeScore(Value);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        } 
    }
}