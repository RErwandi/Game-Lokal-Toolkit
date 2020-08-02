using System;
using System.Collections.Generic;
using System.Linq;
using GameLokal.Common;
using GameLokal.Utility.Singleton;
using UnityEngine;

namespace GameLokal.Utility.SaveLoad
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        /// <summary>
        /// Used to remember filename that is currently player using
        /// </summary>
        private string lastSaveFilename = "";
        
        private List<IGameSave> gameSaves = new List<IGameSave>();
        private SaveLoadConfig config;
        
        private const string DEBUG_TYPE = "SaveLoad";
        private const string FILE_EXTENSION = ".gls";

        private void Awake()
        {
            config = SaveLoadConfig.Instance;
        }

        private void Start()
        {
            if (config.useAutoSave)
            {
                InvokeRepeating(nameof(AutoSave), config.autoSaveInterval.MinuteToSecond(), config.autoSaveInterval.MinuteToSecond());
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (string.IsNullOrEmpty(lastSaveFilename) || !pauseStatus || !config.saveOnApplicationPause) return;

            Save(lastSaveFilename);
        }

        private void OnApplicationQuit()
        {
            if (string.IsNullOrEmpty(lastSaveFilename) || !config.saveOnApplicationQuit) return;

            Save(lastSaveFilename);
        }

        private void AutoSave()
        {
            if (string.IsNullOrEmpty(lastSaveFilename))
            {
                Log.Show("Attempting auto save but no current save file is used at the moment.", DEBUG_TYPE);
                return;
            }
            
            Save(lastSaveFilename);
            Log.Show("Auto Saving...", DEBUG_TYPE);
        }

        public void Initialize(IGameSave gameSave)
        {
            gameSaves.Add(gameSave);
        }

        public void Save(string saveFileName = "")
        {
            if (string.IsNullOrEmpty(saveFileName))
            {
                saveFileName = config.defaultFilename;
            }
            
            lastSaveFilename = saveFileName;
            var wrappers = gameSaves.Select(gameSave => new JsonWrapper {uniqueName = gameSave.GetUniqueName(), value = JsonUtility.ToJson(gameSave.GetSaveData())}).ToList();

            SaveLoad.WriteFile(JsonHelper.ToJson(wrappers, true),saveFileName + FILE_EXTENSION);
            Log.Show($"Game saved to {Application.persistentDataPath}/{saveFileName + FILE_EXTENSION}", DEBUG_TYPE);
        }

        public void Load(string loadFileName = "")
        {
            if (string.IsNullOrEmpty(loadFileName))
            {
                loadFileName = config.defaultFilename;
            }

            if (!SaveLoad.FileExist(loadFileName + FILE_EXTENSION))
            {
                Log.Warning($"No save file with name {loadFileName} is found", DEBUG_TYPE);
                return;
            }
            
            lastSaveFilename = loadFileName;
            
            var json = SaveLoad.Read(loadFileName + FILE_EXTENSION);
            var wrappers = JsonHelper.FromJson<JsonWrapper>(json);
            foreach (var gameSave in gameSaves)
            {
                foreach (var generic in from wrapper in wrappers where wrapper.uniqueName == gameSave.GetUniqueName() select JsonUtility.FromJson(wrapper.value, gameSave.GetSaveDataType()))
                {
                    gameSave.ResetData();
                    gameSave.OnLoad(generic);
                }
            }
            
            Log.Show($"Game loaded from {Application.persistentDataPath}/{loadFileName + FILE_EXTENSION}", DEBUG_TYPE);
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