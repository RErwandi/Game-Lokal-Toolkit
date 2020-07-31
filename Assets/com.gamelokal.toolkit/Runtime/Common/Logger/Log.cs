using UnityEngine;

namespace GameLokal.Common
{
    public static class Log
    {
        private static bool SHOW_DEBUG = true;
        
        public static void Show(string content, string type, params object[] parameters)
        {
            if (!SHOW_DEBUG) return;
            Debug.LogFormat($"<color=#FF3333>[GL_{type}]</color> {content}", parameters);
        }
        
        public static void Warning(string content, string type, params object[] parameters)
        {
            if (!SHOW_DEBUG) return;
            Debug.LogFormat(LogType.Warning, LogOption.None, null, $"<color=#FF3333>[GL_{type}]</color> {content}", parameters);
        }
    }
}