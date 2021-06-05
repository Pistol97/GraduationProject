using System.Collections;
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

    public static void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
