using System.IO;
using GameLokal.Common;
using UnityEngine;

namespace GameLokal.Utility.SaveLoad
{
    public class SaveLoad
    {
        public static void WriteFile(string json, string filename)
        {
            File.WriteAllText(FilePath(filename), json);
        }

        public static string Read(string filename)
        {
            var path = FilePath(filename);
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            
            Log.Show("File not found", "SaveLoad");
            return "";
        }

        public static bool FileExist(string filename)
        {
            return File.Exists(FilePath(filename));
        }

        private static string FilePath(string filename)
        {
            return Path.Combine(Application.persistentDataPath, filename);
        }
    }
}