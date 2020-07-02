///*
//    Author: Ghercioglo Roman (Romeon0)

//    First method(deprecated):
//    Plugin page: https://forum.unity.com/threads/native-plugins-for-unity-ios-android-billing-cloud-save-game-services-webview-sharing-more.316336/
//    Tutorials: https://tutorials.cpnp.voxelbusters.com/Features/Sharing/implementing_feature.html
//    API: http://voxelbusters.github.io/Cross-Platform-Native-Plugins-for-Unity/Documentation/DoxygenOutput/html/annotated.html

//    Second method:
//    https://agrawalsuneet.github.io/blogs/native-android-image-sharing-in-unity-using-fileprovider/
//*/

//using Assets.Scripts.Framework.Other;
//using System;
//using System.Collections;
//using System.Drawing;
//using System.IO;
//using System.Threading.Tasks;
//using Unity.Collections;
//using UnityEngine;
//using UnityEngine.UI;
//using VoxelBusters.NativePlugins;
//using VoxelBusters.Utility;

//namespace Assets.Framework.Assets.Scripts.Sharing
//{
//    class ShareController : SingleInstanceObject<ShareController>
//    {
//        /// <summary>
//        /// Deprecated. Sharing using Native Plugins plugin.
//        /// </summary>

//        //public void ShareSMS(string message, string contact)
//        //{
//        //    //INFO: For sharing with SMS, you first check if the service is available on target platform.
//        //    if (!NPBinding.Sharing.IsMessagingServiceAvailable())
//        //    {
//        //        Debug.LogError("ShareController: Can't share via message. Not possible on this device.");
//        //        return;
//        //    }

//        //    //INFO: Create composer: For sharing, create MessageShareComposer and fill in the details.
//        //    MessageShareComposer composer = new MessageShareComposer();
//        //    composer.Body = message;
//        //    composer.ToRecipients = new string[] { contact };

//        //    //INFO: Show window
//        //    NPBinding.Sharing.ShowView(composer, FinishedSharing);
//        //}

//        //public void ShareByEmail(string subject, string message, string email, 
//        //    bool attachScreenshot = false, Texture2D image = null, Action beforeShot = null, Action afterShot = null, Rect screenshotZone = default)
//        //{
//        //    //INFO: For sharing with SMS, you first check if the service is available on target platform.
//        //    if (!NPBinding.Sharing.IsMailServiceAvailable())
//        //    {
//        //        Debug.LogError("ShareController: Can't share via message. Not possible on this device.");
//        //        return;
//        //    }

//        //    //INFO: Create composer
//        //    MailShareComposer _composer = new MailShareComposer();
//        //    _composer.Subject = subject;
//        //    _composer.Body = message;

//        //    //INFO: Set below to true if the body is HTML
//        //    _composer.IsHTMLBody = false;

//        //    //INFO: Send array of receivers if required
//        //    _composer.ToRecipients = new string[] { email };


//        //    //INFO: If you want to attach screenshot
//        //    if(attachScreenshot)
//        //    {
//        //        if (screenshotZone == default)
//        //            screenshotZone = new Rect(0, 0, Screen.width, Screen.height);
//        //        StartCoroutine(DoScreenshot(_composer, beforeShot, afterShot, screenshotZone));
//        //    }

//        //    //INFO: If you want to attach a file, for ex : image
//        //    if (image != null)
//        //    {
//        //        _composer.AttachImage(image);
//        //    }

//        //    //INFO: If you want to add any other attachment format
//        //    // _composer.AddAttachment(ATTACHMENT_DATA_IN_BYTE_ARRAY, ATTACHMENT_FILE_NAME, MIME_TYPE);

//        //    // Show share view
//        //    NPBinding.Sharing.ShowView(_composer, FinishedSharing);
//        //}

//        //public void ShareSocial(string message, string url=null, Texture2D image = null, 
//        //    bool attachScreenshot=false, Action beforeShot=null, Action afterShot=null, Rect shootZone=default)
//        //{
//        //    // Create share sheet
//        //    SocialShareSheet shareSheet = new SocialShareSheet();


//        //    //INFO: If you want to attach a image
//        //    if (image != null)
//        //        shareSheet.AttachImage(image);

//        //    shareSheet.Text = message;

