using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleManager : MonoBehaviour {

    [SerializeField] ParticleSystem deathExplosionPS;
    [SerializeField] ParticleSystem goalExplosionPS;

    public void DeathExplosion()
    {
        var explosionFX = Instantiate(deathExplosionPS, transform.position, Quaternion.identity);
        float delay = explosionFX.main.duration;
        Destroy(explosionFX.gameObject, delay);
        Destroy(gameObject);
    }

    public void GoalExplosion()
    {
        var explosionFX = Instantiate(goalExplosionPS, transform.position, Quaternion.identity);
        float delay = explosionFX.main.duration;
        Destroy(explosionFX.gameObject, delay);

    }

}
