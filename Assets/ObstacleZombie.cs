using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleZombie : MonoBehaviour
{
    [SerializeField]private AudioClip[] _audios;

    private readonly float _wait = 3f;
    private float _timer;


    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_wait <= _timer)
        {
            _timer = 0;
            GetComponent<AudioSource>().clip = _audios[Random.Range(0, _audios.Length - 1)];
            GetComponent<AudioSource>().Play();
        }
    }
}
