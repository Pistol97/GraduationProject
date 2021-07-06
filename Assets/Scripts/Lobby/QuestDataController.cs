using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataController : MonoBehaviour
{
    /// <summary>
    /// 싱글턴
    /// </summary>
    private static QuestDataController instance;
    public static QuestDataController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<QuestDataController>();

            if (instance == null)
            {
                GameObject container = new GameObject("QuestDataController");

                instance = container.AddComponent<QuestDataController>();
            }
        }
        return instance;
    }
    /// <summary>
    /// 퀘스트 성공 여부 0 = false, 1 = true
    /// </summary>
    public void SetQuest(int _bool)
    {
        PlayerPrefs.SetInt("QuestComplete", _bool);
    }

    public int GetQuest()
    {
        return PlayerPrefs.GetInt("QuestComplete");
    }
    /// <summary>
    /// 퀘스트 아이템 이름
    /// </summary>
    public void SetQuestItem(string _item)
    {
        PlayerPrefs.SetString("Item", _item);
    }

    public string GetQuestItem()
    {
        return PlayerPrefs.GetString("Item");
    }

    /// <summary>
    /// 현재 진행중인 퀘스트 넘버
    /// </summary>
    public void SetCurrentQuestNpcNum(int _num)
    {
        PlayerPrefs.SetInt("QuestNpcNum",_num);
    }

    public int GetCurrentQuestNpcNum()
    {
        return PlayerPrefs.GetInt("QuestNpcNum",1);
    }
    /// <summary>
    /// 게임 클리어 여부 0 = false, 1 = true
    /// </summary>
    public void SetGameClear(int _bool)
    {
        PlayerPrefs.SetInt("GameClear", _bool);
    }
    public int GetGameClear()
    {
        return PlayerPrefs.GetInt("GameClear");
    }
    /// <summary>
    /// npc의 퀘스트 진행 내역
    /// </summary>
    public void SetNpc1QuestNum(int _num)
    {
        PlayerPrefs.SetInt("npc1Quest", _num);
    }
    public int GetNpc1QuestNum()
    {
        return PlayerPrefs.GetInt("npc1Quest");
    }
    public void SetNpc2QuestNum(int _num)
    {
        PlayerPrefs.SetInt("npc2Quest", _num);
    }
    public int GetNpc2QuestNum()
    {
        return PlayerPrefs.GetInt("npc2Quest");
    }
    public void SetNpc3QuestNum(int _num)
    {
        PlayerPrefs.SetInt("npc3Quest", _num);
    }
    public int GetNpc3QuestNum()
    {
        return PlayerPrefs.GetInt("npc3Quest");
    }
}
