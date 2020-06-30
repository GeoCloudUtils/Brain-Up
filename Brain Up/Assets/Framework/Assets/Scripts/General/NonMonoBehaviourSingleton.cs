/*
 
Author: Unknown

*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Framework.Other
{
    public class NonMonoBehaviourSingleton<T> where T : class, new()
    {
        // Check to see if we're about to be destroyed.
        private static bool _mShuttingDown = false;
        private static object _mLock = new object();
        private static T _mInstance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_mShuttingDown)
                {
                     Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                         "' already destroyed. Returning null.");
                    return default(T);
                }
               
                lock (_mLock)
                {
                    // Create new instance if one doesn't already exist.
                    if (_mInstance == null)
                        _mInstance = new T();

                    return _mInstance;
                }
            }
        }


        protected NonMonoBehaviourSingleton() { _mShuttingDown = false; }
    }
}