using GameLokal.Utility;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;


namespace GameLokal.Editor
{
    public class GameLokalToolkitEditor : OdinMenuEditorWindow
    {

        [MenuItem("Tools/GameLokal/Toolkit Editor")]
        private static void OpenWindow()
        {
            var window = GetWindow<GameLokalToolkitEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Add("Save and Load", SaveLoadConfig.Instance);
            return tree;
        }
    }
}