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
    /// 퀘스트 성공 여부
    /// </summary>
    public void SetQuest(int _ox)
    {
        PlayerPrefs.SetInt("QuestComplete", _ox);
    }

    public int GetQuest()
    {
        return PlayerPrefs.GetInt("QuestComplete",0);
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
}
