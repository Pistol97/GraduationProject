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

    public QuestMgr questMgr;

    [SerializeField] private int npcCount1;
    [SerializeField] private int npcCount2;
    [SerializeField] private int npcCount3;

    [SerializeField] private int npcNumCount1;
    [SerializeField] private int npcNumCount2;
    [SerializeField] private int npcNumCount3;

    [SerializeField] Button btn_GameStart;

    [SerializeField] GameObject AllQuestComplete;

    private void Start()
    {
        questMgr = GetComponent<QuestMgr>();
        AudioMgr.Instance.StopSound("BGM_Stage");
        AudioMgr.Instance.PlaySound("BGM_Lobby");
    }

    private void Update()
    {
        SetQuestNum();

        IsQuestComplete();
    }

    public void QuestButton()
    {
        quest.SetActive(true);
        upgrade.SetActive(false);
        story.SetActive(false);
        AudioMgr.Instance.PlaySound("Click");
    }

    public void QuestLoungeButtonOn()
    {
        questLounge.SetActive(true);

    }
    public void QuestLoungeButtonOff()
    {
        questLounge.SetActive(false);
    }

    public void IsQuestComplete()
    {
        if (npcCount1 == npcNumCount1 && npcCount2 == npcNumCount2 && npcCount3 == npcNumCount3)
        {
            AllQuestComplete.SetActive(true);
        }
    }

    public void GameButtonActive(int num)
    {
        switch(num)
        {
            case 1:
                if (npcCount1 == npcNumCount1) btn_GameStart.enabled = false;
                else btn_GameStart.enabled = true;
                break;
            case 2:
                if (npcCount2 == npcNumCount2) btn_GameStart.enabled = false;
                else btn_GameStart.enabled = true;
                break;
            case 3:
                if (npcCount3 == npcNumCount3) btn_GameStart.enabled = false;
                else btn_GameStart.enabled = true;
                break;
            default:
                break;

        }
    }

    public void UpgradeButton()
    {
        quest.SetActive(false);
        upgrade.SetActive(true);
        story.SetActive(false);
        AudioMgr.Instance.PlaySound("Click");
    }

    public void StroyButton()
    {
        quest.SetActive(false);
        upgrade.SetActive(false);
        story.SetActive(true);
        AudioMgr.Instance.PlaySound("Click");
    }

    private void SetQuestNum()
    {
        npcNumCount1 = questMgr.npc1Quest.Count - 1;
        npcNumCount2 = questMgr.npc2Quest.Count - 1;
        npcNumCount3 = questMgr.npc3Quest.Count - 1;

        npcCount1 = QuestMgr.npc1QuestNum;
        npcCount2 = QuestMgr.npc2QuestNum;
        npcCount3 = QuestMgr.npc3QuestNum;

        text_npcNum1.text = QuestMgr.npc1QuestNum + "/" + npcNumCount1.ToString();
        text_npcNum2.text = QuestMgr.npc2QuestNum + "/" + npcNumCount2.ToString();
        text_npcNum3.text = QuestMgr.npc3QuestNum + "/" + npcNumCount3.ToString();
    }
}
