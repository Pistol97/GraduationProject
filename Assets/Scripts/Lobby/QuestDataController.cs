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

    public void SetQuest(int _ox)
    {
        PlayerPrefs.SetInt("QuestComplete", _ox);
    }

    public int GetQuest()
    {
        return PlayerPrefs.GetInt("QuestComplete");
    }

    public void SetQuestItem(string _item)
    {
        PlayerPrefs.SetString("Item", _item);
    }

    public string GetQuestItem()
    {
        return PlayerPrefs.GetString("Item");
    }

    /// <summary>
    /// NPC 퀘스트 번호
    /// </summary>

    //public void SetNPC1QuestNum(int num)
    //{
    //    PlayerPrefs.SetInt("NPC1QuestNum", num);
    //}

    //public int GetNPC1QuestNum()
    //{
    //    return PlayerPrefs.GetInt("NPC1QuestNum");
    //}

    //public void SetNPC2QuestNum(int num)
    //{
    //    PlayerPrefs.SetInt("NPC2QuestNum", num);
    //}

    //public int GetNPC2QuestNum()
    //{
    //    return PlayerPrefs.GetInt("NPC2QuestNum");
    //}

    //public void SetNPC3QuestNum(int num)
    //{
    //    PlayerPrefs.SetInt("NPC3QuestNum", num);
    //}

    //public int GetNPC3QuestNum()
    //{
    //    return PlayerPrefs.GetInt("NPC3QuestNum");
    //}
}
