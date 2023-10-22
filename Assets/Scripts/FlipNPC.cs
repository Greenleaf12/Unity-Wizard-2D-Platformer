using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flips NPC (Skeleton Mage)
public class FlipNPC : MonoBehaviour
{
    public GameObject SkeletonScript;
    private SkeletonMage script;

    void Start()
    {
        script = SkeletonScript.GetComponent<SkeletonMage>();
    }

    void OnTriggerEnter2D(Collider2D turnaround)
    {
        if (turnaround.gameObject.tag == "Player")
        {
                script.Flip();
        }
    }
}