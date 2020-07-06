using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelScreen_manager : MonoBehaviour
{
    public LevelsGridLayout gridLevelLayout;
    public Level_tab info_Tab;
    public LevelScreen_Button[] level_buttons;

    void Start()
    {
        foreach (LevelScreen_Button btn in level_buttons)
        {
            Button l_button = btn.GetComponent<Button>();
            l_button.onClick.AddListener(delegate { DoLevelButtonClick(btn); });
        }
    }
    private void DoLevelButtonClick(LevelScreen_Button btn)
    {
        if (btn.isLocked)
        {
            info_Tab.gameObject.SetActive(true);
            info_Tab.setTarget(btn);
            return;
        }
        gridLevelLayout.gameObject.SetActive(true);
        gridLevelLayout.SetLevelProperties(btn.startLevelIndex, btn.endLevelIndex);
    }
}
