using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임상에서 얻은 문서를 열람할 수 있도록 하는 아카이브 오브젝트
/// 직접 내용을 변경하기 보다 외부 객체에 의해 내용을 변경하도록 유도
/// </summary>
public class Archive : MonoBehaviour
{
    /// <summary>
    /// 노트 데이터 형태를 정의하는 클래스
    /// </summary>
    [System.Serializable]
    private class NoteData
    {
        public int ID = 0;
        public string Context = "";
    }

    /// <summary>
    /// Unity에서 제공하는 JsonUtility는 직렬화가 되어있지 않기 때문에 직접적인 클래스 배열을 읽지 못한다.
    /// 또 다른 클래스로 감싸 직렬화한다.
    /// </summary>
    [System.Serializable]
    private class NoteDataRes
    {
        public NoteData[] NoteDatas = new NoteData[9];
    }

    //NoteData 배열을 담고 있는 객체
    private NoteDataRes _noteDatas;

    //저장될 파일명
    private readonly string _fileName = "ArchiveData";

    [Header("잠금해제 스프라이트")]
    [SerializeField] private Sprite _unlockSprite;

    //아카이브 내의 노트들
    private Note[] _notes;

    //아카이브에 노트 내용을 표시하는 객체
    private Text _noteText;

    private void Awake()
    {
        InitArchive();

        //게임 시작 초기에 비활성화된 상태
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void InitArchive()
    {
        _noteDatas = new NoteDataRes();

        _noteDatas = JsonManager.Instance.LoadJsonFile<NoteDataRes>(Application.dataPath, _fileName);

        //컴포넌트들을 불러옴
        _notes = GetComponentsInChildren<Note>();
        _noteText = transform.GetChild(1).GetComponentInChildren<Text>();

        UnlockNotes();
    }

    //플레이어 데이터에서 해금 내용을 불러와 해금
    private void UnlockNotes()
    {
        int index = 0;

        bool[] unlock = PlayerDataManager.Instance.ArchiveUnlockData;

        foreach (var note in _notes)
        {
            if (unlock[index])
            {
                note.GetComponent<Image>().sprite = _unlockSprite;
                note.IsUnlock = unlock[index];
                note.Context = _noteDatas.NoteDatas[index].Context;
            }

            index++;
        }
    }

    public void ShowSelectedNote(string text)
    {
        _noteText.text = text;
    }
}
