using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour {

    //set in editor
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3 spawningPosition;

    //config
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private float changeDifficultTime = 5f;
    [SerializeField] private float difficultFactor = 1.1f;
    [SerializeField] private float slowingDifficultInTimeFactor;
    [SerializeField] private float minimumDifficultFactor = 1.04f;

    //cached
    public Dictionary<string, EnemyMovement> enemies = new Dictionary<string, EnemyMovement>(); //dictionary contains all enemies that are in scene

    //state
    private float currentSpeed;

    //singleton
    private static EnemiesController instance;
    public static EnemiesController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void Init ()
    {
        currentSpeed = enemyPrefab.GetComponent<EnemyMovement>().movementSpeed;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(ProgresiveDifficult());
	}

    IEnumerator SpawnEnemy()  // constantyly spawning enemies
    {
        int enemyNameIndex = 0;
        while (true)
        {
            enemyNameIndex++;
            var newEnemy = Instantiate(enemyPrefab, spawningPosition, Quaternion.identity);
            newEnemy.transform.parent = transform;
            newEnemy.name = "Enemy " + enemyNameIndex;
            newEnemy.GetComponent<EnemyMovement>().movementSpeed = currentSpeed;
            if (!enemies.ContainsKey(newEnemy.name) && newEnemy.GetComponent<EnemyMovement>()) //error checking
            {
                enemies.Add(newEnemy.name, newEnemy.GetComponent<EnemyMovement>());
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator ProgresiveDifficult() //during gameplay monemnt speed incres and spawning deley decrese
    {
        yield return new WaitForSeconds(changeDifficultTime);
        while (true)
        {
            spawnDelay = spawnDelay / difficultFactor;
            currentSpeed = currentSpeed * difficultFactor;
            foreach (KeyValuePair<string, EnemyMovement> enemy in enemies)
            {
                enemy.Value.movementSpeed = currentSpeed;
            }

            if (difficultFactor > minimumDifficultFactor)
            {
                difficultFactor = difficultFactor * slowingDifficultInTimeFactor;
                difficultFactor = Mathf.Clamp(difficultFactor, minimumDifficultFactor, Mathf.Infinity);
            }
            yield return new WaitForSeconds(changeDifficultTime);
        }
    }

    public void RemoveEnemyFromDictionary(GameObject enemy) //method called by destoyed enemies
    {
        if (enemies.ContainsKey(enemy.name))
        {
            enemies.Remove(enemy.name);
        }
    }

    public void FindNewPaths(PathCube pathCubeBlocked)
    {
        foreach (KeyValuePair<string,EnemyMovement> enemy in enemies)
        {
            enemy.Value.FindNewPath(pathCubeBlocked);
        }
    }

    public void FindNewPaths()
    {
        foreach (KeyValuePair<string, EnemyMovement> enemy in enemies)
        {
            enemy.Value.FindNewPath();
        }
    }

    public void StopEnemiesMovement() //end of game
    {
        foreach (KeyValuePair<string, EnemyMovement> enemy in enemies)
        {
            enemy.Value.StopMovement();
            StopAllCoroutines();
        }
    }


}
