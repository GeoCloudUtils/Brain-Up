using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidTriggerNavigation : MonoBehaviour
{
    public GameObject exitTextIndicator;
    void Update()
    {
        ////if running on Android, check for Menu/Home and exit
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
        //    {
        //        if (FindObjectOfType<DestroyMe>() == null)
        //            Instantiate(exitTextIndicator, Vector3.zero, transform.rotation);
        //        else
        //            Application.Quit();
        //        return;
        //    }
        //}
//#if UNITY_ANDROID
//        //added for Android back button reaction
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
//            activity.Call<bool>("moveTaskToBack", true);
//        }
//#endif

    }
}
