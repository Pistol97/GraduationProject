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
        [TextArea]
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

    public bool resetQuest = false;

    private void Update()
    {
        QuestSuccess();

        if (resetQuest == true)
        {
            QuestDataController.GetInstance().SetQuest(0);
        }
    }

    public void NPC1Quest()
    {
        currentNpc = 1;

        npc.sprite = npc1Quest[npc1QuestNum].sprite;
        npcQuestNum.text = npc1Quest[npc1QuestNum].npcQuestNum.ToString();
        description.text = npc1Quest[npc1QuestNum].description;

        string questItem = npc1Quest[npc1QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(1);
    }

    public void NPC2Quest()
    {
        currentNpc = 2;

        npc.sprite = npc2Quest[npc2QuestNum].sprite;
        npcQuestNum.text = npc2Quest[npc2QuestNum].npcQuestNum.ToString();
        description.text = npc2Quest[npc2QuestNum].description;

        string questItem = npc2Quest[npc2QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(2);
    }

    public void NPC3Quest()
    {
        currentNpc = 3;

        npc.sprite = npc3Quest[npc3QuestNum].sprite;
        npcQuestNum.text = npc3Quest[npc3QuestNum].npcQuestNum.ToString();
        description.text = npc3Quest[npc3QuestNum].description;

        string questItem = npc3Quest[npc3QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(3);
    }

    private void QuestSuccess()
    {
        if (QuestDataController.GetInstance().GetQuest() == 0)
            return;

        if (QuestDataController.GetInstance().GetQuest() == 1)
        {
            Debug.Log("QuestComplete");

            switch (currentNpc)
            {
                case 1:
                    Debug.Log(npc1Quest.Count);
                    Debug.Log(npc1QuestNum);
                    if (npc1QuestNum >= npc1Quest.Count - 1) return;
                    npc1QuestNum += 1;
                    NPC1Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                case 2:
                    if (npc2QuestNum >= npc2Quest.Count - 1) return;
                    npc2QuestNum += 1;
                    NPC2Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                case 3:
                    if (npc3QuestNum >= npc3Quest.Count - 1) return;
                    npc3QuestNum += 1;
                    NPC3Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetQuestIndex(int index)
    {
        if (index > npc1Quest.Count + 1) return;
        index += 1;
    }
}