//        //    // Add below line if you want to share URL
//        //    shareSheet.URL = url;

//        //    // Add below line if you want to share a screenshot
//        //    if (attachScreenshot)
//        //    {
//        //        if (shootZone == default)
//        //            shootZone = new Rect(0, 0, Screen.width, Screen.height);
//        //        StartCoroutine(DoScreenshot(shareSheet, beforeShot, afterShot, shootZone));
//        //    }




//        //    // Show composer
//        //    NPBinding.UI.SetPopoverPointAtLastTouchPosition(); // To show popover at last touch point on iOS. On Android, its ignored.
//        //    NPBinding.Sharing.ShowView(shareSheet, FinishedSharing);
//        //}

//        //#region Helpers

//        //private IEnumerator DoScreenshot(SocialShareSheet sheet, Action beforeShot = null, Action afterShot = null, Rect screenshotZone = default)
//        //{
//        //    beforeShot?.Invoke();
//        //    yield return new WaitForEndOfFrame();
//        //    sheet.ImageAsyncDownloadInProgress = true;


//        //    sheet.AttachScreenShot(screenshotZone);

//        //    while (sheet.ImageAsyncDownloadInProgress)
//        //        yield return null;

//        //    afterShot?.Invoke();

//        //    // Show share view
//        //    NPBinding.Sharing.ShowView(sheet, FinishedSharing);
//        //}

//        //private IEnumerator DoScreenshot(MailShareComposer composer, Action beforeShot, Action afterShot, Rect screenshotZone)
//        //{
//        //    beforeShot?.Invoke();
//        //    yield return new WaitForEndOfFrame();
//        //    composer.ImageAsyncDownloadInProgress = true;
//        //    composer.AttachScreenShot(screenshotZone);
//        //    afterShot?.Invoke();
//        //}



//        //private void AttachImage(ref SocialShareSheet shareSheet, string imgPath)
//        //{
//        //    int index = imgPath.IndexOf('.');
//        //    if (index == -1)
//        //    {
//        //        Debug.LogErrorFormat("Not valid path: {0}; Error: {1}.", imgPath, "No file extension");
//        //        return;
//        //    }

//        //    string fileExt = imgPath.Substring(index + 1, imgPath.Length - index - 1);
//        //    if (!File.Exists(imgPath))
//        //    {
//        //        Debug.LogErrorFormat("Not valid path: {0}; Error: {1}.", imgPath, "File not exists");
//        //        return;
//        //    }

//        //    string mimeType = GetMimeTypeByString(fileExt);
//        //    //Debug.Log("mimeType: " + mimeType);
//        //    if (mimeType != null)
//        //        shareSheet.AttachImageAtPath(imgPath);
//        //}




//        //private void AttachFiles(ref MailShareComposer composer, string[] imagesPaths)
//        //{
//        //    foreach (string imgPath in imagesPaths)
//        //    {
//        //        int index = imgPath.IndexOf('.');
//        //        if (index == -1)
//        //        {
//        //            Debug.LogErrorFormat("Not valid path: {0}; Error: {1}.", imgPath, "No file extension");
//        //            continue;
//        //        }

//        //        string fileExt = imgPath.Substring(index + 1, imgPath.Length - index - 1);
//        //        //Debug.Log("fileExt: " + fileExt);

//        //        if (!File.Exists(imgPath))
//        //        {
//        //            Debug.LogErrorFormat("Not valid path: {0}; Error: {1}.", imgPath, "File not exists");
//        //            continue;
//        //        }

//        //        string mimeType = GetMimeTypeByString(fileExt);
//        //        //Debug.Log("mimeType: " + mimeType);
//        //        if (mimeType == null) continue;

//        //        composer.AddAttachmentAtPath(imgPath, mimeType);
//        //    }
//        //}

//        //private string GetMimeTypeByString(string fileExt)
//        //{
//        //    if (fileExt == "png")
//        //        return MIMEType.kPNG;
//        //    else if (fileExt == "jpeg")
//        //        return MIMEType.kJPEG;
//        //    else if (fileExt == "pdf")
//        //        return MIMEType.kPDF;
//        //    //else return null;

//        //    throw new Exception(string.Format("File extension '{0}' not supported!", fileExt));
//        //}

//        //#endregion


