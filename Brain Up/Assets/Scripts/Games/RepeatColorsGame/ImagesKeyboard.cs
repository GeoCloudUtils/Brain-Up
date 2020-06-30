/*
    Author: Ghercioglo "Romeon0" Roman
 */
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.RepeatColorsGame
{
    public class ImagesKeyboard : MonoBehaviour
    {
        public Image[] keyImages;
        public ImagesWord word;

        public Sprite[] testLetters;


        void Start()
        {
            if (keyImages == null || keyImages.Length == 0) Debug.LogError(nameof(keyImages) + " is not assigned!");
            if (word == null) Debug.LogError(nameof(word) + " is not assigned!");

            SetLetters(testLetters);
        }

        public void SetLetters(Sprite[] letters)
        {
            int count = 0;
            int buttonsInRow = keyImages[0].transform.parent.parent.childCount;
            Debug.Log("ImagesKeyboad::buttonsInRow: " + buttonsInRow + "; Name: " + keyImages[0].transform.name);
            for (int a = 0; a < keyImages.Length; ++a)
            {
                if (a < letters.Length)
                {
                    keyImages[a].sprite = letters[a];
                    keyImages[a].transform.parent.gameObject.SetActive(true);
                    count += 1;
                }
                else
                {
                    keyImages[a].transform.parent.gameObject.SetActive(false);
                }

                if (((a + 1) % buttonsInRow) == 0)
                {
                    keyImages[a].transform.parent.parent.gameObject.SetActive(count != 0);
                    count = 0;
                }
            }
        }

        public void OnKeyPressed(Image image)
        {
            Debug.Log("KeyPressed!");

            if (word.currSelectedLetterIndex == -1)
                word.SelectLetter(0);

            word.SetLetter(word.currSelectedLetterIndex, image.sprite);
            if (word.currSelectedLetterIndex < word.GetLettersCount() - 1)
                word.SelectLetter(word.currSelectedLetterIndex + 1);
        }
    }
}
