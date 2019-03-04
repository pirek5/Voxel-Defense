using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    [RequireComponent(typeof(ScreenFader))] 
    public class SplashScreen : MonoBehaviour
    {
        private ScreenFader screenFader;
        [SerializeField] private float delay = 0f;

        private void Awake()
        {
            screenFader = GetComponent<ScreenFader>();
        }

        private void Start()
        {
            screenFader.FadeOn();
        }

        public void FadeAndLoad()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        private IEnumerator FadeAndLoadRoutine()
        {
            yield return new WaitForSeconds(delay);
            screenFader.FadeOff();
            LevelLoader.instance.LoadMainMenuLevel();
            yield return new WaitForSeconds(screenFader.FadeOffDuration);
            Destroy(gameObject);
        }
    } 
}
