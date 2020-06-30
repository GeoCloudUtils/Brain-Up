/*
    Author: Ghercioglo "Romeon0" Roman
 */

using System;
using UnityEngine;

namespace Assets.Scripts.Games.__Other
{
    [Serializable]
    public class ModuleDescriptionRow
    {
        public GameId gameId;
        public string description;
    }

    [CreateAssetMenu(fileName = "ModuleDescriptions", menuName = "ScriptableObjects/ModuleDescriptions", order = 1)]
    public class ModuleDescriptions : ScriptableObject
    {
        public ModuleDescriptionRow[] descriptions;
    }
}