//        //void FinishedSharing(eShareResult _result)
//        //{
//        //    Debug.Log("Sharing finished. Share Result: " + _result);
//        //}


//        private bool isFocus = false;
//        private bool isProcessing = false;
//        private string screenshotName = "screenshot.png";
//        private bool doingScreenshot = false;
//        private Texture2D lastScreenshot;


//        public void Share(string subject, string message, bool makeScreenshot=false, Rect screenshotZone=default, string shareSubject= "Share to...")
//        {

//#if UNITY_ANDROID
//            if (!isProcessing)
//            {
//                StartCoroutine(ShareOnAndroid(subject, message, makeScreenshot, screenshotZone, shareSubject));
//            }
//#else
//		    Debug.LogError("No sharing set up for this platform.");
//#endif
//        }

       

//        private IEnumerator ShareOnAndroid(string subject, string message, bool makeScreenshot, Rect screenshotzone, string shareSubject)
//        {
//            Debug.Log("Sharing...");
//            isProcessing = true;

//            // wait for graphics to render
//            yield return new WaitForEndOfFrame();


//            //Making screenshot
//            string screenShotPath = null;// 
//            if (makeScreenshot)
//            {
//                doingScreenshot = true;
//                screenShotPath = Application.persistentDataPath + "/" + screenshotName; 
//                DoScreenshot(screenshotzone);
//                while (doingScreenshot)
//                {
//                    yield return null;
//                }

//                using (FileStream fs = File.Open(screenShotPath, FileMode.Create))
//                {
//                    using (BinaryWriter writer = new BinaryWriter(fs))
//                    {
//                        byte[] bytes = lastScreenshot.EncodeToJPG();
//                        writer.Write(bytes);
//                    }
//                }
//            }

//            if (!Application.isEditor)
//            {
//                //current activity context
//                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

//                //Create intent for action send
//                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
//                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
//                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
//                Debug.LogFormat("unity: {0}", unity);
//                Debug.LogFormat("currentActivity: {0}", currentActivity);
//                Debug.LogFormat("intentClass: {0}", intentClass);
//                Debug.LogFormat("intentObject: {0}", intentObject);
//                //old code which is not allowed in Android 8 or above
//                //create image URI to add it to the intent
//                //AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
//                //AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + screenShotPath);

//                //create file object of the screenshot captured
//                AndroidJavaObject fileObject = makeScreenshot ? new AndroidJavaObject("java.io.File", screenShotPath) : null;
//                Debug.LogFormat("fileObject: {0}", fileObject);

//                //create FileProvider class object
//                AndroidJavaClass fileProviderClass = new AndroidJavaClass("android.support.v4.content.FileProvider");

//                Debug.LogFormat("fileProviderClass: {0}", fileProviderClass);

//                object[] providerParams = new object[3];
//                providerParams[0] = currentActivity;
//                providerParams[1] = Application.identifier + ".provider";
//                providerParams[2] = fileObject;
//                Debug.LogFormat("providerParams: {0}; {1}; {2}", providerParams[0],
//                    providerParams[1],
//                    providerParams[2]);

//                //instead of parsing the uri, will get the uri from file using FileProvider
//                AndroidJavaObject uriObject = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", providerParams);

//                Debug.LogFormat("uriObject: {0}", uriObject);

//                //put image and string extra
//                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
//                intentObject.Call<AndroidJavaObject>("setType", "image/png");
//                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
//                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

//                //additionally grant permission to read the uri
//                intentObject.Call<AndroidJavaObject>("addFlags", intentClass.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION"));

//                AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareSubject);
//                currentActivity.Call("startActivity", chooser);
//            }

//            yield return new WaitUntil(() => isFocus);
//            isProcessing = false;

//            Debug.Log("Sharing end.");
//        }

        
//        private void DoScreenshot(Rect zone)
//        {
//            doingScreenshot = true;

//            IEnumerator takeScreenShotCoroutine = TextureExtensions.TakeScreenshot((texture) =>
//            {
//                lastScreenshot = texture;
//                doingScreenshot = false;
//            }, zone);

//            NPBinding.Instance.StartCoroutine(takeScreenShotCoroutine);
//        }


//        void OnApplicationFocus(bool focus)
//        {
//            isFocus = focus;
//        }
//    }
//}
