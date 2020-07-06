/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games.GameData.Question_;
using UnityEngine;

namespace Assets.Scripts.Games.GameData.MultipleAnswersQuestion
{
    [CreateAssetMenu(fileName = "MultipleAnswersQuestion", menuName = "ScriptableObjects/MultipleAnswersQuestion", order = 1)]
    public class MultipleAnswersQuestion : ScriptableObject
    {
        public Question[] questions;
    }
}
