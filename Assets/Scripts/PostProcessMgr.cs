using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessMgr : MonoBehaviour
{
    private Volume _postprocessVolume;

    private readonly string _path = "UpgradeProfile/Upgrade";

    private int _profileNumber = 0;

    private void Awake()
    {
        _postprocessVolume = GetComponent<Volume>();
    }

    private void Start()
    {
        PlayerDataManager.Instance.SyncUpgradeData(ref _profileNumber);
        Debug.Log(_path + _profileNumber);
        _postprocessVolume.profile = Resources.Load(_path + _profileNumber) as VolumeProfile;
    }
}
