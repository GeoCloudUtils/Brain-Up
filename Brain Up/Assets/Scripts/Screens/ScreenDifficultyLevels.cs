using Assets.Scripts.Games;
using Assets.Scripts.Games.Gamedata.TripleValueList;
using Assets.Scripts.Games.GameData.DifficultyData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.Games.GameData.SimpleWordsDictionary;
using Assets.Scripts.Games.Other;
using Assets.Scripts.Screens;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDifficultyLevels : MonoBehaviour
{
    public GameObject screen;
    public UIDifficultyLevel[] levels;
    public ScreenSelectModule selectModuleScreen;
    private GameId currGameId;


    public void InitScreen(GameId gameId)
    {
        currGameId = gameId;
        foreach (UIDifficultyLevel level in levels)
            level.SetDifficulty(gameId);
    }

    public void Show(bool show)
    {
        screen.SetActive(show);
    }

    public void OnExitClicked()
    {
        Show(false);
        selectModuleScreen.Show(true);
    }

    public void OnLevelClicked(UIDifficultyLevel uiButton)
    {
        Debug.Log("Hello!--_");
        if (uiButton.locked)
        {
            Debug.LogWarning("The level is locked!");
            return;
        }

        Show(false);
        GameScreenGlobal.Instance.Show(true);
        var global = ControllerGlobal.Instance;
        global.SetGameDifficulty(uiButton.difficulty);
        global.StartGame(currGameId);
    }
}
