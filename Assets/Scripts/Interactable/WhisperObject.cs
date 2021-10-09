using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhisperObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _noticeUI;

    [SerializeField]
    private GameObject _noticeProgressBar;

    private GameObject _bar;

    private float _currentSec;
    private readonly float _holdSec = 3f;

    private Slider _slider;

    private Player _player;

    private void Awake()
    {



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_player)
            {
                _player = other.GetComponent<Player>();
            }

            other.GetComponentInChildren<SelectionManager>().ActionText.gameObject.SetActive(true);
            other.GetComponentInChildren<SelectionManager>().ActionText.text = "<color=yellow>" + " (F) " + "</color>" + "누른채 유지";

            //몇 초간 키 연속 입력
            if (Input.GetKey(KeyCode.F) && _holdSec >= _currentSec)
            {
                if(!_bar)
                {
                    _bar = Instantiate(_noticeProgressBar, _player.Hud.transform);

                    _slider = _bar.GetComponent<Slider>();
                    _slider.value = 0f;
                    _slider.maxValue = _holdSec;
                }

                _currentSec += Time.deltaTime;

                _slider.value += _currentSec * Time.deltaTime;

                if (_holdSec <= _currentSec)
                {
                    ObjectInteract();
                }
            }

            //키를 땠을 때 예외 처리
            if (Input.GetKeyUp(KeyCode.F))
            {
                _currentSec = 0f;
                Destroy(_bar);
            }
        }
    }

    public void ObjectInteract()
    {
        Debug.Log("Collect Note");

        Destroy(_bar);

        Instantiate(_noticeUI, _player.Hud.transform);

        //아카이브에 노트 추가
        Destroy(gameObject, 0f);
    }
}
