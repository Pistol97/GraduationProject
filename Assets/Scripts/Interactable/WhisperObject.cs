using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 획득했을 시 노트를 해금하는 클래스
/// Observer Pattern
/// </summary>
public class WhisperObject : MonoBehaviour, IInteractable, INoteUnlockPublisher
{
    [SerializeField] private int _unlockArchiveNumber;

    #region Prefabs
    [SerializeField]
    private GameObject _noticeUI;

    [SerializeField]
    private GameObject _noticeProgressBar;
    #endregion

    private GameObject _progressBar;

    #region Timer
    //획득시 키 유지시간 타이머의 변수
    private float _currentSec;
    private readonly float _holdSec = 3f;
    #endregion

    private Player _player;
    private Slider _progressSlider;

    //옵저버를 담는 리스트
    private List<INoteUnlockObserver> observers = new List<INoteUnlockObserver>();

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

    public void AddObserver(INoteUnlockObserver observer)
    {
        observers.Add(observer);
    }

    public void DeleteObserver(INoteUnlockObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach(var observer in observers)
        {
            observer.UpdateUnlock(_unlockArchiveNumber);
        }
    }

    public void ObjectInteract()
    {
        Debug.Log("Collect Note");

        Destroy(_progressBar);

        Instantiate(_noticeUI, _player.Hud.transform);

        //아카이브, 플레이어 데이터에 노트 해금 알림
        NotifyObserver();

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
            _progressSlider.value = 0f;
            Destroy(_progressBar);
        }
    }
}
