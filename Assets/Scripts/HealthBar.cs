using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public GameObject player;

    private Image barImage;

    public AudioClip death;
    AudioSource audioSource;

    public ParticleSystem FXsystem;

    private bool dead = false;
    public const int HEALTH_MAX = 200;
    public float healthAmount;
    public float healthRegenAmount;

    private void Awake()

    {
        player = GameObject.Find("Player");
        barImage = transform.Find("bar").GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        healthAmount = 200;
        healthRegenAmount = 3f;

    }

    private void Update()
    {
        
        barImage.fillAmount = GetHealthNormalized();

        healthAmount += healthRegenAmount * Time.deltaTime;
        healthAmount = Mathf.Clamp(healthAmount, -10f, HEALTH_MAX);

        if (healthAmount <= 0 & dead == false)
        {
            audioSource.PlayOneShot(death, 0.7F);
            GameObject.Instantiate(FXsystem, player.transform.position, Quaternion.identity);
 
            Die();
            dead = true;       
        }      
    }

    private void Die()
    {
             
        Destroy(GameObject.FindWithTag("Player"));
        
        {
            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {           
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
        
    public void RemoveHealth(int amount)
    {
        healthAmount -= amount;
    }

    public void AddHealth(int amount)
    {
        healthAmount += amount;
    }

    public float GetHealthNormalized()
    {
        return healthAmount / HEALTH_MAX;
    }
}  

