/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Screens;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public interface ViewAbstract
    {
        void StartGame(Action callback);
        void StopGame();
        bool Hint();
        void SetModel(ModelAbstract model);
        GameScreenAbstract GetScreen();
        ModelAbstract GetModel();
    }
}
