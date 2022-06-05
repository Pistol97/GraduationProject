using UnityEngine;

/// <summary>
/// 초기화, 동기화 등 실행 순서 설정을 담당하는 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    public Material[] outlines;

    private void Awake()
    {
        InitGameManager();

        //플레이어 데이터 불러오기
        PlayerDataManager.Instance.InitPlayerData();
    }

    private void InitGameManager()
    {
        //게임 시작시 모든 Outline 비활성화
        foreach (var line in outlines)
        {
            line.SetFloat("_NormalStrength", 0f);
        }
    }
}
