using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;//기본 시간
        Debug.Log("Demo_Stage");
        SceneManager.LoadScene("Demo_Stage");
    }
}
