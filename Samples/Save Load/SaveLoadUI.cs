using GameLokal.Utility.SaveLoad;
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
