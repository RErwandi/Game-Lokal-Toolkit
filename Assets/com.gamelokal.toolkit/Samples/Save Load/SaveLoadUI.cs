using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public void Save()
    {
        SaveLoadManager.Instance.Save("test");
    }

    public void Load()
    {
        SaveLoadManager.Instance.Load("test");
    }
}
