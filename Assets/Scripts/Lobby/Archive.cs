using System.IO;
using System.Text;
using System.Collections.Generic;
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
    [System.Serializable]
    private class NoteData
    {
        public int ID = 0;
        public bool IsUnlock = false;
        public string Context = "";
    }

    [System.Serializable]
    private class NoteDataRes
    {
        public NoteData[] NoteDatas = new NoteData[9];
    }

    private readonly string _fileName = "ArchiveData";

    [Header("잠금해제 스프라이트")]
    [SerializeField] private Sprite _unlockSprite;

    private StoryButton[] _storyButtons;
    private Text _noteText;

    //private List<uint> _ids;

    private List<NoteData> notes = new List<NoteData>(9);

    private Dictionary<uint, NoteData> _datas;

    private void Awake()
    {
        //NoteDataRes datas = new NoteDataRes();

        //string json = JsonManager.Instance.ObjectToJson(datas);

        //JsonManager.Instance.CreateJsonFile(Application.dataPath, _fileName, json);

        _storyButtons = GetComponentsInChildren<StoryButton>(); //자식 객체로부터 노트들을 받아옴, 배열
        _noteText = transform.GetChild(1).GetComponentInChildren<Text>();   //아카이브 메뉴 상에서 노트 내용을 표시하는 텍스트
        
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
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
    }

    public void SetUnlock(int num)
    {
        _storyButtons[num].GetComponent<Image>().sprite = _unlockSprite;
        _storyButtons[num].IsUnlock = true;
    }

    public void ShowSelectedNote(string text)
    {
        _noteText.text = text;
    }
}
