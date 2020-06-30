/*
    Author: Ghercioglo "Romeon0" Roman
 */

using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public abstract class AbstractModel : MonoBehaviour
    {
        public abstract void StartGame();
        public abstract void StopGame();
        public abstract void Create();
    }
}
