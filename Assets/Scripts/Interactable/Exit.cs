using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour, IInteractable
{
    private Fade _fade;

    private void Awake()
    {
        _fade = FindObjectOfType<Fade>();
    }

    public void ObjectInteract()
    {
        StartCoroutine(_fade.FadeOut());    //로비 전환 페이드 아웃 재생

        Invoke("ChangeScene", 2f);  //페이드 아웃 애니메이션
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Test_Lobby");   //로비 전환
    }
}
