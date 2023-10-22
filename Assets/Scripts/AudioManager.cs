using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float FadeTime = 0.5f;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        Play("Music");
        if (sceneName != "Main Menu")
        {
            Play("Forest");
            Play("Spawn_Player");
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetVolume(string name, float setVolume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = setVolume;
    }

    public void Fade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float startVolume = s.source.volume;

        while (s.source.volume > 0)
        {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;
        }
        {
            StartCoroutine(TimerCoroutinex());
        }

        IEnumerator TimerCoroutinex()
        {
            yield return new WaitForSeconds(FadeTime);
            s.source.volume = startVolume;
            s.source.Stop();           
        }
    }
}
