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
    public QuestDialogue description;
    public Button back;

    public List<QuestDataProperty> npc1Quest;
    public List<QuestDataProperty> npc2Quest;
    public List<QuestDataProperty> npc3Quest;

    public static int npc1QuestNum = 0;
    public static int npc2QuestNum = 0;
    public static int npc3QuestNum = 0;

    public int currentNpc;

    public bool resetQuest = false;

    private void Start()
    {
        npc1QuestNum = QuestDataController.GetInstance().GetNpc1QuestNum();
        npc2QuestNum = QuestDataController.GetInstance().GetNpc2QuestNum();
        npc3QuestNum = QuestDataController.GetInstance().GetNpc3QuestNum();
    }

    private void Update()
    {
        if (resetQuest == true)
        {
            QuestDataController.GetInstance().SetQuest(0);
            QuestDataController.GetInstance().SetNpc1QuestNum(0);
            QuestDataController.GetInstance().SetNpc2QuestNum(0);
            QuestDataController.GetInstance().SetNpc3QuestNum(0);
        }

        QuestSuccess();

    }

    //중복 함수 고치기,,,
    //public void NPCQuest(int _npcQuestNum, int _currentNpc)
    //{
    //    List<QuestDataProperty> QuestData = new List<QuestDataProperty>(2);

    //    currentNpc = _currentNpc;
        
    //    switch(_currentNpc)
    //    {
    //        case 1:
    //            QuestData = npc1Quest;
    //            return;
    //        case 2:
    //            QuestData = npc2Quest;
    //            return;
    //        case 3:
    //            QuestData = npc3Quest;
    //            return;
    //    }

    //    npc.sprite = QuestData[_npcQuestNum].sprite;
    //    npcQuestNum.text = QuestData[_npcQuestNum].npcQuestNum.ToString();
    //    description._description = QuestData[_npcQuestNum].description;

    //    string questItem = QuestData[_npcQuestNum].questItem;
    //    QuestDataController.GetInstance().SetQuestItem(questItem);
    //    QuestDataController.GetInstance().SetCurrentQuestNpcNum(currentNpc);
    //}

    public void NPC1Quest()
    {
        currentNpc = 1;

        npc.sprite = npc1Quest[npc1QuestNum].sprite;
        npcQuestNum.text = npc1Quest[npc1QuestNum].npcQuestNum.ToString();
        description._description = npc1Quest[npc1QuestNum].description;

        string questItem = npc1Quest[npc1QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(1);
    }

    public void NPC2Quest()
    {
        currentNpc = 2;

        npc.sprite = npc2Quest[npc2QuestNum].sprite;
        npcQuestNum.text = npc2Quest[npc2QuestNum].npcQuestNum.ToString();
        description._description = npc2Quest[npc2QuestNum].description;

        string questItem = npc2Quest[npc2QuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(2);
    }

    public void NPC3Quest()
    {
        currentNpc = 3;

        npc.sprite = npc3Quest[npc3QuestNum].sprite;
        npcQuestNum.text = npc3Quest[npc3QuestNum].npcQuestNum.ToString();
        description._description = npc3Quest[npc3QuestNum].description;

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

            switch (QuestDataController.GetInstance().GetCurrentQuestNpcNum())
            {
                //어떻게 고치지,,
                case 1:
                    if (npc1QuestNum >= npc1Quest.Count - 1) return;
                    npc1QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc1QuestNum(npc1QuestNum);
                    NPC1Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    PlayerDataManager.Instance.UnlockUpgradeProfile(1);
                    break;
                case 2:
                    if (npc2QuestNum >= npc2Quest.Count - 1) return;
                    npc2QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc2QuestNum(npc2QuestNum);
                    NPC2Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    PlayerDataManager.Instance.UnlockUpgradeProfile(2);
                    break;
                case 3:
                    if (npc3QuestNum >= npc3Quest.Count - 1) return;
                    npc3QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc3QuestNum(npc3QuestNum);
                    NPC3Quest();
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                default:
                    break;
            }
        }
    }

    private void SaveQuest(int _questId, bool _bool)
    {
        PlayerDataManager.Instance.SetQuestData(_questId, _bool);
    }
}
