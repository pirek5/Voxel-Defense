using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected Menu menuToOpen;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
            }
        }

        protected virtual void Start()
        {
            var canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.worldCamera = UICamera.Instance.GetComponent<Camera>();
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance = (T)this)
            {
                instance = null;
            }
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }

        }
    }
}
