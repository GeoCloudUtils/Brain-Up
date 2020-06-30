using System;
using UnityEngine;

namespace Assets.Scripts.Framework.Database
{

    [Serializable]
    public class AbstractDatabaseData
    {

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static T FromString<T>(string str)
        {
            return JsonUtility.FromJson<T>(str);
        }
    }
}
