using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMgr : MonoBehaviour
{
    [SerializeField] private GameObject quest;

    [SerializeField] private GameObject questLounge;

    [SerializeField] private GameObject upgrade;

    [SerializeField] private GameObject story;

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


}
