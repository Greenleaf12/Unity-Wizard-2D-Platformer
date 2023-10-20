using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static string globalUserName = "New Player";
    [SerializeField]  private TMP_InputField input;
   
    private void Awake()
    {
        //input = GameObject.Find ("InputField").GetComponent<TMP_InputField>();
    }

    public void GetInput (string name)
    {
        globalUserName = name;
    }
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("Music");

    }

    public void PlayGame()

    {
        Debug.Log("Name is" + globalUserName);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
