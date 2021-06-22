using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mark a transform as an invisibility revealer
/// </summary>
[ExecuteInEditMode()]
public class Sonar : MonoBehaviour
{
    #region Personnal data
    public float Radius = 0f;

    public Vector3 Size;
    public float Gradient;
    public Color GradientColor;
    public bool Hide = false;
    #endregion

    #region Sonar data
    [SerializeField] private Transform pulse;
    private float range = 0f;
    private float rangeMax = 20f;
    private float rangeSpeed = 10f;

    private float disappearTimer = 0f;
    private float disappearTimerMax = 3f;

    List<Collider> detectedEnemy;
    [SerializeField] private Transform sonarPing;
    [SerializeField] private Material outline;
    [SerializeField] private Material outline_enemy;
    [SerializeField] private Material outline_item;
    #endregion

    /// <summary>
    /// Maximum number of Revealers expected.
    /// NOTE : important as GPU don't seem to allow arrays resizing mid-execution -> need to use the maximum size from the start
    /// </summary>
    static private int MaxSize = 64;

    /// <summary>
    /// All registered seers
    /// </summary>
    static private List<Sonar> Seers = new List<Sonar>();

    #region Reusable storage

    static private Vector4[] AllPosition = new Vector4[Sonar.MaxSize];
    static private float[] AllRadius = new float[Sonar.MaxSize];
    static private Vector4[] AllSize = new Vector4[Sonar.MaxSize];
    static private float[] AllGradient = new float[Sonar.MaxSize];
    static private Vector4[] AllColor = new Vector4[Sonar.MaxSize];
    static private float[] AllShape = new float[Sonar.MaxSize];
    static private float[] AllHider = new float[Sonar.MaxSize];

    #endregion

    /// <summary>
    /// Data changed and need to be re-sent to the shader
    /// </summary>
    static private bool NeedUpdate = true;

    [SerializeField] private bool _isTest;
    private void Awake()
    {
        Sonar.Seers.Add(this);
        detectedEnemy = new List<Collider>();

        outline = Resources.Load<Material>("Shader/TrasparentOutlines");
        outline_enemy = Resources.Load<Material>("Shader/EnemyOutline");
        outline_item = Resources.Load<Material>("Shader/ItemOutline");
        //사용시 외곽선 활성화
        outline.SetFloat("_NormalStrength", 1f);
        outline_enemy.SetFloat("_NormalStrength", 1f);
        outline_item.SetFloat("_NormalStrength", 1f);
    }

    private void OnDestroy()
    {
        Sonar.Seers.Remove(this);
    }

    private void Update()
    {
        Sonar.NeedUpdate = true;

        if (!_isTest)
        {
            SonarPulseCast();
            Radius = range;
        }

    }

    private void LateUpdate()
    {
        //No update required (and/or already done by another seer)
        if (Sonar.NeedUpdate == false) return;
        Sonar.NeedUpdate = false;

        //(Expected) total number
        int Total = Sonar.Seers.Count;

        //Extract info and store into shader-compatible arrays
        int Cursor = 0;
        for (int Indice = 0; Indice < Sonar.Seers.Count; Indice++)
        {
            Sonar Current = Sonar.Seers[Indice];

            //Check if supposed to be active
            if (Current.enabled == false || Current.gameObject.activeInHierarchy == false)
            {
                //Arrays won't be completly filled, but shader use "Total" as array size anyway
                Total--;
                continue;
            }

            Sonar.AllPosition[Cursor] = Current.transform.position;
            Sonar.AllRadius[Cursor] = Current.Radius;
            Sonar.AllSize[Cursor] = Current.Size;
            Sonar.AllGradient[Cursor] = Current.Gradient;
            Sonar.AllColor[Cursor] = Current.GradientColor;

            if (Current.Hide == true) Sonar.AllHider[Cursor] = 1;
            else Sonar.AllHider[Cursor] = 0;

            Cursor++;
        }

        //Apply updates   
        Shader.SetGlobalInt("_Seers", Total);
        Shader.SetGlobalVectorArray("_SeerPosition", Sonar.AllPosition);
        Shader.SetGlobalFloatArray("_SeerRadius", Sonar.AllRadius);
        Shader.SetGlobalVectorArray("_SeerSize", Sonar.AllSize);
        Shader.SetGlobalFloatArray("_SeerGradient", Sonar.AllGradient);
        Shader.SetGlobalVectorArray("_SeerColor", Sonar.AllColor);
        Shader.SetGlobalFloatArray("_SeerHider", Sonar.AllHider);
    }

    //적 감지 펄스
    private void SonarPulseCast()
    {
        range += rangeSpeed * Time.deltaTime;

        if (pulse)
        {
            pulse.localScale = new Vector3(Radius, Radius);
        }

        if (range >= rangeMax)
        {
            disappearTimer += Time.deltaTime;

            var dissappear = Mathf.Lerp(1f, 0f, disappearTimer / disappearTimerMax);
            //Debug.Log(dissappear);
            outline.SetFloat("_NormalStrength", dissappear);
            outline_enemy.SetFloat("_NormalStrength", dissappear);
            outline_item.SetFloat("_NormalStrength", dissappear);
            //Debug.Log(outline.GetFloat("_NormalStrength"));

            rangeSpeed = 0f;
            //detectedEnemy.Clear();  //탐색 리스트 클리어

            if (pulse)
            {
                Destroy(pulse.gameObject);
            }

            if (0f >= outline.GetFloat("_NormalStrength"))
            {
                Sonar.Seers.Remove(this);
                Destroy(gameObject, 0.5f);
            }
        }

        var hits = Physics.SphereCastAll(transform.position, range, Vector3.up, 0f);

        foreach (var hit in hits)
        {
            //적 감지
            if (hit.transform.CompareTag("Enemy")
                && !detectedEnemy.Contains(hit.collider))   //이미 탐색된 적은 무시, 한 번만 탐색
            {
                Debug.Log("Enemy Detected" + hit.transform.position);
                detectedEnemy.Add(hit.collider);    //처음 탐색 되었을 시 추가

                Instantiate(sonarPing, hit.transform.position, Quaternion.Euler(new Vector3(90f, 0f)));
            }
        }

    }
}
