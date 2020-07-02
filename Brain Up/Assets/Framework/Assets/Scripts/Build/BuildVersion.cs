/*
 
Author: Ghercioglo Roman (Romeon0)

*/

using UnityEngine;

namespace Assets.Scripts.Framework.Build
{
    [CreateAssetMenu(fileName = "BuildVersion", menuName = "ScriptableObjects/BuildVersion")]
    class BuildVersion : ScriptableObject
    {
        public string version;
    }
}
