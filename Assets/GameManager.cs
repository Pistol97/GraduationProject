using UnityEngine;

/// <summary>
/// 초기 설정 등 중요한 게임 로직 순서 설정을 담당하는 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Material[] outlines;

    private void Awake()
    {
        InitGameManager();

        Archive archive = FindObjectOfType<Archive>();

        //Subject에 옵저버 등록
        foreach (var whisperObject in FindObjectsOfType<WhisperObject>())
        {
            whisperObject.AddObserver(PlayerDataManager.Instance);
            whisperObject.AddObserver(archive);
        }

        //플레이어 데이터 불러오기
        PlayerDataManager.Instance.InitPlayerData();

        //아카이브 초기 설정 실행 
        archive.InitArchive();
    }

    private void InitGameManager()
    {
        //게임 시작시 모든 Outline 초기화
        foreach (var line in outlines)
        {
            line.SetFloat("_NormalStrength", 0f);
        }
    }
}
