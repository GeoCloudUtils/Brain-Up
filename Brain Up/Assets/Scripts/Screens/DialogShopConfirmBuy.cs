/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class DialogShopConfirmBuy : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public Image itemIcon;
        public TMP_Text itemCount;
        public DialogShopNoCoins dialogNoMoney = null;
        public DialogShopItemBought dialogItemBought = null;
        private int currItemId;
        private int currItemCount;



        public void SetItem(int id, Sprite icon, int count)
        {
            currItemId = id;
            currItemCount = count;
            itemIcon.sprite = icon;

            if (count > 1)
            {
                itemCount.text = "x"+count;
                itemCount.gameObject.SetActive(true);
            }
            else
            {
                itemCount.gameObject.SetActive(false);
            }
        }


        internal void Show(bool show)
        {
            screen.SetActive(show);
        }


        public void OnConfirm(int selected)
        {
            Show(false);

            if(selected == 1)
            {
                Debug.Log("Purchase confirmed.");
                Database _database = Database.Instance;
                ShopController _controller = ShopController.Instance;
                ShopDataRow item = _controller.GetItem(currItemId);

                if (_database.Coins >= item.price)
                {
                    Debug.Log("Purchase: Item Bought.");
                    _controller.SetAsBought(currItemId);
                    _database.Coins -= item.price;
                    dialogItemBought.SetItem(itemIcon.sprite, currItemCount);
                    dialogItemBought.Show(true);
                }
                else
                {
                    Debug.Log("Purchase: No coins.");
                    _controller.SetAsNonBought(currItemId);
                    dialogNoMoney.Show(true);
                }
            }
        }
    }
}
