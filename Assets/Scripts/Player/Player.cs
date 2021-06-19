using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class Player : MonoBehaviour
{
    #region Components
    [SerializeField] private AudioClip _deadSound;
    [SerializeField] private Image panel;

    private Slider _barFear;
    private Slider _barSonar;
    private GameObject _sonar;

    private AudioClip[] _footsteps;

    private AudioSource _audioSource;
    private PlayerControl _playerControl;
    private GameObject _playerCam;
    private CameraControl _camControl;
    private Animator _animator;
    public Canvas Hud
    {
        get;
        private set;
    }
    #endregion

    public float FearRange
    {
        get;
        set;
    }

    [SerializeField]
    private GameObject quickInventory;

    private readonly float _maxFearRange = 100;//최대 공포 수치, 게임 오버

    [Header("배터리 감소량")]
    [SerializeField] private float use_sonar;

    private float timer = 0;
    private float wait = 0;

    private float sonar_timer = 0;
    private float sonar_cooltime = 5f;
    private bool _isSonar = false;

    private float time = 0f;
    private float timeMax = 0.5f;

    public GameObject LookTarget
    {
        private get;
        set;
    }

    private readonly string _footstepsPath = "Sound/FX/Footsteps";

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerControl = GetComponent<PlayerControl>();
        _playerCam = transform.GetChild(0).gameObject;
        _camControl = transform.GetChild(0).GetComponent<CameraControl>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Hud = GameObject.Find("Canvas_HUD").GetComponent<Canvas>();
        _barFear = Hud.transform.GetChild(2).GetComponent<Slider>();
        _barSonar = Hud.transform.GetChild(3).GetComponent<Slider>();
        FearRange = 0f;
        _barFear.value = FearRange;
        _barFear.maxValue = _maxFearRange;
        _barSonar.value = 100f;
        AudioMgr.Instance.StopSound("BGM_Lobby");
        AudioMgr.Instance.PlaySound("BGM_Stage");
    }

    private void Update()
    {
        if (_isSonar)
        {
            sonar_timer += Time.deltaTime;
            if (sonar_timer >= sonar_cooltime)
            {
                sonar_timer = 0f;
                _isSonar = false;
            }
        }

        if (_playerControl.GetVelocitySpeed() != 0)
        {
            if (!_audioSource.isPlaying && timer >= wait)
            {
                GetComponent<Player>().PlayWalkFootstep();
                timer = 0f;
            }
            timer += Time.deltaTime;
        }

        ActiveSonar();
    }

    private void LateUpdate()
    {
        _barFear.value = FearRange;
        if (_maxFearRange <= FearRange)
        {
            StartCoroutine(PlayerDie());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            LookTarget = collision.transform.GetChild(1).gameObject;
            GetComponent<CapsuleCollider>().isTrigger = false;
            StartButtonAction();
        }
    }

    public void PlayWalkFootstep()
    {
        _footsteps = Resources.LoadAll<AudioClip>(_footstepsPath);
        int i = Random.Range(0, _footsteps.Length - 1);

        _audioSource.clip = _footsteps[i];
        wait = _footsteps[i].length + 0.3f;
        _audioSource.Play();
    }

    public void UseCell(int val)
    {
        _barSonar.value += val;
    }

    private void ActiveSonar()
    {
        _sonar = Resources.Load("Ability/Sonar") as GameObject;
        //소나 사용
        if (Input.GetKeyDown(KeyCode.Space) && _barSonar.IsActive() && !_isSonar)
        {
            _isSonar = true;
            Instantiate(_sonar, transform.position, Quaternion.Euler(new Vector3(90f, 0f)));

            if (0f >= _barSonar.value - use_sonar)
            {
                FearRange -= (_barSonar.value - use_sonar);
                _barFear.value = FearRange;
            }
            _barSonar.value = _barSonar.value - use_sonar;
        }
    }

    private void StartButtonAction()
    {
        if (!_animator.GetBool("IsCaught"))
        {
            Debug.Log("Enter ShakeState");
            _camControl.enabled = false;
            Vector3 TargetFront = LookTarget.transform.forward;
            Vector3 TargetPosition = new Vector3(TargetFront.x, TargetFront.y, TargetFront.z);
            _playerCam.transform.rotation = Quaternion.LookRotation(TargetPosition);
            FindObjectOfType<EventMessage>().DisplayMessage("A + D를 연타하여 탈출");
            _animator.SetBool("IsCaught", true);

            quickInventory.SetActive(false);

        }
    }

    private void PlayerInvincible()
    {
        this.GetComponent<CapsuleCollider>().enabled = false;
        Invoke("PlayerInvincibleEnd", 5.0f);
    }

    private void PlayerInvincibleEnd()
    {
        quickInventory.SetActive(true);
        this.GetComponent<CapsuleCollider>().enabled = true;
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

        _audioSource.clip = _deadSound;
        _audioSource.Play();

        yield return new WaitForSeconds(3f);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }
}
