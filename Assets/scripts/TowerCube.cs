using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCube : MonoBehaviour {

    public bool isPlaceable = true; // sprawdza czy da się położyć kolejna wieże (wykorzystywane przez "interactiveObject" i "TowerSpawner"

    public void PlaceTower()
    {
        if (isPlaceable)
        {
            TowerSpawner.Instance.MoveTower(this);
            isPlaceable = false;
        }
    }
}
