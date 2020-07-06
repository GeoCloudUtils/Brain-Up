using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsGridLayout : MonoBehaviour
{
    public Transform grid_content;
    public levelButton leveButtonPrefab;
    public Button backButton;
    protected int start_index = -1;
    protected int end_index;

    private void Awake()
    {
        Clear();
    }
    private void OnDisable()
    {
        Clear();
    }
    void Start()
    {
        GetComponent<DOTweenAnimation>().DOPlay();
        backButton.onClick.AddListener(CloseLevelScreen);
    }

    private void CloseLevelScreen()
    {
        Clear();
        gameObject.SetActive(false);
    }

    private void Clear()
    {
        int childs = grid_content.childCount;
        for (int i = 0; i < childs; i++)
            GameObject.Destroy(grid_content.GetChild(i).gameObject);
    }

    public void SetLevelProperties(int start_levelIndex, int end_levelIndex)
    {
        start_index = start_levelIndex;
        end_index = end_levelIndex;
        LoadContent();
    }

    protected void LoadContent()
    {
        for (int i = 0; i < end_index; i++)
        {
            levelButton button_level = Instantiate(leveButtonPrefab, Vector3.zero, transform.rotation);
            button_level.transform.SetParent(grid_content);
            button_level.GetComponent<RectTransform>().localScale = Vector3.one;
            button_level.SetNumber(start_index + i);
            button_level.SetAcces(start_index + i <= end_index / 2);
            button_level.SetMostAdvanced(start_index + i == end_index / 2);
        }
    }

}
