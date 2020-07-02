/*
    Author: Ghercioglo "Romeon0" Roman
 */

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.__Other
{
    public class StatesImage : MonoBehaviour
    {
        public Image overlay;
        public Image image;
        public TMP_Text text;
        public Color[] states;
        public Func<StatesImage, int> CheckState;
        public int index;
        public int currState;

        public void SetState(int index)
        {
            overlay.color = states[index];
            currState = index;
        }

        public void ShowImage(bool show)
        {
            image.gameObject.SetActive(show);
        }

        public void ShowText(bool show)
        {
            text.gameObject.SetActive(show);
        }

        public void OnClick()
        {
            int state = 0;
            if(CheckState!=null)
                state = CheckState.Invoke(this);

            SetState(state);
        }

        internal int GetState()
        {
            return currState;
        }
    }
}
