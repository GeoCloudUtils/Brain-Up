/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public abstract class AbstractController<T>  : SingleInstanceObject<T> where T:MonoBehaviour
    {
        public bool enableTimer = true;

        public void StartGame(Action<bool, bool> callback) { }
        public void StopGame() { }
        public bool Hint() { return true; }
    }
}
