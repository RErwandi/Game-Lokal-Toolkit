using System;
using System.Collections.Generic;
using GameLokal.Utility.SaveLoad;
using UnityEngine;

public class SaveMyState : MonoBehaviour, IGameSave
{
    [System.Serializable]
    public class CustomState
    {
        public string characterName;
        public int characterLevel;
        public float characterExp;
        public List<string> inventory;
    }

    public CustomState state;
    
    void Start()
    {
        SaveLoadManager.Instance.Initialize(this);
    }

    public string GetUniqueName()
    {
        return gameObject.name;
    }

    public object GetSaveData()
    {
        return state;
    }

    public Type GetSaveDataType()
    {
        return typeof(CustomState);
    }

    public void ResetData()
    {
        state = new CustomState();
    }

    public void OnLoad(object generic)
    {
        var loadedState = (CustomState) generic;
        state = loadedState;
    }
}