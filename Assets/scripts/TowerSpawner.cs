using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour {

    //set in editor
    [SerializeField] public GameObject[] towers;

    //cached data
    Queue<GameObject> towersQueue = new Queue<GameObject>();

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
        EnemiesController.Instance.FindNewPaths();

        var lastTower = towersQueue.Dequeue();
        MakeBasePointPlaceable(lastTower);
        lastTower.transform.position = baseBlock.transform.position; // change place of the oldest tower to the new place
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

    public void SwitchAutomaticManual(bool manualAim)
    {
        foreach(GameObject tower in towers)
        {
            tower.GetComponent<Tower>().SwitchAutomaticManualAiming(manualAim);
        }
    }
}
