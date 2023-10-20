using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    public float timeLeft;
    public float timeRight;
    public float platformSpeedX;
    private bool moveLeft;

    public Transform point1;
    public Transform point2;



    void Start()
    {
       move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             collision.collider.transform.SetParent(null);
        }
    }

    void Update()
    {

        if (moveLeft == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1.position, platformSpeedX * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 7);
        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, point2.position, platformSpeedX * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 7);
        }
          
    }

    void move()
    {
        {
            StartCoroutine(TimerCoroutinex());
        }
        IEnumerator TimerCoroutinex()
        {
            moveLeft = true;              
            // Wait 
            yield return new WaitForSeconds(timeLeft);
            moveRight();
        }
    }

    void moveRight()
    {     
        {
            StartCoroutine(TimerCoroutinex());
        }
        IEnumerator TimerCoroutinex()
        {
            moveLeft = false;
            // Wait 
            yield return new WaitForSeconds(timeRight);
            move();
        }
    }
}
