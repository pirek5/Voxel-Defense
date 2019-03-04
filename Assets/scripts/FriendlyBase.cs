using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBase : MonoBehaviour {
    
    //config
    [SerializeField] private int baseHealth = 20;

    //cached
    UiBar healthbar;

    //state
    public int currentBaseHealth;

    //singleton
    private static FriendlyBase instance;
    public static FriendlyBase Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            healthbar = GetComponentInChildren<UiBar>();
            currentBaseHealth = baseHealth;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void LoseHealth()
    {
        currentBaseHealth--;
        healthbar.value = (float)currentBaseHealth /baseHealth;
        if(currentBaseHealth <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }
}
