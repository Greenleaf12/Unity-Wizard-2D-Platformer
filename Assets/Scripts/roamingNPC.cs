using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roamingNPC : MonoBehaviour
{
    public Transform startingPoint;
    public float speed;
    public float XYpos;
    // Cooldown for Random Numbers
    public float cooldownTime2 = 10.0f;
    private float nextFireTime2 = 0f;

    private float cordX; // Random X
    private float cordY; // Random Y

    void Start()
    {
        ReturnHome();
    }

    void Update()
    {
        ReturnHome();

        // Fire Random Number
        if (Time.time > nextFireTime2)
        {
            randomNumber();
            nextFireTime2 = Time.time + cooldownTime2;
            Debug.Log(nextFireTime2);
        }
    }

    private void ReturnHome()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.transform.position + new Vector3(cordX, cordY, 0.0f), speed * Time.deltaTime);
    }

    private void randomNumber()
    {
        cordX = Random.Range(-XYpos, XYpos);
        cordY = Random.Range(-XYpos, XYpos);
    }
}
