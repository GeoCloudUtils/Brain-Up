//using System;
//using admob;
//using Assets.Scripts.Framework.Other;
//using UnityEngine;

//namespace Assets.Scripts
//{
//    class GoogleAdmobModel : Singleton<GoogleAdmobModel>
//    { 
//        #region Classes

//        [Serializable]
//        public enum AdType
//        {
//            Test = 1,
//            Interstitial = 2,
//            Banner = 3,
//            Reward = 4,
//        }

//        [Serializable]
//        public class AdId
//        {
//            public string id;
//            public AdType type;

//            public AdId(string id, AdType type)
//            {
//                this.id = id;
//                this.type = type;
//            }
//        }

//        #endregion


//        #region Variables

//        [Header("General")]
//        public bool testMode = true;
//        public string packageName = "com.OveractGames.RedBlue.AnotherWay";
//        [Header("Settings for Android")]
//        public string androidAppId = "ca-app-pub-7391836943335428~6106200317";
//        public AdId[] androidAds = new AdId[3]
//        {
//            new AdId("ca-app-pub-7391836943335428/3611705394", AdType.Banner),
//            new AdId("ca-app-pub-7391836943335428/8955246003", AdType.Interstitial),
//            new AdId("ca-app-pub-7391836943335428/9102230444", AdType.Reward)
//        };


//        [Header("Settings for iOS")]
//        public string iosAppId = "insert iOS app id";
//        public AdId[] iosAds = new AdId[3]
//        {
//            new AdId("insert id", AdType.Banner),
//            new AdId("insert id", AdType.Interstitial),
//            new AdId("insert id", AdType.Reward)
//        };
//        //Other
//        public bool Shown { get; set; }=false;
//        //Private
//        private AdId[] _testAds = new AdId[3]
//        {
//            new AdId("ca-app-pub-3940256099942544/6300978111", AdType.Banner),
//            new AdId("ca-app-pub-3940256099942544/8691691433", AdType.Interstitial),
//            new AdId("ca-app-pub-3940256099942544/5224354917", AdType.Reward)
//        };

//        private Admob _admob;
//        private Action<bool> _lastRewardedVideoEndCallback;
//        private Action _lastRewardedVideoShowCallback;

//        #endregion


//        public new void Awake()
//        {
//            if (Application.identifier != packageName)
//            {
//                Debug.LogErrorFormat("GoogleAdmobModel can't be initialized: Wrong packageName(are u missed to update AppId and Ads Ids?)\n" +
//                    "; Wrong:{0}, Correct:{1}", packageName, Application.identifier);
//                return;
//            }

//            base.Awake();
//            _admob = Admob.Instance();
//            // ShowBanner(null, new Vector2(0, 500));
//            Debug.Log("GoogleAdmobModel initialized.");
//            Admob.AdmobEventHandler rewardVideoEventsHandler = new Admob.AdmobEventHandler(OnRewardVideoEvent);
//            _admob.rewardedVideoEventHandler += rewardVideoEventsHandler;
//            Admob.AdmobEventHandler interEventsHandler = new Admob.AdmobEventHandler(OnInterstitialEvent);
//            _admob.interstitialEventHandler += interEventsHandler;

            
//        }




//        #region Public API

//        public void ShowBanner(string bannerName, Vector2 position)
//        {
//            if (bannerName == null) bannerName = "SimpleBanner";
//            if (position == null) position = default;


//            //remove old banner
//            _admob.removeBanner(bannerName);


//            #if UNITY_ANDROID
//                string adId = testMode? _testAds[0].id : androidAds[0].id;
//            #elif UNITY_IPHONE
//                string adId = testMode? _testAds[0].id : iosAds[0].id;
//            #else
//                string adId = "unexpected_platform";
//            #endif

//            //Show new banner
//            //_admob.showBannerAbsolute(adId, AdSize.SMART_BANNER, (int)position.x, (int)position.y, bannerName);
//            _admob.showBannerRelative(adId, AdSize.SMART_BANNER, AdPosition.BOTTOM_CENTER, 0, bannerName);

//            Debug.Log($"Banner '{bannerName}' shown.");
//        }

       
//        public void ShowRewarded(Action<bool> endCallback, Action showCallback = null)
//        {
//            if (Shown) return;

//            #if !UNITY_EDITOR
//                Shown = true;
//            #endif

//            Debug.Log("Rewarded Ad request sending...");

//            #if UNITY_ANDROID
//                string adId = testMode ? _testAds[2].id : androidAds[2].id;
//            #elif UNITY_IPHONE
//                string adId = testMode? _testAds[2].id : iosAds[2].id;
//            #else
//                string adId = "unexpected_platform";
//            #endif

//            _lastRewardedVideoEndCallback = endCallback;
//           _lastRewardedVideoShowCallback = showCallback;

//            _admob.loadRewardedVideo(adId);

//            Debug.Log("Rewarded Ad request sent.");

//            #if UNITY_EDITOR
//            OnRewardVideoEvent("onAdLoaded", null);
//            OnRewardVideoEvent("onAdOpened", null);
//            OnRewardVideoEvent("onRewarded", null);
//            OnRewardVideoEvent("onAdClosed", null);
//            #endif
//        }


//        public void ShowInterstitial()
//        {
//#if UNITY_ANDROID
//            string adId = testMode ? _testAds[1].id : androidAds[1].id;
//#elif UNITY_IPHONE
//                    string adId = testMode? _testAds[1].id : iosAds[1].id;
//#else
//                    string adId = "unexpected_platform";
//#endif

//            _admob.loadInterstitial(adId);
//            _admob.showInterstitial();
//        }


//        #endregion




//        #region Private Methods

//        private void OnRewardVideoEvent(string eventName, string msg)
//        {
//           // Debug.LogFormat("OnRewardVideoEvent. EventName: {0}; msg: {1}", eventName, msg);
//           Debug.Log("EventName: "+ eventName);

//            if (eventName == "onAdLoaded")
//            {
//                _admob.showRewardedVideo();

//                if (_lastRewardedVideoShowCallback != null)
//                    UnityMainThreadDispatcher.Instance.Enqueue(_lastRewardedVideoShowCallback);
//            }
//            else if (eventName == "onAdOpened")
//            {

//            }
//            else if (eventName == "onAdClosed")
//            {
//                if (null != _lastRewardedVideoEndCallback)
//                {
//                    if (_lastRewardedVideoEndCallback != null)
//                        UnityMainThreadDispatcher.Instance.Enqueue(_lastRewardedVideoEndCallback, false);
//                    _lastRewardedVideoEndCallback = null;
//                }

//                Shown = false;
//            }
//            else if (eventName == "onRewarded")
//            {

//                if (_lastRewardedVideoEndCallback != null)
//                    UnityMainThreadDispatcher.Instance.Enqueue(_lastRewardedVideoEndCallback, true);
//                _lastRewardedVideoEndCallback = null;
//            }
//            else
//            {
//                if (null != _lastRewardedVideoEndCallback)
//                {
//                    if (_lastRewardedVideoEndCallback != null)
//                        UnityMainThreadDispatcher.Instance.Enqueue(_lastRewardedVideoEndCallback, false);
//                    _lastRewardedVideoEndCallback = null;
//                }

//                Shown = false;
//            }

//        }

        
//        private void OnInterstitialEvent(string eventName, string msg)
//        {
//            Debug.LogWarning("OnInterstitialEvent. Need implementation");
//        }

//        #endregion
//    }
//}
