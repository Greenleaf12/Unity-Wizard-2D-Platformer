using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    public GameObject SkeletonScript;
    private SkeletonNew script;
    private GameObject player;

    void Start()
    {
        script = SkeletonScript.GetComponent<SkeletonNew>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D turnaround)
    {

        if (turnaround.gameObject.tag == "Player")
        {
            if (transform.position.x > player.transform.position.x)
            {
                script.facingLeft = true;
                script.facingRight = false;
            }

            if (transform.position.x < player.transform.position.x)
            {
                script.facingRight = true;
                script.facingLeft = false;
            }

                

        }
    }
}
