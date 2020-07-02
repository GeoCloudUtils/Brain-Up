using Assets.Scripts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TMPro;
using UnityEngine;
public class Test_4 : MonoBehaviour
{
    public TMP_Text coinsCount;
    public int coins = 0;

    private void Start()
    {
        coinsCount.text = coins.ToString();
    }

    public void ShowBanner()
    {
        GoogleAdmobModel.Instance.ShowBanner("TestBanner", Vector2.zero);
    }

    public void ShowRewardedAd()
    {
        GoogleAdmobModel.Instance.ShowRewarded((watched)=>
        {
            if (watched)
            {
                coins += 50;
                coinsCount.text = coins.ToString();
            }
        });
    }

    public void ShowInterstitialAd()
    {
        GoogleAdmobModel.Instance.ShowInterstitial();
    }
}
