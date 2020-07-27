using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace GameLokal.Utility.BetterEvent
{
    public class BetterEventListDrawer : OdinValueDrawer<BetterEvent>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children["Events"].Draw(label);
        }
    }
}