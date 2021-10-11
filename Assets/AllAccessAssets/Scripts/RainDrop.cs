using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour 
{
    public ParticleSystem m_particleSystem;
    public GameObject rippleEffect;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = m_particleSystem.GetCollisionEvents(other, collisionEvents);
        
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 collisionHitLoc = collisionEvents[i].intersection;
            collisionHitLoc.y -= .0125f;

            GameObject nps = GameObject.Instantiate(rippleEffect, collisionHitLoc, Quaternion.identity);
            i++;
            Destroy(nps, 2.0f);
        }
    }
}
