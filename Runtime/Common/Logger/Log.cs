using UnityEngine;

namespace GameLokal
{
    public static class Log
    {
        private static bool SHOW_DEBUG = true;
        
        public static void Show(string content, string type , params object[] parameters)
        {
            if (!SHOW_DEBUG) return;
            Debug.LogFormat($"<color=#FF3333>[GL_{type}]</color> {content}", parameters);
        }
    }
}