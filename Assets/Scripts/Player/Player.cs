using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip dead_sound;
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

    [SerializeField] private Image panel;

    private float timer = 0;
    private float wait = 0;

    private float sonar_timer = 0;
    private float sonar_cooltime = 7f;
    private bool isSonar = false;

    private float time = 0f;
    private float timeMax = 0.5f;
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
        if(Input.GetKeyDown(KeyCode.Space) && Bar_Sonar.IsActive() && !isSonar)
        {
            isSonar = true;
            Instantiate(sonar, transform.position, Quaternion.Euler(new Vector3(90f, 0f)));

            if(0f >= Bar_Sonar.value - use_sonar)
            {
                Bar_Fear.value -= (Bar_Sonar.value - use_sonar);
            }
            Bar_Sonar.value = Bar_Sonar.value - use_sonar;

            if(Bar_Fear.maxValue <= Bar_Fear.value)
            {
                StartCoroutine(PlayerDie());
                Debug.Log("플레이어 사망!");
            }
        }

        if(isSonar)
        {
            sonar_timer += Time.deltaTime;
            if(sonar_timer >= sonar_cooltime)
            {
                sonar_timer = 0f;
                isSonar = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            StartCoroutine(PlayerDie());
        }
    }
    public void PlayWalkFootstep()
    {
        int footstep = Random.Range(0, footsteps.Length - 1);

        audioSource.clip = footsteps[footstep];
        wait = footsteps[footstep].length + 0.3f;
        audioSource.Play();
    }

    public void UseCell(int val)
    {
        Bar_Sonar.value += val;
    }

    private IEnumerator PlayerDie()
    {
        Debug.Log("플레이어 사망!");
        panel.gameObject.SetActive(true);
        time = 0f;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        time = 0f;

        audioSource.clip = dead_sound;
        audioSource.Play();

        yield return new WaitForSeconds(3f);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }
}
