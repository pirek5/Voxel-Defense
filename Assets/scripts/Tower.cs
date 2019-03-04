using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour {
    //set in editor
    [SerializeField] private Transform objectToPan;

    //config
    [SerializeField] private float range = 50;
    [SerializeField] private ParticleSystem plasmaPS;
    [SerializeField] private float searchForEnemyPeriod = 1f;

    //cached
    private BluePlasmaPS bluePlasmaPS;

    //state
    public TowerCube baseBlock;
    private GameObject targetEnemy;
    public bool manualAiming = false;

    private void Start()
    {
        bluePlasmaPS = GetComponentInChildren<BluePlasmaPS>();
    }

    public void Init()
    {
        StopAllCoroutines();
        StartCoroutine(SearchForClosestCoroutine());
    }

    void Update()
    {
        {
            if (targetEnemy)
            {
                objectToPan.transform.LookAt(targetEnemy.transform);
                TowerShoot();
            }
            else
            {
                bluePlasmaPS.Shooting(false);
            }
        }
    }

    IEnumerator SearchForClosestCoroutine()
    {
        while (true)
        {
            FindClosestEnemy();
            yield return new WaitForSeconds(searchForEnemyPeriod);
        }
    }

    private void TowerShoot()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, transform.position);
        if (distance < range  || manualAiming)
        {
            bluePlasmaPS.Shooting(true);
        }
        else
        {
            bluePlasmaPS.Shooting(false);
        }
    }

    void FindClosestEnemy()
    {
        Dictionary<String, EnemyMovement> enemies = EnemiesController.Instance.enemies;
        if(enemies == null) { return; }

        var firstElement = enemies.First();
        Transform closestEnemy = firstElement.Value.transform;
        foreach(KeyValuePair<String,EnemyMovement> testEnemy in enemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.Value.transform);
        }
        targetEnemy = closestEnemy.gameObject;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemyTransform)
    {
        float currentClosestDistance = Vector3.Distance(closestEnemy.transform.position, transform.position );
        float testDistance = Vector3.Distance(testEnemyTransform.position, transform.position);
        if(testDistance < currentClosestDistance)
        {
            closestEnemy = testEnemyTransform;
        }
        return closestEnemy;
    }

    public void Stop() // after end of game
    {
        StopAllCoroutines();
        targetEnemy = null;
    }

    public void SwitchAutomaticManualAiming(bool isManual)
    {
        manualAiming = isManual;
        if (manualAiming) //switch from automatic to manual
        {
            if (UserController.Instance != null)
            {
                targetEnemy = UserController.Instance.crosshair;
                StopAllCoroutines();
            }
        }
        else //switch from manual to automatic
        {
            targetEnemy = null;
            Init();
        }
    }
}



