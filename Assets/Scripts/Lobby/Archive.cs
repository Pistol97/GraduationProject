using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임상에서 얻은 문서를 열람할 수 있도록 하는 아카이브 오브젝트
/// Observer Pattern
/// </summary>
public class Archive : MonoBehaviour, INoteUnlockObserver
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
    private readonly string _fileName = "NoteData";

    [Header("잠금해제 스프라이트")]
    [SerializeField] private Sprite _unlockSprite;

    //아카이브 내의 노트들
    private Note[] _notes;

    //아카이브에 노트 내용을 표시하는 객체
    private Text _noteText;

    public void InitArchive()
    {
        _noteDatas = new NoteDataRes();

        //노트 데이터를 JSON으로부터 가져옴
        _noteDatas = JsonManager.Instance.LoadJsonFile<NoteDataRes>(Application.dataPath, _fileName);

        //필요한 하위 컴포넌트들을 불러옴
        _notes = GetComponentsInChildren<Note>();
        _noteText = transform.GetChild(1).GetComponentInChildren<Text>();

        SetNoteData();

        //비활성화
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    //인터페이스 메소드
    //Subject로부터 알림을 받아 노트 해금
    public void UpdateUnlock(int noteNumber)
    {
        //배열 인덱스에 맞춰 -1
        UnlockNote(noteNumber - 1);
    }

    private void UnlockNote(int index)
    {
        _notes[index].GetComponent<Image>().sprite = _unlockSprite;
        _notes[index].IsUnlock = true;
    }

    //플레이어 데이터에서 해금 내용을 불러와 해금
    private void SetNoteData()
    {
        int index = 0;

        bool[] unlock = PlayerDataManager.Instance.ArchiveUnlockData;

        foreach (var note in _notes)
        {
            if (unlock[index])
            {
                UnlockNote(index);
            }
            //노트 내용 입력
            note.Context = _noteDatas.NoteDatas[index].Context;
            index++;
        }
    }

    public void ShowSelectedNote(string text)
    {
        _noteText.text = text;
    }
}
