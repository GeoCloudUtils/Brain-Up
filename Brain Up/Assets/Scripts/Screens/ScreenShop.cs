using Assets.Scripts.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class ScreenShop : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text currencyCount = null;
        //Private members
        private ShopController _controller;
        private Database _database;
        public DialogShopConfirmBuy confirmBuyDialog;


        private void Start()
        {
            _database = Database.Instance;
            _controller = ShopController.Instance;

            //connect to events
            _database.onCoinsCountChanged+= OnCoinsCountChanged;

            //update interface
            OnCoinsCountChanged(0, _database.Coins);
        }



        #region EVENTS

        private void OnCoinsCountChanged(int oldCount, int newCount)
        {
            currencyCount.text = newCount.ToString();
        }

        #endregion


        #region GUI Events
        public void OnBuyRequest(int itemId)
        {
            Debug.Log("Slot with itemId=" + itemId + " clicked!");
            if (_database.IsItemBought(itemId))
            {
                Debug.Log("Item already bought!");
                return;
            }

            ShopDataRow item = _controller.GetItem(itemId);
            confirmBuyDialog.SetItem(itemId,item.icon,1);
            confirmBuyDialog.Show(true);

        }

        public void Show(bool show) => gameObject.SetActive(show);

        #endregion
    }
}