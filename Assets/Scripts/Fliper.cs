using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flips NPC (Bats) 

public class Fliper : MonoBehaviour
{
    private GameObject player;

    public GameObject EnemyObject;
    private FlyingEnemy script;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        script = EnemyObject.GetComponent<FlyingEnemy>();
    }

    void Update()
    {
        if (EnemyObject != null && player != null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (EnemyObject != null && player != null)
        {
            if (transform.position.x > player.transform.position.x)
                transform.localScale = new Vector2(1.0f, 1.0f);

            if (transform.position.x < player.transform.position.x)
                transform.localScale = new Vector2(-1.0f, 1.0f);
        }
    }
}
