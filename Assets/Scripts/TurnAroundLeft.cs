using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundLeft : MonoBehaviour
{
    public GameObject SkeletonScript;
    public SkeletonNew script;
    public GameObject player;


    void Start()
    {
        script = SkeletonScript.GetComponent<SkeletonNew>();

    }

/*
    private void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            flipLeft();
        }
        else 
        { 
            flipRight();
        }
    }*/

    void OnTriggerEnter2D(Collider2D turnaround)
    {
        if (turnaround.gameObject.tag == "Player")
        {
            if (script.facingRight == true)
            {
                flipLeft();
                return;
            }
            if (script.facingLeft == true)
            {
                flipRight();
                return;
            }


        }
    }

    void flipRight()
    {
        
        script.facingLeft = false;
        script.facingRight = true;

        script.Move();


    }

    void flipLeft()
    {
        script.facingLeft = true;
        script.facingRight = false;

        script.Move();


    }
}
