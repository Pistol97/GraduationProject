using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class Player : MonoBehaviour
{
    #region Components
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
    private NavmeshPathDraw _navPath;

    [SerializeField] private GameObject subCamera;

    public Canvas Hud
    {
        get;
        private set;
    }

    public FadeEffect Fade
    {
        get
        {
            return panel.GetComponent<FadeEffect>();
        }
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
    private float sonar_cooltime = 6f;
    private bool _isSonar = false;

    private float time = 0f;
    private float timeMax = 1f;

    public GameObject LookTarget
    {
        private get;
        set;
    }

    private readonly string _footstepsPath = "Sound/FX/Footsteps";

    private bool _isDead;

    public Transform JumpPos;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerControl = GetComponent<PlayerControl>();
        _playerCam = transform.GetChild(0).gameObject;
        _camControl = transform.GetChild(0).GetComponent<CameraControl>();
        _animator = GetComponentInChildren<Animator>();
        _navPath = transform.GetChild(1).GetComponent<NavmeshPathDraw>();
        _navPath.gameObject.SetActive(false);
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

        if (0f <= FearRange / _maxFearRange * 100 && 30f > FearRange / _maxFearRange * 100)
        {
            AudioMgr.Instance.StopSound("Heartbeat", 0);
            AudioMgr.Instance.StopSound("Heartbeat", 1);
            AudioMgr.Instance.StopSound("Heartbeat", 2);
            AudioMgr.Instance.StopSound("Panting");
        }

        if (30f <= FearRange / _maxFearRange * 100 && 50 > FearRange / _maxFearRange * 100)
        {
            AudioMgr.Instance.PlaySound("Heartbeat", 0);
        }

        else if (50 <= FearRange / _maxFearRange * 100 && 70 > FearRange / _maxFearRange * 100)
        {
            AudioMgr.Instance.PlaySound("Heartbeat", 1);
        }

        else if(70 < FearRange / _maxFearRange * 100)
        {
            AudioMgr.Instance.PlaySound("Heartbeat", 2);
            AudioMgr.Instance.PlaySound("Panting");
        }

        if (_maxFearRange <= FearRange && !_isDead)
        {
            QuestDataController.GetInstance().SetQuest(0);
            _isDead = true;
            GetComponent<Animator>().SetBool("IsCaught", false);
            GetComponent<CharacterController>().detectCollisions=  false;
            //GetComponent<CapsuleCollider>().enabled = false;

            StartCoroutine(PlayerDie());
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            LookTarget = collision.GetComponentInParent<Enemy>().transform.GetChild(1).gameObject;
            Debug.Log(LookTarget.name);
            GetComponent<CharacterController>().detectCollisions = false;
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
    
    public void UseSyringe(int val)
    {
        FearRange -= val;

        if(0 >= FearRange)
        {
            FearRange = 0;
        }
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

            _navPath.gameObject.SetActive(true);

            Invoke("StopNavLine", 5f);
        }
    }

    private void StopNavLine()
    {
        _navPath.gameObject.SetActive(false);
    }

    private void StartButtonAction()
    {
        if (!_animator.GetBool("IsCaught"))
        {
            Debug.Log("Enter ShakeState");

            _camControl.enabled = false;

            _playerCam.SetActive(false);
            subCamera.SetActive(true);

            ///Vector3 target = new Vector3(_playerCam.transform.position.x, _playerCam.transform.position.y, _playerCam.transform.position.z);

            subCamera.transform.position = _playerCam.transform.position;
            subCamera.transform.LookAt(LookTarget.transform);
            //subCamera.transform.LookAt(target);

            FindObjectOfType<EventMessage>().DisplayMessage("Bashing A + D to escape");
            _animator.SetBool("IsCaught", true);

            quickInventory.SetActive(false);
        }
    }

    public void ResetCamera()
    {
        _playerCam.SetActive(true);
        subCamera.SetActive(false);
    }

    private void PlayerInvincible()
    {
        GetComponent<CharacterController>().detectCollisions = false;
        Invoke("PlayerInvincibleEnd", 5.0f);
    }

    private void PlayerInvincibleEnd()
    {
        quickInventory.SetActive(true);
        GetComponent<CharacterController>().detectCollisions = true;
    }

    private IEnumerator PlayerDie()
    {
        Debug.Log("Player Dead!");
        panel.gameObject.SetActive(true);
        time = 0f;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        Color alpha = panel.color;
        AudioMgr.Instance.PlaySound("PlayerDie");
        //panel.GetComponent<Animator>().Play("Fade", 0)
        while (alpha.a < 1f)
        {
            Debug.Log(alpha.a);
            time += Time.deltaTime / timeMax;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }
}
