/*
    Author: Ghercioglo "Romeon0" Roman
 */

using System;
using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public interface ModelAbstract
    {
        void StartGame();
        void StopGame();
        void Create();

        GameId GameId { get; set; }
    }
}
