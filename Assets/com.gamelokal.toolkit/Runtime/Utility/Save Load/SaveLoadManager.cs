using System.Collections.Generic;
using GameLokal.Common;
using GameLokal.Utility.Singleton;
using UnityEngine;

namespace GameLokal.Utility
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
            
            foreach (var gameSave in gameSaves)
            {
                ES3.Save(gameSave.GetUniqueName(), gameSave.GetSaveData(), $"{saveFileName}.es3");
            }
            
            Log.Show($"Game saved to {Application.persistentDataPath}/{saveFileName}", DEBUG_TYPE);
        }

        public void Load(string loadFileName)
        {
            if (!ES3.FileExists($"{loadFileName}.es3"))
                return;
            
            ResetSaveData();
            lastSaveFilename = loadFileName;
            
            foreach (var gameSave in gameSaves)
            {
                gameSave.OnLoad(ES3.Load(gameSave.GetUniqueName(), $"{loadFileName}.es3"));
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
    }
}