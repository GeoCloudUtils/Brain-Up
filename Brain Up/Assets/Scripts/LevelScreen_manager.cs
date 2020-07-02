using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelScreen_manager : MonoBehaviour
{
    public Level_tab info_Tab;
    public Level_button[] level_buttons;

    void Start()
    {
        foreach (Level_button btn in level_buttons)
            btn.onClick.AddListener(delegate { DoLevelButtonClick(btn); });
    }
    private void DoLevelButtonClick(Level_button btn)
    {
        if (btn.isLocked)
        {
            info_Tab.gameObject.SetActive(true);
            info_Tab.setTarget(btn);
            return;
        }
        //do stuff
    }
}
