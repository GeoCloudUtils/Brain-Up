using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Nav_manager : MonoBehaviour
{
    public Button file_button;
    public Button section_level_button;
    public Button home_button;
    public Button cart_button;
    public Button edit_button;
    public Button[] allButtons;
    public GameObject level_panel;
    public GameObject cart_panel;
    public GameObject game_grid;

    void Start()
    {
        file_button.onClick.AddListener(OpenFilePanel);
        section_level_button.onClick.AddListener(OpenLevelPanel);
        home_button.onClick.AddListener(OpenHomeScreen);
        cart_button.onClick.AddListener(OpenCartPanel);
        edit_button.onClick.AddListener(OpenEditPanel);
        home_button.transform.GetChild(0).gameObject.SetActive(true);
        game_grid.SetActive(true);
        game_grid.GetComponent<DOTweenAnimation>().DOPlay();
    }

    private void OpenEditPanel()
    {
        onButtonClick(edit_button);
    }

    private void OpenCartPanel()
    {
        onButtonClick(cart_button);
    }

    private void OpenHomeScreen()
    {
        onButtonClick(home_button);
    }

    private void OpenLevelPanel()
    {
        onButtonClick(section_level_button);
    }

    private void OpenFilePanel()
    {
        onButtonClick(file_button);
    }

    private void onButtonClick(Button target)
    {
        if (target.transform.childCount > 0)
        {
            if (target.transform.GetChild(0).gameObject.activeSelf)
                return;
            switch (target.name)
            {
                case "file_button":
                    game_grid.SetActive(false);
                    break;
                case "adv_menu":
                    level_panel.SetActive(true);
                    if (!level_panel.GetComponent<DOTweenAnimation>().tween.IsPlaying())
                        level_panel.GetComponent<DOTweenAnimation>().DOPlay();
                    cart_panel.SetActive(false);
                    game_grid.SetActive(false);
                    break;
                case "home":
                    level_panel.SetActive(false);
                    cart_panel.SetActive(false);
                    game_grid.SetActive(true);
                    if(!game_grid.GetComponent<DOTweenAnimation>().tween.IsPlaying())
                        game_grid.GetComponent<DOTweenAnimation>().DOPlay();
                    break;
                case "cart":
                    cart_panel.SetActive(true);
                    if (!cart_panel.GetComponent<DOTweenAnimation>().tween.IsPlaying())
                        cart_panel.GetComponent<DOTweenAnimation>().DOPlay();
                    level_panel.SetActive(false);
                    game_grid.SetActive(false);
                    break;
                case "edit":
                    game_grid.SetActive(false);
                    break;
                default:
                    break;
            }
            target.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1.3f);
            target.transform.GetChild(0).gameObject.SetActive(true);
            DisableOther(target);
        }
    }
    private void DisableOther(Button target)
    {
        foreach(Button btn in allButtons)
        {
            if (btn != target)
            {
                btn.GetComponent<RectTransform>().localScale = Vector3.one;
                btn.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }
}
