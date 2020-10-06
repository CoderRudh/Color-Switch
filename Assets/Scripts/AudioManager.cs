using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    //public Sound[] sounds;
    public AudioSource[] audioSources;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        /*
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        */
    }

    void Start()
    {
        Play("theme");
    }

    public void StopMusic()
    {
        /*
        Sound s = Array.Find(sounds, sound => sound.name == "theme");
        s.source.mute = true;
        */
        AudioSource s = Array.Find(audioSources, sound => sound.clip.name == "theme");
        s.mute = true;
    }

    public void PlayMusic()
    {
        /*
        Sound s = Array.Find(sounds, sound => sound.name == "theme");
        s.source.mute = false;
        */
        AudioSource s = Array.Find(audioSources, sound => sound.clip.name == "theme");
        s.mute = false;
    }

    public void StopSounds()
    {
        /*
        Sound[] s = Array.FindAll(sounds, sound => sound.name != "theme");
        foreach (Sound sound in s)
            sound.source.mute = true;
        */
        AudioSource[] s = Array.FindAll(audioSources, sound => sound.clip.name != "theme");
        foreach (AudioSource sound in s)
            sound.mute = true;
    }

    public void PlaySounds()
    {
        /*
        Sound[] s = Array.FindAll(sounds, sound => sound.name != "theme");
        foreach (Sound sound in s)
            sound.source.mute = false;
        */
        AudioSource[] s = Array.FindAll(audioSources, sound => sound.clip.name != "theme");
        foreach (AudioSource sound in s)
            sound.mute = false;
    }

    public void Play(string name)
    {
        /*
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
        */
        AudioSource AS = Array.Find(audioSources, sound => sound.clip.name == name);
        AS.Play();
    }
}
