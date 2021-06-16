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

    //private string questItmeName;

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
        //questItmeName = _item;
        PlayerPrefs.SetString("Item", _item);
    }

    public string GetQuestItem()
    {
        return PlayerPrefs.GetString("Item");
    }
}
