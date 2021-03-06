﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

public class GameManager : MonoBehaviour {

    //set in editor
    public PathCube endWaypoint;
    [SerializeField] private GameObject UIMenu;

    //config
    private static int gridSize = 10;
    public static int GridSize{ get { return gridSize; } }
    [SerializeField] public float pathCubeGoingUpTime, pathCubeGoingDownTime, towerTeleportReloadTime, blockMoverReloadTime;

    //state
    private bool scoreSent = false;
    public bool ScoreSent { get{return scoreSent;} set{scoreSent = value;} }

    //singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            UIMenu.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void StartGame()
    {
        OnOffUi();
        EnemiesController.Instance.Init();
        TowerSpawner.Instance.TowersInit();
    }

    public void EndGame()
    {
        OnOffUi();
        EnemiesController.Instance.StopEnemiesMovement();
        var towers = FindObjectsOfType<Tower>();
        foreach(Tower tower in towers)
        {
            tower.Stop();
        }
        LoseScreen.Open();
    }

    public void OnOffUi() // Ui is disabled before start, when game is paused and after end of the game
    {
        UIMenu.SetActive(!UIMenu.activeSelf);
    }
}
