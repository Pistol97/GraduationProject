﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static void OpenScene(string sceneName)
    {
        if(sceneName== "QuitGame")
        {
            QuitGame();
            return;
        }

        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void GUISceneMenu()
    {
        SceneMenu();
    }

    public void GUIQuitGame()
    {
        QuitGame();
    }

    public void GUIScenePlay()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("Demo_Stage");
        SceneManager.LoadScene("Demo_Stage");
    }

    public void GUIGameLobby()
    {
        GameLobby();
    }

    public static void SceneMenu()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("LoadMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public static void ScenePlay()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("Demo_Stage");
        SceneManager.LoadScene("Demo_Stage");
    }

    public static void GameLobby()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("GameLobby");
        SceneManager.LoadScene("GameLobby");
    }
    public static void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    public void GameLoad()
    {
        //Time.timeScale = 1.0f;//기본 시간
        Debug.Log("GameLoad");
        SceneManager.LoadScene("GameLoad");
    }

    public void Epilogue()
    {
        Debug.Log("Epilogue");
        AudioManager.Instance.StopSound("BGM_Lobby");
        SceneManager.LoadScene("Epilogue");
    }
}
