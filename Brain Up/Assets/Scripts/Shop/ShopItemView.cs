using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    [Serializable]
    public class ShopItemView : MonoBehaviour
    {
        [Header("GUI References")]
        public Text price;
        public Button buyButton;
        public Button boughtButton;
        public Image icon;
        public Text itemName;

        [HideInInspector]public ShopItemModel model;
        

        void Start()
        {
            if (price == null) { Debug.LogError("price is not assigned!"); }
            if (buyButton == null) { Debug.LogError("buyButton is not assigned!"); }
            if (boughtButton == null) { Debug.LogError("boughtButton is not assigned!"); }
            if (icon == null) { Debug.LogError("icon is not assigned!"); }
            if (itemName == null) { Debug.LogError("itemName is not assigned!"); }
        }
    }
}
