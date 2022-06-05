using UnityEngine;

/// <summary>
/// 플레이어 데이터 관리 클래스
/// Singleton Pattern, Observer Pattern
/// </summary>
public class PlayerDataManager : NoteUnlockObserver
{
    /// <summary>
    /// 플레이어의 데이터 형태
    /// </summary>
    [System.Serializable]
    private class PlayerData
    {
        public bool[] ArchiveUnlock = new bool[9];
    }

    private PlayerData _playerData;

    //아카이브 연동을 하기 위한 프로퍼티
    public bool[] ArchiveUnlockData
    {
        get
        {
            return _playerData.ArchiveUnlock;
        }
    }

    //저장될 파일명
    private readonly string _fileName = "PlayerData";

    #region Singleton
    private static PlayerDataManager _instance = null;
    public static PlayerDataManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new PlayerDataManager();
            }

            return _instance;
        }
    }
    #endregion

    //퍼블리셔로부터 알림을 받아 노트 해금
    public void UpdateUnlock(int noteNumber)
    {
        //해당 노트 해금
        //배열 인덱스에 맞춰 -1
        _playerData.ArchiveUnlock[noteNumber - 1] = true;
        //갱신된 데이터 저장
        CreatePlayerData();
    }

    public void InitPlayerData()
    {
        _playerData = new PlayerData();

        //플레이어 데이터 불러오기에 성공했을 경우
        if (default != JsonManager.Instance.LoadJsonFile<PlayerData>(Application.dataPath, _fileName))
        {
            _playerData = JsonManager.Instance.LoadJsonFile<PlayerData>(Application.dataPath, _fileName);
        }
        
        //플레이어 데이터 불러오기에 실패 했을시
        else
        {
            Debug.Log("Load PlayerData Fail!");
            CreatePlayerData();
        }
    }

    public void CreatePlayerData()
    {
        string json = JsonManager.Instance.ObjectToJson(_playerData);
        JsonManager.Instance.CreateJsonFile(Application.dataPath, "PlayerData", json);
    }
}
