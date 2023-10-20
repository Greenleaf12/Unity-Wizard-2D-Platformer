using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject);
        }
    }
}
