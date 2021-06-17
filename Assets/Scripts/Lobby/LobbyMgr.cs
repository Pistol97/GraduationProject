using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMgr : MonoBehaviour
{
    [SerializeField] private GameObject quest;

    [SerializeField] private GameObject questLounge;

    [SerializeField] private GameObject upgrade;

    [SerializeField] private GameObject story;

    public Text text_npcNum1;
    public Text text_npcNum2;
    public Text text_npcNum3;

    private int npcNum1;
    private int npcNum2;
    private int npcNum3;

    public QuestMgr questMgr;

    private void Start()
    {
        questMgr = GetComponent<QuestMgr>();
    }

    private void Update()
    {
        SetQuestNum();
    }

    public void QuestButton()
    {
        quest.SetActive(true);
        upgrade.SetActive(false);
        story.SetActive(false);
    }

    public void QuestLoungeButtonOn()
    {
        questLounge.SetActive(true);
    }
    public void QuestLoungeButtonOff()
    {
        questLounge.SetActive(false);
    }

    public void UpgradeButton()
    {
        quest.SetActive(false);
        upgrade.SetActive(true);
        story.SetActive(false);
    }

    public void StroyButton()
    {
        quest.SetActive(false);
        upgrade.SetActive(false);
        story.SetActive(true);
    }

    private void SetQuestNum()
    {
        npcNum1 = QuestMgr.npc1QuestNum + 1;
        npcNum2 = QuestMgr.npc2QuestNum + 1;
        npcNum3 = QuestMgr.npc3QuestNum + 1;

        text_npcNum1.text = npcNum1.ToString() + "/" + questMgr.npc1Quest.Count;
        text_npcNum2.text = npcNum2.ToString() + "/" + questMgr.npc2Quest.Count;
        text_npcNum3.text = npcNum3.ToString() + "/" + questMgr.npc3Quest.Count;
    }
}
