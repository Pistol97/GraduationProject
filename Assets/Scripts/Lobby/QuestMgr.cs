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

    public void NPCQuest(int _npcQuestNum, int _currentNpc)
    {
        List<QuestDataProperty> QuestData = new List<QuestDataProperty>();

        currentNpc = _currentNpc;

        switch (_currentNpc)
        {
            case 1:
                QuestData = npc1Quest;
                return;
            case 2:
                QuestData = npc2Quest;
                return;
            case 3:
                QuestData = npc3Quest;
                return;
        }

        npc.sprite = QuestData[_npcQuestNum].sprite;
        npcQuestNum.text = QuestData[_npcQuestNum].npcQuestNum.ToString();
        description._description = QuestData[_npcQuestNum].description;

        string questItem = QuestData[_npcQuestNum].questItem;
        QuestDataController.GetInstance().SetQuestItem(questItem);
        QuestDataController.GetInstance().SetCurrentQuestNpcNum(currentNpc);
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
                //중복 코드 수정
                case 1:
                    if (npc1QuestNum >= npc1Quest.Count - 1) return;
                    npc1QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc1QuestNum(npc1QuestNum);
                    NPCQuest(npc1QuestNum,1);
                    QuestDataController.GetInstance().SetQuest(0);
                    PlayerDataManager.Instance.UnlockUpgradeProfile(1);
                    break;
                case 2:
                    if (npc2QuestNum >= npc2Quest.Count - 1) return;
                    npc2QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc2QuestNum(npc2QuestNum);
                    NPCQuest(npc2QuestNum, 2);
                    QuestDataController.GetInstance().SetQuest(0);
                    PlayerDataManager.Instance.UnlockUpgradeProfile(2);
                    break;
                case 3:
                    if (npc3QuestNum >= npc3Quest.Count - 1) return;
                    npc3QuestNum += 1;
                    QuestDataController.GetInstance().SetNpc3QuestNum(npc3QuestNum);
                    NPCQuest(npc3QuestNum, 3);
                    QuestDataController.GetInstance().SetQuest(0);
                    break;
                default:
                    break;
            }
        }
    }

    private void NpcQuestSuccess(int _npcQuestNum, List<QuestDataProperty> _npcQuest, int _currentNpc)
    {
        if (_npcQuestNum >= _npcQuest.Count - 1) return;
        _npcQuestNum += 1;
        QuestDataController.GetInstance().SetNpc1QuestNum(_npcQuestNum);
        NPCQuest(npc1QuestNum, _currentNpc);
        QuestDataController.GetInstance().SetQuest(0);
    }

    private void SaveQuest(int _questId, bool _bool)
    {
        PlayerDataManager.Instance.SetQuestData(_questId, _bool);
    }
}
