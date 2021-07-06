using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임상에서 얻은 문서를 열람할 수 있도록 하는 아카이브 오브젝트
/// 직접 내용을 변경하기 보다 외부 객체에 의해 내용을 변경하도록 유도하였다.
/// !중요!
/// 객체 구조 변경시 반드시 확인 할 것
/// </summary>
public class Archive : MonoBehaviour
{
    [Header("잠금해제 스프라이트")]
    [SerializeField] private Sprite _unlockSprite;
    private GameObject _epilogue;
    private Text _noteText;
    private StoryButton[] _storyButtons;
    private void Awake()
    {
        _epilogue = transform.GetChild(2).gameObject;
        _noteText = transform.GetChild(0).GetComponentInChildren<Text>();   //자식의 자식 UI Text컴포넌트를 가져옴
        _storyButtons = GetComponentsInChildren<StoryButton>(); //자식 객체로부터 노트들을 받아옴
    }

    private void Start()
    {
        PlayerDataManager.Instance.SyncArchiveData(_storyButtons);
        int count = 0;

        foreach (var button in _storyButtons)
        {
            if (button.IsUnlock)
            {
                //노트 언락시 해금 스프라이트로 변경
                button.GetComponent<Image>().sprite = _unlockSprite;
                count++;
            }
        }

        if(_storyButtons.Length <= count && !_epilogue.activeSelf)
        {
            _epilogue.SetActive(true);
            Debug.Log("에필로그");
        }
    }

    public void ShowSelectedNote(string text)
    {
        _noteText.text = text;
    }
}
