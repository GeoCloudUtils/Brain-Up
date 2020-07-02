/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class DialogShopItemBought : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public Image itemIcon;
        public TMP_Text itemCount;

        private void Start()
        {

        }

        public void SetItem(Sprite icon, int count)
        {
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


        public void OnGamesListClicked()
        {
            Show(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
        }

        public void OnCloseClicked()
        {
            Show(false);
        }
    }
}
