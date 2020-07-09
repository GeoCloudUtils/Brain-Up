using Firebase;
using System.Collections;
using UnityEngine;


public class Test_8 : MonoBehaviour
{
    private FirebaseApp app;
    private void Start()
    {
      //  StartCoroutine(StartFirebase());
    }

    private IEnumerator StartFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
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

        yield return null;
    }
}
