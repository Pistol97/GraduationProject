using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 탈출구 클래스
/// </summary>
public class Exit : MonoBehaviour, IInteractable
{
    private Fade _fade;

    private void Awake()
    {
        _fade = FindObjectOfType<Fade>();
    }

    public void ObjectInteract()
    {
        PlayerDataManager.Instance.GetInventoryData(FindObjectOfType<Inventory>());
        PlayerDataManager.Instance.SavePlayerData();
        StartCoroutine(_fade.FadeOut());    //로비 전환 페이드 아웃 재생

        Invoke("ChangeScene", 2f);  //페이드 아웃 애니메이션
    }

    //레벨 탈출
    private void ChangeScene()
    {
        //PlayerDataManager.Instance.SyncPlayerData();   //습득한 내용 플레이어 데이터와 동기화
        SceneManager.LoadScene("GameLobby");   //로비 전환
    }
}
