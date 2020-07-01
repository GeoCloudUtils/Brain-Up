using Assets.Scripts.Framework.Other;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class ShopController : SingleInstanceObject<ShopController>
    {
        private ShopData shopData;


        private void Start()
        {
            shopData = Resources.Load<ShopData>("Shop/ShopItems");
        }

        #region Public API

        public void SetAsBought(int id)
        {
            Database.Instance.SetAsBought(id);
        }

        public void SetAsNonBought(int itemId)
        {
            Database.Instance.SetAsNonBought(itemId);
        }

        public ShopDataRow GetItem(int itemId)
        {
            foreach(ShopDataRow item in shopData.items)
            {
                if (item.id == itemId) 
                    return item;
            }
            return null;
        }

        #endregion
    }
}
