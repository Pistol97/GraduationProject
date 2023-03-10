using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임상에서 얻은 문서를 열람할 수 있도록 하는 아카이브 오브젝트
/// Observer Pattern
/// </summary>
public class Archive : MonoBehaviour, INoteUnlockObserver
{
    /// <summary>
    /// 노트 데이터 클래스
    /// </summary>
    [System.Serializable]
    private class NoteData
    {
        public int ID = 0;
        public string Context = "";
    }

    /// <summary>
    /// Unity에서 제공하는 JsonUtility는 직렬화가 되어있지 않기 때문에 클래스 배열을 직접 읽지 못한다.
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

        //노트 데이터 입력
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

    /// <summary>
    /// 해당하는 인덱스의 노트를 아카이브에서 해금함
    /// </summary>
    /// <param name="index">해당 노트 인덱스</param>
    private void UnlockNote(int index)
    {
        //해금 스프라이트 교체
        _notes[index].GetComponent<Image>().sprite = _unlockSprite;
        //해당 노트 해금
        _notes[index].IsUnlock = true;
    }

    /// <summary>
    /// 플레이어 데이터에서 해금 사항 불러오기 및 노트 내용 입력
    /// </summary>
    private void SetNoteData()
    {
        int index = 0;

        //저장된 플레이어 데이터 중 해금 여부를 불러옴
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
