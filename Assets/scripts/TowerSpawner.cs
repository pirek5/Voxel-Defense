using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour {


    //[SerializeField] int towersLimit = 5;
    private GameObject towerPrefab;

    //set in editor
    [SerializeField] GameObject[] towers;

    
    //cached data
    Queue<GameObject> towersQueue = new Queue<GameObject>();

    //state
    private bool automaticAiming = true;

    //singleton
    private static TowerSpawner instance;
    public static TowerSpawner Instance { get { return instance; } }

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
        //StartCoroutine(AutoSpawn()); debug only
    }

    public void TowersInit()
    {
        foreach(GameObject tower in towers)
        {
            towersQueue.Enqueue(tower);
            tower.GetComponent<Tower>().Init();
        }
    }

    public void MoveTower(TowerCube baseBlock)
    {
        var lastTower = towersQueue.Dequeue();
        MakeBasePointPlaceable(lastTower);
        lastTower.transform.position = baseBlock.transform.position; // change place of the oldest tower to the new place
        //lastTower.SetActive(true);
        lastTower.GetComponent<Tower>().Init();
        lastTower.GetComponent<TowerTeleporter>().TeleporterInit();
        SetBasePoint(lastTower, baseBlock);
        towersQueue.Enqueue(lastTower);
    }

    void SetBasePoint(GameObject tower, TowerCube baseBlock)
    {
        tower.GetComponent<Tower>().baseBlock = baseBlock;
    }

    void MakeBasePointPlaceable(GameObject tower)
    {
        if(tower.GetComponent<Tower>().baseBlock != null)
        {
            tower.GetComponent<Tower>().baseBlock.isPlaceable = true;
        }
    }

    IEnumerator AutoSpawn() // for debugging
    {
        int x = 0;
        int y = 0;
        while (true)
        {
            x += 10;
            y += 10;
            Vector3 spawnPosition = new Vector3(x, 0, y);
            Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    public void SwitchAutomaticManual(bool manualAim)
    {
        foreach(GameObject tower in towers)
        {
            tower.GetComponent<Tower>().SwitchAutomaticManualAiming(manualAim);
        }
    }
}
