using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material[] outlines;

    public GameObject player;
    private void Awake()
    {
        player.GetComponent<CharacterController>().detectCollisions = true;
        //player.GetComponent<CapsuleCollider>().isTrigger = true;
        foreach(var line in outlines)
        {
            line.SetFloat("_NormalStrength", 0f);
        }
    }
}
