using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility.MenuManager;
using UnityEngine;

public class TestPanelMenu : Menu<TestPanelMenu>
{
    public override void OnBackPressed()
    {
        base.OnBackPressed();
        TestMainMenu.Open();
    }
}
