using System.Collections;
using System.Collections.Generic;
using GameLokal.Utility.MenuManager;
using UnityEngine;

public class TestMainMenu : Menu<TestMainMenu>
{
    public void OpenPanel()
    {
        TestPanelMenu.Open();
    }

    public void OpenPopup()
    {
        TestPopupMenu.Open();
    }
}
