using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{

    public class GameMenu : Menu<GameMenu>
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                OnPausePressed();
            }
        }

        public void OnPausePressed()
        {
            Time.timeScale = 0;
            PauseMenu.Open();
        }
    }
}



