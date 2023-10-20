using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthAdd : MonoBehaviour
{
    public int AddHealthAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))

        {
            FindObjectOfType<HealthBar>().AddHealth(AddHealthAmount);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }
    
    }
}
