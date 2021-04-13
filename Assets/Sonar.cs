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

    /// <summary>
    /// Unity's "constructor"
    /// </summary>
    private void Awake()
    {
        Sonar.Seers.Add(this);
    }

    /// <summary>
    /// Unity's "destructor"
    /// </summary>
    private void OnDestroy()
    {
        Sonar.Seers.Remove(this);
    }

    /// <summary>
    /// Every frame
    /// </summary>
    private void Update()
    {
        //At least one seer active this frame -> update needed
        Sonar.NeedUpdate = true;
        Radius = Radius + (10f * Time.deltaTime);
        if (20f <= Radius)
        {
            Radius = 0;
        }
    }

    /// <summary>
    /// Every frame, after all regular Update()
    /// </summary>
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
}
