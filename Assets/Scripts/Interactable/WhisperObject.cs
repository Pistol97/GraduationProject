using UnityEngine;
using UnityEngine.UI;

public class WhisperObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _unlockArchiveNumber;

    #region Prefabs
    [SerializeField]
    private GameObject _noticeUI;

    [SerializeField]
    private GameObject _noticeProgressBar;
    #endregion

    private GameObject _progressBar;

    //획득 유지시간 타이머의 변수
    private float _currentSec;
    private readonly float _holdSec = 3f;

    private Player _player;
    private Slider _progressSlider;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_player)
            {
                _player = other.GetComponent<Player>();
            }

            CollectWhispers(_player);
        }
    }

    public void ObjectInteract()
    {
        Debug.Log("Collect Note");

        Destroy(_progressBar);

        Instantiate(_noticeUI, _player.Hud.transform);

        //아카이브에 노트 추가
        //옵저버 패턴을 활용 해볼까

        //_player.GetComponentInChildren<PlayerDataManager>().UnlockArchive(_unlockArchiveNumber);

        Destroy(gameObject, 0f);
    }

    private void CollectWhispers(Player player)
    {
        _player.GetComponentInChildren<SelectionManager>().ActionText.gameObject.SetActive(true);
        _player.GetComponentInChildren<SelectionManager>().ActionText.text = "Hold" + "<color=yellow>" + " (F) " + "</color>";

        //몇 초간 키 연속 입력
        if (Input.GetKey(KeyCode.F) && _holdSec >= _currentSec)
        {
            if (!_progressBar)
            {
                _progressBar = Instantiate(_noticeProgressBar, _player.Hud.transform);

                _progressSlider = _progressBar.GetComponent<Slider>();
                _progressSlider.value = 0f;
                _progressSlider.maxValue = _holdSec;
            }

            _currentSec += Time.deltaTime;

            _progressSlider.value += _currentSec * Time.deltaTime;

            if (_holdSec <= _currentSec)
            {
                ObjectInteract();
            }
        }

        //키를 땠을 때 예외 처리
        if (Input.GetKeyUp(KeyCode.F))
        {
            _currentSec = 0f;
            Destroy(_progressBar);
        }
    }
}
