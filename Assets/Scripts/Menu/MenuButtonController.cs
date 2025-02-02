﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour 
{

	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () 
	{
		if(Input.GetAxis ("Vertical") != 0)
		{
			if(!keyDown)
			{
				if (Input.GetAxis ("Vertical") < 0) 
				{
					if(index < maxIndex)
					{
						index++;
					}
					else
					{
						index = 0;
					}
				} 
				else if(Input.GetAxis ("Vertical") > 0)
				{
					if(index > 0)
					{
						index --; 
					}
					else
					{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}
		else
		{
			keyDown = false;
		}

		ChangeScene();
	}

	void ChangeScene()
    {
		if (index == 0)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				SceneMgr.GameLobby();
			}
		}
		if (index == 1)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				SceneMgr.SceneMenu();
			}
		}
		if (index == 2)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				SceneMgr.QuitGame();
			}
		}
	}

	public void SetIndex(int num)
    {
		index = num;
    }

}
