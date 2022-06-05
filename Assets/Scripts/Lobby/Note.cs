using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아카이브에서 해금된 노트를 확인 할 수 있는 버튼 오브젝트
/// 버튼 클릭시 아카이브에 해당 노트의 내용을 전달하여 출력
/// </summary>
public class Note : MonoBehaviour
{
    private string _context;

    public string Context
    {
        set
        {
            _context = value;
        }
    }

    #region Components
    private Button _button;
    private Archive _archive;
    #endregion

    public bool IsUnlock
    {
        get;
        set;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _archive = GetComponentInParent<Archive>();
    }

    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked); //버튼 클릭 이벤트 추가
    }

    /// <summary>
    /// 버튼 클릭시의 동작을 담당
    /// </summary>
    private void ButtonClicked()
    {
        if (IsUnlock)
        {
            //언락시 해당 노트의 내용을 메소드를 통해 전달
            _archive.ShowSelectedNote(_context);
        }
        else
        {
            _archive.ShowSelectedNote("Locked");
        }
    }
}
