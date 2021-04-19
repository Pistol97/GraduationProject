using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mark a transform as an invisibility revealer
/// </summary>
[ExecuteInEditMode()]
public class Sonar : MonoBehaviour
{
    #region Personnal data
    public Shapes Shape = Shapes.Sphere;
    public float Radius;

    public Vector3 Size;
    public float Gradient;
    public Color GradientColor;
    public bool Hide = false;
    #endregion

    #region Sonar data
    [SerializeField] private Transform pulse;
    [SerializeField] private float range;
    private float rangeMax = 10f;

    List<Collider> detectedEnemy;
    [SerializeField] private Transform sonarPing;
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

    /// <summary>
    /// Shape of the revealing zone
    /// </summary>
    public enum Shapes : int
    {
        Sphere,
        Cylinder,
        AABB
    }

    private void Awake()
    {
        Sonar.Seers.Add(this);
        detectedEnemy = new List<Collider>();
    }

    private void OnDestroy()
    {
        Sonar.Seers.Remove(this);
    }

    private void Update()
    {
        Sonar.NeedUpdate = true;
        SonarPulseCast();
        Radius = range;
        pulse.localScale = new Vector3(Radius, Radius);
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

            if (Current.Shape == Sonar.Shapes.Cylinder) Sonar.AllShape[Cursor] = 1;
            else if (Current.Shape == Sonar.Shapes.AABB) Sonar.AllShape[Cursor] = 2;
            else Sonar.AllShape[Cursor] = 0;

            Cursor++;
        }

        //Apply updates   
        Shader.SetGlobalInt("_Seers", Total);
        Shader.SetGlobalVectorArray("_SeerPosition", Sonar.AllPosition);
        Shader.SetGlobalFloatArray("_SeerRadius", Sonar.AllRadius);
        Shader.SetGlobalVectorArray("_SeerSize", Sonar.AllSize);
        Shader.SetGlobalFloatArray("_SeerGradient", Sonar.AllGradient);
        Shader.SetGlobalVectorArray("_SeerColor", Sonar.AllColor);
        Shader.SetGlobalFloatArray("_SeerShape", Sonar.AllShape);
        Shader.SetGlobalFloatArray("_SeerHider", Sonar.AllHider);
    }

    //적 감지 펄스
    private void SonarPulseCast()
    {
        float rangeSpeed = 5f;
        range += rangeSpeed * Time.deltaTime;

        if (range > rangeMax)
        {
            range = 0f;
            detectedEnemy.Clear();  //탐색 리스트 클리어
        }

        var hits = Physics.SphereCastAll(transform.position, range, Vector3.up, 0f, LayerMask.NameToLayer("Enemy"));

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
