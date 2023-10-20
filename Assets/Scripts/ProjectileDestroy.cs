using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.CompareTag("MageOrb"))
        {
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("WizardOrb"))
        {
            Destroy(hit.gameObject);
        }
    }
}