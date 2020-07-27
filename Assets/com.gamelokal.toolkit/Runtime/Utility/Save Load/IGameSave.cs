using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLokal.Utility
{
    public interface IGameSave
    {
        string GetUniqueName();
        object GetSaveData();
        void ResetData();
        void OnLoad(object generic);
    }
}