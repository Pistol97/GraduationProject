using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSystemManager : MonoBehaviour
{
    /// <summary>
    /// 싱글턴
    /// </summary>
    private static NoiseSystemManager instance;
    public static NoiseSystemManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<NoiseSystemManager>();

            if (instance == null)
            {
                GameObject container = new GameObject("NoiseSystemManager");

                instance = container.AddComponent<NoiseSystemManager>();
            }
        }
        return instance;
    }

    [SerializeField] private Slider fearSlider;

    [SerializeField] private float currentFear;
    [SerializeField] private float MaxFear;
    [SerializeField] private int fearLevel;

    private void Start()
    {
        //currentFear = 0;
        //fearLevel = 1;
        //MaxFear = 400;
        //fearSlider.maxValue = MaxFear;
    }

    private void Update()
    {
        //fearSlider.value = currentFear;
        //SetFearLevel();
    }

    public float GetFear()
    {
        return currentFear;
    }

    public void AddFear(float _addAmount)
    {
        currentFear += _addAmount;
        //SetFearLevel();
    }

    public int GetFearLevel()
    {
        return fearLevel;
    }

    public void SetFearLevel(int _level)
    {
        fearLevel = _level;
    }

    public void SetFearLevel()
    {
        switch(currentFear)
        {
            case float n when (0 <= n && n <= 100):
                SetFearLevel(1);
                break;
            case float n when (101 <= n && n <= 200):
                SetFearLevel(2);
                break;
            case float n when (201 <= n && n <= 300):
                SetFearLevel(3);
                break;
            case float n when (301 <= n):
                SetFearLevel(4);
                break;
            default:
                break;
        }
    }
}
