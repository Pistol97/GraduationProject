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

    public static int npc1QuestNum = 0;
    public static int npc2QuestNum = 0;
    public static int npc3QuestNum = 0;

    public int currentNpc;

    private void Update()
    {
        QuestSuccess();
    }

    public void NPC1Quest()
    {
        currentNpc = 1;
        //npc1QuestNum = QuestDataController.GetInstance().GetNPC1QuestNum();

        npc.sprite = npc1Quest[npc1QuestNum].sprite;
        npcQuestNum.text = npc1Quest[npc1QuestNum].npcQuestNum.ToString();
        description.text = npc1Quest[npc1QuestNum].description;

        string questItem = npc1Quest[npc1QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
    }

    public void NPC2Quest()
    {
        currentNpc = 2;
        //npc2QuestNum = QuestDataController.GetInstance().GetNPC2QuestNum();

        npc.sprite = npc2Quest[npc2QuestNum].sprite;
        npcQuestNum.text = npc2Quest[npc2QuestNum].npcQuestNum.ToString();
        description.text = npc2Quest[npc2QuestNum].description;

        string questItem = npc2Quest[npc2QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
    }

    public void NPC3Quest()
    {
        currentNpc = 3;
        //npc3QuestNum = QuestDataController.GetInstance().GetNPC3QuestNum();

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
        
        if(QuestDataController.GetInstance().GetQuest() == 1)
        {
            Debug.Log("QuestComplete");

            switch (currentNpc)
            {
                case 1:
                    npc1QuestNum += 1;
                    NPC1Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                case 2:
                    npc2QuestNum += 1;
                    NPC2Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                case 3:
                    npc3QuestNum += 1;
                    NPC3Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                default:
                    break;
            }
        }
    }
}
