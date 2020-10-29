using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroScript : MonoBehaviour
{
    public AudioSource[] audioSources;

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void StartTheme()
    {
        FindObjectOfType<AudioManager>().Play("theme");
    }

    public void Play(string name)
    {
        AudioSource s = Array.Find(audioSources, sound => sound.clip.name == name);
        s.Play();
    }

}
