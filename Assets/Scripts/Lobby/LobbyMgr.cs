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

    private void Start()
    {
        questMgr = GetComponent<QuestMgr>();
    }

    private void Update()
    {
        text_npcNum1.text = QuestMgr.npc1QuestNum.ToString()+"/"+ questMgr.npc1Quest.Count;
        text_npcNum2.text = QuestMgr.npc1QuestNum.ToString() + "/" + questMgr.npc2Quest.Count;
        text_npcNum3.text = QuestMgr.npc1QuestNum.ToString() + "/" + questMgr.npc3Quest.Count;
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


}
