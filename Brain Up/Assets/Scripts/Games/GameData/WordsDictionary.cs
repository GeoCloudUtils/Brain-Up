/*
    Author: Ghercioglo "Romeon0" Roman
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.Gamedata.WordsDictionary
{
    [Serializable]
    public class WordRow
    {
        public string word;
        public string description;
    }

    [CreateAssetMenu(fileName = "WordsDictionary", menuName = "ScriptableObjects/WordsDictionary", order = 1)]
    public class WordsDictionary: ScriptableObject
    {
        public WordRow[] words;
    }
}
