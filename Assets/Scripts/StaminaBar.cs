using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public GameObject player;

    private Image barImage;

    public Animator m_Animator;

    public const int STAMINA_MAX = 200;
    public float staminaAmount;
    public float staminaRegenAmount;
    public float newstaminaRegenAmount;

    private void Awake()

    {
        player = GameObject.Find("Player");
        barImage = transform.Find("bar").GetComponent<Image>();
      
        staminaAmount = 200;
        staminaRegenAmount = 3f;
    }

    private void Update()
    {

        barImage.fillAmount = GetStaminaNormalized();

        staminaAmount += staminaRegenAmount * Time.deltaTime;
        staminaAmount = Mathf.Clamp(staminaAmount, -10f, STAMINA_MAX);

        if (Input.GetButton("Run") && staminaAmount > 5)
        {
            staminaRegenAmount = -10f;
        }
        else
        {
            staminaRegenAmount = newstaminaRegenAmount;
        }

       // if (Input.GetButtonDown("Jump") && (m_Animator.GetBool("IsJumping" ) == false) && staminaAmount > 20)
       // if (Input.GetButtonDown("Jump") && staminaAmount > 20)
       // {
       //     RemoveStamina(10);
       // }

    }


    public void RemoveStamina(int amount)
    {
        staminaAmount -= amount;
    }

    public void AddStamina(int amount)
    {
        staminaAmount += amount;
    }

    public float GetStaminaNormalized()
    {
        return staminaAmount / STAMINA_MAX;
    }

}  

