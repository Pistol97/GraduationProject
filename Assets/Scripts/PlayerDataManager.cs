using System.IO;
using System.Text;
using UnityEngine;


/// <summary>
/// 플레이어 데이터 관리 클래스
/// Singleton Pattern
/// </summary>
public class PlayerDataManager
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

    public void InitPlayerData()
    {
        _playerData = new PlayerData();

        if (default != JsonManager.Instance.LoadJsonFile<PlayerData>(Application.dataPath, "PlayerData"))
        {
            _playerData = JsonManager.Instance.LoadJsonFile<PlayerData>(Application.dataPath, "PlayerData");
        }
        
        else
        {
            Debug.Log("Load PlayerData Fail!");
            string json = JsonManager.Instance.ObjectToJson(_playerData);
            JsonManager.Instance.CreateJsonFile(Application.dataPath, "PlayerData", json);
        }
    }

    public void SavePlayerData()
    {
        string json = JsonManager.Instance.ObjectToJson(_playerData);
        JsonManager.Instance.CreateJsonFile(Application.dataPath, "PlayerData", json);
    }

    //상태 변경 함수
    //구독한 아카이브에 변경됨을 알림
}
