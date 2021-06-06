using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아카이브에서 해금된 노트를 확인 할 수 있는 버튼 오브젝트
/// 버튼 클릭시 아카이브 오브젝트에 해당 노트의 내용을 전달하여 출력할 수 있게끔 설계
/// </summary>
public class StoryButton : MonoBehaviour
{
    [Header("해당 노트의 스토리를 작성")]
    [TextArea]
    [SerializeField] private string _story;

    [Header("잠금해제 스프라이트")]
    [SerializeField]private Sprite unlockSprite;

    #region Components
    private Image _noteImage;
    private Button _button;
    private Archive _archive;
    #endregion

    //TODO: 테스트용 SerializeField 제거
    [SerializeField]private bool _isUnlock;

    private void Awake()
    {
        _noteImage = GetComponent<Image>();
        _button = GetComponent<Button>();
        _archive = GetComponentInParent<Archive>();
    }

    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked); //버튼 클릭 이벤트 추가

        //노트 언락시 해금 스프라이트로 변경
        if (_isUnlock)
        {
            _noteImage.sprite = unlockSprite;
        }
    }

    /// <summary>
    /// 버튼 클릭시의 동작을 담당
    /// </summary>
    private void ButtonClicked()
    {
        if (_isUnlock)
        {
            _archive.ShowSelectedNote(_story);  //언락시 해당 노트의 내용을 메소드를 통해 전달
        }
        else
        {
            _archive.ShowSelectedNote("잠겨있음");
        }
    }
}
