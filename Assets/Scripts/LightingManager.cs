using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class LightingManager : MonoBehaviour
{

    public TextMeshProUGUI timeofdayCounter;
    private TimeSpan timePlaying;
    private bool timerGoing;

    [SerializeField] private UnityEngine.Rendering.Universal.Light2D Light;    
    [SerializeField] private LightingPreset Preset;

    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Start()
    {

        timeofdayCounter.text = "Time of Day:  Morning";
        TimeOfDay = 30;
    }


    private void Update()
    {
        if (Preset == null)
            return;

        if(Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 120;
            UpdateLighting(TimeOfDay / 120);

           
            timePlaying = TimeSpan.FromSeconds(TimeOfDay);

            if (TimeOfDay > 30)
            {
                string timePlayingStr = "Time of Day:  " + "Dawn";
                timeofdayCounter.text = timePlayingStr;
            }
            
            if (TimeOfDay > 60)
            {
                string timePlayingStr = "Time of Day:  " + "Noon";
                timeofdayCounter.text = timePlayingStr;
            }

            if (TimeOfDay > 90)
            {
                string timePlayingStr = "Time of Day:  " + "Dusk";
                timeofdayCounter.text = timePlayingStr;
            }

            if (TimeOfDay > 0 && TimeOfDay  < 30)
            {
                string timePlayingStr = "Time of Day:  " + "Night";
                timeofdayCounter.text = timePlayingStr;
            }





        }

    }
    private void UpdateLighting (float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.ambientLight = Preset.FogColor.Evaluate(timePercent);

        if (Light != null)
        {
           // Light.intensity = TimeOfDay / 10;
                Light.color = Preset.DirectionalColor.Evaluate(timePercent);


        }


    }

    private void OnValidate()
    {
    
    }
}
