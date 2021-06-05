using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public Text storyText;
    public Image buttonImage;
    public Sprite unlockSprite;
    public string story;
    public bool isUnlock = false;

    private void Update()
    {
        if(isUnlock == true)
        {
            buttonImage.sprite = unlockSprite;
        }
    }

    public void StoryButton()
    {
        if(isUnlock == true)
        {
            storyText.text = story;
        }
        else
        {
            storyText.text = "잠겨있다";
        }
    }
}
