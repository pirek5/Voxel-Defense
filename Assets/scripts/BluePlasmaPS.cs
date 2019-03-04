using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlasmaPS : MonoBehaviour {

    [SerializeField] ParticleSystem plasmaPS;
    [SerializeField] ParticleSystem blueFX;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public void Shooting(bool isActive)
    {
        var emissionModule = plasmaPS.emission;
        emissionModule.enabled = isActive;
    }

    private void OnParticleCollision(GameObject other)
    {

        ParticlePhysicsExtensions.GetCollisionEvents(plasmaPS, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent collisionEvent)
    {
        if (collisionEvent.normal == Vector3.zero && collisionEvent.intersection == Vector3.zero)
        {
            // do nothing, weird unity bug 
        }
        else
        {
            Instantiate(blueFX, collisionEvent.intersection, Quaternion.LookRotation(collisionEvent.normal));
        }
    }

}
