using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Framework.Assets.Scripts.Database
{
    public class FirebaseController: MonoBehaviour
    {
        private FirebaseApp app;
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    app = FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    Debug.Log("Firebase ready to work hard!");
                }
                else
                {
                    Debug.LogError(System.String.Format(
                       "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}
