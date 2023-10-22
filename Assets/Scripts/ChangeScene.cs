using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour

{
    private int ScoreUpdate;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {       
            yield return new WaitForSeconds(6);
            ScoreUpdate = ScoreManager.score;
            FindObjectOfType<HighscoreTable2>().AddHighscoreEntry(ScoreUpdate, "NEW");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
