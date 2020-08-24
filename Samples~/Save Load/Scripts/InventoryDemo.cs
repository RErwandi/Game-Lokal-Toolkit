using System;
using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility.Event;
using GameLokal.Utility.SaveLoad;
using GameLokal.Utility.Singleton;
using UnityEngine;

public class InventoryDemo : Singleton<InventoryDemo>, IGameSave
{
    [System.Serializable]
    public class InventoryState
    {
        public int coins;
        public int gems;
        public List<string> items = new List<string>();
    }

    public InventoryState state;

    public int Coin
    {
        get => state.coins;
        set => state.coins = value;
    }
    
    public int Gems
    {
        get => state.gems;
        set => state.gems = value;
    }

    public List<string> Items => state.items;

    private void Start()
    {
        SaveLoadManager.Instance.Initialize(this);
    }

    public void AddCoins()
    {
        Coin += 1000;
        GameEvent.Trigger("Coins Updated");
    }

    public void AddGems()
    {
        Gems += 1000;
        GameEvent.Trigger("Gems Updated");
    }

    public void AddItems()
    {
        Items.Add("Wooden Sword");
        GameEvent.Trigger("Items Updated");
    }

    public string GetUniqueName()
    {
        return "inventory";
    }

    public object GetSaveData()
    {
        return state;
    }

    public Type GetSaveDataType()
    {
        return typeof(InventoryState);
    }

    public void ResetData()
    {
        state = new InventoryState();
    }

    public void OnLoad(object generic)
    {
        var loaded = (InventoryState) generic;
        state = loaded;
        GameEvent.Trigger("Coins Updated");
        GameEvent.Trigger("Gems Updated");
        GameEvent.Trigger("Items Updated");
    }
}
