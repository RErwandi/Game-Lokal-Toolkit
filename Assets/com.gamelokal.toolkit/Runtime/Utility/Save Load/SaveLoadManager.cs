using System.Collections.Generic;
using System.Linq;
using GameLokal.Common;
using GameLokal.Utility.Singleton;
using UnityEngine;

namespace GameLokal.Utility.SaveLoad
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        private List<IGameSave> gameSaves = new List<IGameSave>();
        private string lastSaveFilename = "";
        private SaveLoadConfig config;
        private const string DEBUG_TYPE = "SaveLoad";

        private void Start()
        {
            config = SaveLoadConfig.Instance;

            InvokeRepeating(nameof(AutoSave), config.autoSaveInterval.MinuteToSecond(), config.autoSaveInterval.MinuteToSecond());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (string.IsNullOrEmpty(lastSaveFilename)) return;
            if (pauseStatus == false) return;
            
            Save(lastSaveFilename);
        }

        private void OnApplicationQuit()
        {
            if (string.IsNullOrEmpty(lastSaveFilename)) return;

            Save(lastSaveFilename);
        }

        private void AutoSave()
        {
            if (string.IsNullOrEmpty(lastSaveFilename)) return;

            Log.Show("Auto Saving...", DEBUG_TYPE);
            Save(lastSaveFilename);
        }

        public void Initialize(IGameSave gameSave)
        {
            gameSaves.Add(gameSave);
        }

        public void Save(string saveFileName)
        {
            lastSaveFilename = saveFileName;
            var wrappers = new List<JsonWrapper>();
            foreach (var gameSave in gameSaves)
            {
                var json = new JsonWrapper();
                json.uniqueName = gameSave.GetUniqueName();
                json.value = JsonUtility.ToJson(gameSave.GetSaveData());
                wrappers.Add(json);
            }
            
            SaveLoad.WriteFile(JsonHelper.ToJson(wrappers, true),saveFileName + ".gl");
            Log.Show($"Game saved to {Application.persistentDataPath}/{saveFileName}", DEBUG_TYPE);
        }

        public void Load(string loadFileName)
        {
            ResetSaveData();
            lastSaveFilename = loadFileName;
            
            if (!SaveLoad.FileExist(loadFileName + ".gl")) return;
            var json = SaveLoad.Read(loadFileName + ".gl");
            var wrappers = JsonHelper.FromJson<JsonWrapper>(json);
            foreach (var gameSave in gameSaves)
            {
                foreach (var generic in from wrapper in wrappers where wrapper.uniqueName == gameSave.GetUniqueName() select JsonUtility.FromJson(wrapper.value, gameSave.GetSaveDataType()))
                {
                    gameSave.OnLoad(generic);
                }
            }
            
            Log.Show($"Game loaded from {Application.persistentDataPath}/{loadFileName}", DEBUG_TYPE);
        }

        private void ResetSaveData()
        {
            foreach (var gameSave in gameSaves)
            {
                gameSave.ResetData();
            }
        }
        
        public void ChangeAutoSaveInterval(float minute)
        {
            config.autoSaveInterval = minute;
            Log.Show($"Auto save interval has been changed to {minute} minute(s)", DEBUG_TYPE);
        }

        /// <summary>
        /// Prevent auto-save until saving or loading is occured
        /// </summary>
        public void ResetLastSavedFilename()
        {
            lastSaveFilename = "";
            Log.Show($"Last saved filename has been cleared. Auto-save is now turned off until saving or loading is occured.", DEBUG_TYPE);
        }

        [System.Serializable]
        private class JsonWrapper
        {
            public string uniqueName;
            public string value;
        }
    }
}