using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 진행도에 따라 해금되는 업그레이드 상태를 확인 할 수 있는 연구실 오브젝트
/// </summary>
public class Upgrade : MonoBehaviour
{
    [SerializeField] private Sprite _unlockSprite;

    private UpgradeButton[] _upgradeButtons;

    private void Awake()
    {
        _upgradeButtons = GetComponentsInChildren<UpgradeButton>();
    }

    private void Start()
    {
        PlayerDataManager.Instance.SyncUpgradeData(_upgradeButtons);

        foreach(var button in _upgradeButtons)
        {
            if(button.IsUnlock)
            {
                button.GetComponent<Image>().sprite = _unlockSprite;
            }
        }
    }
}
