using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    [SerializeField]
    private Slider Bar_Fear;

    [Header("플레이어 상태 변수")]
    [SerializeField]
    private float fearRange = 0;//공포 수치

    [SerializeField]
    private float maxFearRange = 100;//최대 공포 수치, 게임 오버

    private void Start()
    {
        //공포 수치 초기화
        Bar_Fear.value = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IncreaseFear();

            Debug.Log(fearRange);
        }
    }

    private void IncreaseFear()
    {
        fearRange += 10;
        Bar_Fear.value = fearRange / maxFearRange;
    }
}
