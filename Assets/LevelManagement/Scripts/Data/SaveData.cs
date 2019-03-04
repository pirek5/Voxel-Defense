using System.Collections;
using System.Collections.Generic;
using System;

namespace LevelManagement.Data
{

    [Serializable]
    public class SaveData
    {
        public string playerName;

        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;

        public string hashValue;

        public SaveData()
        {
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            hashValue = String.Empty;
        }
    } 
}
