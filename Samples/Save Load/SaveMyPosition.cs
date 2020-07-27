using System;
using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility;
using UnityEngine;

public class SaveMyPosition : MonoBehaviour, IGameSave
{
    void Start()
    {
        SaveLoadManager.Instance.Initialize(this);
    }

    public string GetUniqueName()
    {
        return "some-unique-name";
    }

    public object GetSaveData()
    {
        return transform.position;
    }

    public void ResetData()
    {
        transform.position = Vector3.zero;
    }

    public void OnLoad(object generic)
    {
        var loadedPosition = (Vector3) generic;
        transform.position = loadedPosition;
    }
}
