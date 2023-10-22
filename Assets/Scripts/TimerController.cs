using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;    
    public TextMeshProUGUI timeCounter;
    public TextMeshProUGUI timeofdayCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;
    public float elapsedTime;

    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject uiObject3;
    public GameObject uiObject4;
    public GameObject uiObject5;

    private void Awake()
    {
        instance = this;      
    }

    private void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        uiObject3.SetActive(false);
        timeCounter.text = "Time: 00:00.00";
        //timeofdayCounter.text = "Time of Day: Morning";
        timerGoing = true;
    }

    public void BeginTimer()
    {      
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        {
            StartCoroutine(TimeBonus());
        }

        IEnumerator TimeBonus()
        {
            if (elapsedTime < 60.0f)
            {
                yield return new WaitForSeconds(4);
                ScoreManager.instance.ChangeScore(100);
                uiObject4.SetActive(false);
                uiObject1.SetActive(true);
                
                GetComponent<Collider2D>().enabled = false;
            }
            else if (elapsedTime > 60.0f)
            {
                yield return new WaitForSeconds(4);
                ScoreManager.instance.ChangeScore(50);
                uiObject4.SetActive(false);
                uiObject2.SetActive(true);
                GetComponent<Collider2D>().enabled = false;
            }
            else if (elapsedTime > 90.0f)
            {
                yield return new WaitForSeconds(4);
                ScoreManager.instance.ChangeScore(25);
                uiObject4.SetActive(false);
                uiObject3.SetActive(true);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;            
        }     
    }
}