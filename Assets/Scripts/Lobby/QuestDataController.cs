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

    //[SerializeField] private int[,] questProgress;

    //[SerializeField] private int npcnum1;
    //[SerializeField] private int npcnum2;
    //[SerializeField] private int npcnum3;
    //[SerializeField] private int npcquestnum1;
    //[SerializeField] private int npcquestnum2;
    //[SerializeField] private int npcquestnum3;

    //private void Awake()
    //{
    //    questProgress = new int[2,2];
    //}

    //public void QuestSetComplete()
    //{
    //    PlayerPrefs.SetInt("QuestComplete", 0);
    //}

    //public void QuestSetFail()
    //{
    //    PlayerPrefs.SetInt("QuestComplete", 1);
    //}

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
}
