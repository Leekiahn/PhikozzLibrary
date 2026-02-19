using System;

namespace PhikozzLibrary
{
    [Serializable]
    public class SaveData
    {
        public PlayerData PlayerData;
        public SettingsData SettingsData;
        public string SaveTime;
    }

    [Serializable]
    public class PlayerData
    {
    }

    [Serializable]
    public class SettingsData
    {
    }
}