using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static void SceneMenu()
    {
        Debug.Log("LoadMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public static void ScenePlay()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("Demo_Stage");
        SceneManager.LoadScene("Demo_Stage");
    }

    public static void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
