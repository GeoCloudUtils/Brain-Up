/*
    Author: Ghercioglo "Romeon0" Roman
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Letters
{
    [CreateAssetMenu(fileName = "SimpleWordsDictionary", menuName = "ScriptableObjects/SimpleWordsDictionary", order = 1)]
    public class SimpleWordsDictionary : ScriptableObject
    {
        public string[] words;
    }
}
