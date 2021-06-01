using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMgr : MonoBehaviour
{
    [System.Serializable]
    public class QuestDataProperty
    {
        //public int npcID;
        public int npcQuestNum;
        public Sprite sprite;
        public string description;
        public string reward;
    }

    public Image npc;
    public Text npcQuestNum;
    public Text description;
    public Button back;

    public int testquest;

    public List<QuestDataProperty> questData;

    public Dictionary<int,QuestDataProperty> npcQuestData;
    
    public List<QuestDataProperty> npc1Quest;
    public List<QuestDataProperty> npc2Quest;
    public List<QuestDataProperty> npc3Quest;

    public int npc1QuestNum = 0;
    public int npc2QuestNum = 0;
    public int npc3QuestNum = 0;

    [System.Serializable]
    public class SerializeDicString : SerializeDictionary<string, string> { }

    public SerializeDicString questDictionary = new SerializeDicString();

    private void Update()
    {
        //if (testquest >= 0 && testquest <= questData.Count)
        //{
        //    UpdateContant(testquest - 1);
        //}
    }

    private void UpdateContant(int npcID)
    {
        npc.sprite = questData[npcID].sprite;
        npcQuestNum.text = questData[npcID].npcQuestNum.ToString();
        description.text = questData[npcID].description;
    }

    public void NPC1Quest()
    {
        npc.sprite = npc1Quest[npc1QuestNum].sprite;
        npcQuestNum.text = npc1Quest[npc1QuestNum].npcQuestNum.ToString();
        description.text = npc1Quest[npc1QuestNum].description;
    }

    public void NPC2Quest()
    {
        npc.sprite = npc2Quest[npc2QuestNum].sprite;
        npcQuestNum.text = npc2Quest[npc2QuestNum].npcQuestNum.ToString();
        description.text = npc2Quest[npc2QuestNum].description;
    }

    public void NPC3Quest()
    {
        npc.sprite = npc3Quest[npc3QuestNum].sprite;
        npcQuestNum.text = npc3Quest[npc3QuestNum].npcQuestNum.ToString();
        description.text = npc3Quest[npc3QuestNum].description;
    }
}
