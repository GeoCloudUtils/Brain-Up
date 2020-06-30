/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.GameData.CategorizedWordsDictionary
{
    [Serializable]
    public class CatWord
    {
        public string word;
        public string description;
        [SerializeField] public string[] hints;
    }

    [Serializable]
    public class CatWordRow
    {
        public string category;
        [SerializeField]public CatWord[] words;
    }

    [CreateAssetMenu(fileName = "CategorizedWordsDictionary", menuName = "ScriptableObjects/CategorizedWordsDictionary", order = 1)]
    public class CategorizedWordsDictionary: ScriptableObject
    {
        public CatWordRow[] words;
    }
}
