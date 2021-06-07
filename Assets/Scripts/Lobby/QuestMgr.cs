using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMgr : MonoBehaviour
{
    [System.Serializable]
    public class QuestDataProperty
    {
        public int npcQuestNum;
        public Sprite sprite;
        public string description;
        public string questItem;
        public string reward;
    }

    public Image npc;
    public Text npcQuestNum;
    public Text description;
    public Button back;
    
    public List<QuestDataProperty> npc1Quest;
    public List<QuestDataProperty> npc2Quest;
    public List<QuestDataProperty> npc3Quest;

    //public List<List<QuestDataProperty>> Quest;

    public int npc1QuestNum = 0;
    public int npc2QuestNum = 0;
    public int npc3QuestNum = 0;

    public int currentNpc;

    public bool test=false;

    Dictionary<int, List<QuestDataProperty>> testQuest;

    private void Awake()
    {
        QuestDataController.GetInstance().SetQuest(0);

        //Quest = new List<List<QuestDataProperty>>();
        //Quest.Add(npc1Quest);
        //Quest.Add(npc2Quest);
        //Quest.Add(npc3Quest);

        //testQuest = new Dictionary<int, List<QuestDataProperty>>();
        //testQuest.Add(1, npc1Quest);
        //testQuest.Add(2, npc2Quest);
        //testQuest.Add(3, npc3Quest);
    }

    private void Update()
    {
        //for(int i=0;i<Quest.Count;i++)
        //{
        //    for(int j =0; j<Quest[i].Count;j++)
        //    {
        //        Debug.Log("퀘스트" + i+" = " + Quest[i][j].ToString());
        //    }
        //}

        //foreach(KeyValuePair<int,QuestDataProperty> pair in testQuest)
        //{
        //}

        if (test == true)
            QuestDataController.GetInstance().SetQuest(1);

        QuestSuccess();
    }

    public void Show()
    {
    }

    public void NPC1Quest()
    {
        currentNpc = 1;

        npc.sprite = npc1Quest[npc1QuestNum].sprite;
        npcQuestNum.text = npc1Quest[npc1QuestNum].npcQuestNum.ToString();
        description.text = npc1Quest[npc1QuestNum].description;

        string questItem = npc1Quest[npc1QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
    }

    public void NPC2Quest()
    {
        currentNpc = 2;

        npc.sprite = npc2Quest[npc2QuestNum].sprite;
        npcQuestNum.text = npc2Quest[npc2QuestNum].npcQuestNum.ToString();
        description.text = npc2Quest[npc2QuestNum].description;

        string questItem = npc2Quest[npc2QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
    }

    public void NPC3Quest()
    {
        currentNpc = 3;

        npc.sprite = npc3Quest[npc3QuestNum].sprite;
        npcQuestNum.text = npc3Quest[npc3QuestNum].npcQuestNum.ToString();
        description.text = npc3Quest[npc3QuestNum].description;

        string questItem = npc3Quest[npc3QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
    }

    private void QuestSuccess()
    {
        if (QuestDataController.GetInstance().GetQuest()==0)
            return;

        switch(currentNpc)
        {
            case 1:
                npc1QuestNum += 1;
                test = false;
                QuestDataController.GetInstance().SetQuest(0);
                break;
            case 2:
                npc2QuestNum += 1;
                test = false;
                QuestDataController.GetInstance().SetQuest(0);
                break;
            case 3:
                npc3QuestNum += 1;
                test = false;
                QuestDataController.GetInstance().SetQuest(0);
                break;
            default:
                break;
        }
    }
}
