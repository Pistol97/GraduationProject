using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, 10f, _player.transform.position.z);
        transform.rotation = Quaternion.Euler(90f, _player.GetComponentInChildren<CameraControl>().rotationY, 0f);
    }
}
