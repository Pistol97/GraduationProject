﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        Debug.Log("Demo_Stage");
        SceneManager.LoadScene("Demo_Stage");
    }
}