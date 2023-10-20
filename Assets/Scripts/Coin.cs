using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int Value = 100;

    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))

        {
            
            ScoreManager.instance.ChangeGold(Value);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }
    
    }
}
