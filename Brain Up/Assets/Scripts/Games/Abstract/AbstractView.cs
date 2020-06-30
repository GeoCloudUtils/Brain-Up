/*
    Author: Ghercioglo "Romeon0" Roman
 */

using System;
using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public abstract class AbstractView<T> : MonoBehaviour
    {
        public abstract void StartGame(Action callback);
        public abstract void StopGame();
        public abstract void Create(T model);
        public abstract bool Hint();
    }
}
