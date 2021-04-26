using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;

    [SerializeField] private Slider Bar_Fear;

    [Header("플레이어 상태 변수")]
    [SerializeField]
    private float fearRange = 0;//공포 수치

    [SerializeField]
    private float maxFearRange = 100;//최대 공포 수치, 게임 오버

    private PlayerControl playerControl;

    private float timer = 0;
    private float wait = 0;
    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    private void Start()
    {
        //공포 수치 초기화
        Bar_Fear.value = 0f;
    }

    private void Update()
    {
        if (playerControl.GetVelocitySpeed() != 0)
        {
            if (!GetComponent<AudioSource>().isPlaying && timer >= wait)
            {
                GetComponent<Player>().PlayWalkFootstep();
                timer = 0f;
            }
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IncreaseFear();

            Debug.Log(fearRange);
        }
    }

    private void IncreaseFear()
    {
        fearRange += 10;
        Bar_Fear.value = fearRange / maxFearRange;
    }

    public void PlayWalkFootstep()
    {
        int footstep = Random.Range(0, footsteps.Length - 1);

        GetComponent<AudioSource>().clip = footsteps[footstep];
        wait = footsteps[footstep].length + 0.3f;
        GetComponent<AudioSource>().Play();
        Debug.Log("Footsteps");
    }
}
