using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어 데이터 관리 오브젝트
/// 싱글톤 패턴
/// 다른 씬 전환시에도 계속 사용가능
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    /// <summary>
    /// 플레이어의 데이터를 저장하는 클래스
    /// </summary>
    [System.Serializable]
    private class PlayerData
    {
        public bool[] Quests;
        public bool[] Upgrades = new bool[3];
        public bool[] Archives = new bool[9];
    }

    private PlayerData _playerData;

    private static PlayerDataManager _instance = null;
    public static PlayerDataManager Instance
    {
        get
        {
            if (!_instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            _playerData = new PlayerData();
            DontDestroyOnLoad(gameObject);
        }

        else if (this != _instance)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var pData = LoadJsonFile<PlayerData>(Application.dataPath, "PlayerData");   //플레이어 데이터 파일 불러오기
        if (null != pData)
        {
            Debug.Log("PlayerData Load Success");
            _playerData = pData;
        }

        else
        {
            Debug.Log("PlayerData Load Fail!");
        }
    }

    private string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    /// <summary>
    /// 플레이어 데이터 파일을 생성하는 메소드
    /// </summary>
    /// <param name="createPath">생성 경로</param>
    /// <param name="fileName">파일 이름</param>
    /// <param name="jsonData">변환한 jsonData</param>
    private void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        if (null != fileStream)
        {
            Debug.Log("Create File Success");
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
        else
        {
            Debug.Log("Create File Fail!");
            return;
        }
    }

    /// <summary>
    /// 플레이어 데이터 파일을 불러오는 메소드
    /// 주로 플레이어 데이터 클래스 형식을 받으나 추후 다른 클래스의 형태로 사용가능
    /// </summary>
    /// <typeparam name="T">데이터 클래스 자료형</typeparam>
    /// <param name="loadPath">데이터 불러오기 경로</param>
    /// <param name="fileName">파일명 ex) *.json </param>
    /// <returns></returns>
    private T LoadJsonFile<T>(string loadPath, string fileName)
    {
        //파일 검사
        //파일이 존재하지 않을 시 새로 생성
        if (!File.Exists(loadPath + "/PlayerData.json"))
        {
            Debug.Log("File Not Found");
            PlayerData playerData = new PlayerData();
            string json = ObjectToJson(playerData);
            CreateJsonFile(Application.dataPath, "PlayerData", json);
        }

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    /// <summary>
    /// 플레이어 인벤토리 데이터 저장 메소드
    /// TODO: EnergyCell말고 Note아이템이 완성될 시 변경하여 적용 시킬 것
    /// </summary>
    /// <param name="inventory"></param>
    public void GetInventoryData(Inventory inventory)
    {
        int max = _playerData.Archives.Length - 1;
        int i = Random.Range(0, max);

        foreach (var slot in inventory.Slots)
        {
            if (slot.item && "EnergyCell" == slot.item.itemName)
            {
                for (int count = 0; count < slot.itemCount;)
                {
                    if (_playerData.Archives[i])
                    {
                        i = Random.Range(0, max);
                    }
                    else
                    {
                        _playerData.Archives[i] = true;
                        count++;
                    }
                }
            }
        }

        foreach (var slot in inventory.QuickSlots)
        {
            if (slot.item && "EnergyCell" == slot.item.itemName)
            {
                for (int count = 0; count < slot.itemCount;)
                {
                    if (_playerData.Archives[i])
                    {
                        i = Random.Range(0, max);
                    }
                    else
                    {
                        _playerData.Archives[i] = true;
                        count++;
                    }
                }
            }
        }
    }


    /// <summary>
    /// 플레이어 데이터 저장 메소드
    /// </summary>
    public void SavePlayerData()
    {
        string json = ObjectToJson(_playerData);
        CreateJsonFile(Application.dataPath, "PlayerData", json);
    }

    /// <summary>
    /// 플레이어 업그레이드 프로필 연동 메소드
    /// </summary>
    /// <param name="profileNumber"></param>
    public void SyncUpgradeProfile(ref int profileNumber)
    {
        for (int i = 0; i < _playerData.Upgrades.Length; i++)
        {
            if (!_playerData.Upgrades[i])
            {
                Debug.Log(profileNumber);
                return;
            }
            profileNumber = i;
        }
    }

    /// <summary>
    /// 로비 내 업그레이드 데이터 연동 메소드
    /// </summary>
    /// <param name="images"></param>
    public void SyncUpgradeData(UpgradeButton[] upgradeButtons)
    {
        int i = 0;
        foreach (var unlock in _playerData.Upgrades)
        {
            if (unlock)
            {
                upgradeButtons[i].IsUnlock = true;
            }
            i++;
        }
    }

    /// <summary>
    /// 로비 내 아카이브 데이터 연동 메소드
    /// </summary>
    /// <param name="storyButtons"></param>
    public void SyncArchiveData(StoryButton[] storyButtons)
    {
        int i = 0;
        foreach (var unlock in _playerData.Archives)
        {
            if (unlock)
            {
                storyButtons[i].IsUnlock = true;
            }
            i++;
        }

    }
}
