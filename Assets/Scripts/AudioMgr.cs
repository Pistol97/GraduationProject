﻿using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMgr : MonoBehaviour
{
    [System.Serializable]
    private class Sound
    {
        public string Name;

        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume;

        [Range(.1f, 3f)]
        public float Pitch;

        public bool IsLoop;

        public AudioSource source;
    }

    private static AudioMgr _instance = null;
    public static AudioMgr Instance
    {
        get
        {
            if(!_instance)
            {
                return null;
            }
            return _instance;
        }
    }

    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (this != _instance)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.IsLoop;
        }
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        s.source.Play();
    }
}