using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    private class Sound
    {
        //효과음 명
        public string Name;
        //사운드 파일을 담는 배열
        public AudioClip[] Clips;

        //소리 크기
        [Range(0f, 1f)]
        public float Volume;

        //음높이
        [Range(.1f, 3f)]
        public float Pitch;
        //반복 재생
        public bool IsLoop;
        //재생할 AudioSource 컴포넌트
        public AudioSource source;
    }

    [SerializeField] private Sound[] sounds;

    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                return null;
            }
            return _instance;
        }
    }

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
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.IsLoop;
        }
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        if (!s.source.isPlaying)
        {
            Debug.Log("Play sound: " + name);
            s.source.clip = s.Clips[0];
            s.source.Play();
        }
    }

    // 단계적인 소리 재생에 사용 ex)심장박동
    public void PlayStepSound(string name , int index)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        if (!s.source.isPlaying || (s.source.isPlaying && s.source.clip != s.Clips[index]))
        {
            Debug.Log("Play sound: " + name + index);
            s.source.clip = s.Clips[index];
            s.source.Play();
        }
    }

    public void PlayRandomSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        int r = UnityEngine.Random.Range(0, s.Clips.Length - 1);
        s.source.clip = s.Clips[r];
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        if (s.source.isPlaying)
        {
            Debug.Log("Stop sound: " + name);
            s.source.clip = s.Clips[0];
            s.source.Stop();
        }
    }

    public void StopSound(string name, int index)
    {
        Sound s = Array.Find(sounds, Sound => Sound.Name == name);
        if (s.source.isPlaying)
        {
            Debug.Log("Stop sound: " + name + index);
            //s.source.clip = s.Clips[index];
            s.source.Stop();
        }
    }
}
