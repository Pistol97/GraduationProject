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

    //레벨 탈출
    private void ChangeScene()
    {
        //PlayerDataManager.Instance.SyncPlayerData();   //습득한 내용 플레이어 데이터와 동기화
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene("GameLobby");   //로비 전환
    }
}
