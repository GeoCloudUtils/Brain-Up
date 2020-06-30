/*
    Author: Ghercioglo "Romeon0" Roman
 */

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.VM_Colors
{
    public class ClickableImage : MonoBehaviour
    {
        public Image image;
        public Image background;
        public Color selectedColor;
        public Color normalColor;

        public void Select(bool select)
        {
            if(select)
                background.color = selectedColor;
            else
                background.color = normalColor;
        }

        public void ShowImage(bool show)
        {
            image.gameObject.SetActive(show);
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
            ShowImage(true);
        }

        public Sprite GetImage()
        {
            return image.sprite;
        }

        public void OnClick()
        {
            Select(true);
        }
    }
}
