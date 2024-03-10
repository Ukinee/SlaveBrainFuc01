using Codebase.PlayerData.Services.Interfaces;
using UnityEngine;

namespace Codebase.PlayerData.Services.Implementations
{
    public class PlayerPrefsSaveLoadService : ISaveLoadService
    {
        public void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string Load(string key)
        {
            if (HasSave(key) == false)
                throw new System.Exception("Key not found in PlayerPrefs");

            return PlayerPrefs.GetString(key);
        }

        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public bool HasSave(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}
