using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Simple dont destroy object
public class DontDestroy : MonoBehaviour
{
    public GameObject uiObject;

    void Awake()
        {
            DontDestroyOnLoad(uiObject);
        }  
}
