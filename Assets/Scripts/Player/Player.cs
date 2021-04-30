using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private Slider Bar_Fear;
    [SerializeField] private Slider Bar_Sonar;

    [SerializeField] private GameObject sonar;


    [Header("플레이어 상태 변수")]
    [SerializeField]
    private float fearRange = 0;//공포 수치

    [SerializeField]
    private float maxFearRange = 100;//최대 공포 수치, 게임 오버

    [Header("배터리 감소량")]
    [SerializeField] private float use_sonar;

    private AudioSource audioSource;
    private PlayerControl playerControl;

    private float timer = 0;
    private float wait = 0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
    }

    private void Start()
    {
        //공포 수치 초기화
        Bar_Fear.value = 0f;
        Bar_Sonar.value = 100f;
    }

    private void Update()
    {
        if (playerControl.GetVelocitySpeed() != 0)
        {
            if (!audioSource.isPlaying && timer >= wait)
            {
                GetComponent<Player>().PlayWalkFootstep();
                timer = 0f;
            }
            timer += Time.deltaTime;
        }

        //소나 사용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(sonar, transform.position, Quaternion.Euler(new Vector3(90f, 0f)));

            if(0f >= Bar_Sonar.value - use_sonar)
            {
                Bar_Fear.value -= (Bar_Sonar.value - use_sonar);
            }
            Bar_Sonar.value = Bar_Sonar.value - use_sonar;

            if(Bar_Fear.maxValue <= Bar_Fear.value)
            {
                //플레이어 사망 처리
            }
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

        audioSource.clip = footsteps[footstep];
        wait = footsteps[footstep].length + 0.3f;
        audioSource.Play();
    }
}
