using UnityEngine;
using UnityEngine.UI;

public class EventMessage : MonoBehaviour
{
    #region Variables
    private bool _isStartBlur;

    private float _disappearTimer = 0f;
    private readonly float _disappearTimerMax = 5f;
    #endregion

    #region Components
    private Text _dialogue;

    private Color _alpha;
    private Color _originColor;
    #endregion

    private void Awake()
    {
        _dialogue = GetComponent<Text>();

        //원본 텍스트 컬러 저장
        _originColor = new Color(
            _dialogue.color.r,
            _dialogue.color.g,
            _dialogue.color.b,
            _dialogue.color.a);
        _alpha = _originColor;  //alpha값 초기화
    }

    private void LateUpdate()
    {
        if (_isStartBlur)
        {
            Blurring();
        }

    }

    public void DisplayMessage(string message)
    {
        _dialogue.text = message;
        _dialogue.color = _originColor;
        _disappearTimer = 0f;
        _isStartBlur = true;
    }

    public void Blurring()
    {
        if (0f == _dialogue.color.a)
        {
            _isStartBlur = false;
            return;
        }

        _disappearTimer += Time.deltaTime;

        //alpha값 보간
        _alpha.a = Mathf.Lerp(_disappearTimerMax, 0f, _disappearTimer / _disappearTimerMax);    
        _dialogue.color = _alpha;
    }
}
