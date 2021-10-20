using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessMgr : MonoBehaviour
{
    private Volume _postprocessVolume;

    private readonly string _path = "UpgradeProfile/Upgrade";

    private Player _player;

    private Vignette _vg;
    private ColorAdjustments _colorAdj;

    private void Awake()
    {
        _postprocessVolume = GetComponent<Volume>();
    }

    private void Start()
    {
        //PlayerDataManager.Instance.SyncUpgradeProfile(ref _profileNumber);
        //Debug.Log(_path + _profileNumber);
        _postprocessVolume.profile = GetComponent<Volume>().profile;
        _player = FindObjectOfType<Player>();

        _postprocessVolume.profile.TryGet(out Vignette vg);
        _vg = vg;
        _postprocessVolume.profile.TryGet(out ColorAdjustments colorAdj);
        _colorAdj = colorAdj;
    }

    private void LateUpdate()
    {
        _vg.intensity.value = (_player.FearRange - 20) / 100;


        _colorAdj.colorFilter.value = new Color(1, 1 - (_player.FearRange / 100), 1 - (_player.FearRange / 100));
    }
}
