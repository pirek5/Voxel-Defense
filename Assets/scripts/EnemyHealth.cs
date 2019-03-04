using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    //config
    [SerializeField] private int health = 100;

    //cached
    UiBar healthbar;
    private EnemyParticleManager enemyParticleManager;

    //state
    private int currentHealth;

    private void Start()
    {
        healthbar = GetComponentInChildren<UiBar>();
        enemyParticleManager = GetComponent<EnemyParticleManager>();
        currentHealth = health;
    }

    private void OnParticleCollision(GameObject other) // triggered by towers "bullets"
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHealth--;
        healthbar.value = (float)currentHealth / health; 
        if (currentHealth <= 0)
        {
            KillEnemy();
        }

    }   

    private void KillEnemy()
    {
        ScoreManager.Instance.AddScore();
        enemyParticleManager.DeathExplosion();
        EnemiesController.Instance.RemoveEnemyFromDictionary(this.gameObject);
        Destroy(gameObject);
    }

}
