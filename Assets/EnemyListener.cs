using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListener : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Noise")
        {
            Debug.Log("Enemy hear some sounds");

            enemy.location = other.transform.position;
            enemy.chase = true;
        }
    }
}
