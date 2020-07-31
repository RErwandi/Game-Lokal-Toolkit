using System;

namespace GameLokal.Utility.SaveLoad
{
    public interface IGameSave
    {
        string GetUniqueName();
        object GetSaveData();
        Type GetSaveDataType();
        void ResetData();
        void OnLoad(object generic);
    }
}