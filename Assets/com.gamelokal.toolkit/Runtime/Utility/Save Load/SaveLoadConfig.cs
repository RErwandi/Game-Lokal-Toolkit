using GameLokal.Common;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace GameLokal.Utility
{
    [GlobalConfig(GlobalConfigHelper.defaultGlobalConfigLocation + "Utility/Save Load/Resources")]
    public class SaveLoadConfig : GlobalConfig<SaveLoadConfig>
    {
        public bool useAutoSave = true;
        [ShowIf("useAutoSave"), Range(1, 100), SuffixLabel("minute(s)")]
        public float autoSaveInterval = 5;
    }
}