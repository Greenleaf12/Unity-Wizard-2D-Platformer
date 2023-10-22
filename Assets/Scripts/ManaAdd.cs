using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManaAdd : MonoBehaviour
{
    public int AddManaAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ManaBarNew>().AddMana(AddManaAmount);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }   
    }
}
