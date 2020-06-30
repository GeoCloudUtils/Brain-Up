/*
    Author: Ghercioglo "Romeon0" Roman
 */
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.VM_Letters
{
    public class Keyboard : MonoBehaviour
    {
        public Text[] keyTexts;
        public Word word;

        void Start()
        {
            if (keyTexts == null || keyTexts.Length == 0) Debug.LogError(nameof(keyTexts) + " is not assigned!");
            if (word == null) Debug.LogError(nameof(word) + " is not assigned!");

            SetLetters("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
            SetLetters("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".ToCharArray());
            SetLetters("AĂÂBCDEFGHIÎJKLMNOPQRSȘTȚUVWXYZ".ToCharArray());
        }

        public void SetLetters(char[] letters)
        {
            int count = 0;
            int buttonsInRow = keyTexts[0].transform.parent.parent.childCount;
            for (int a = 0; a < keyTexts.Length; ++a)
            {
                if (a < letters.Length)
                {
                    keyTexts[a].text = letters[a].ToString();
                    keyTexts[a].transform.parent.gameObject.SetActive(true);
                    count += 1;
                }
                else
                {
                    keyTexts[a].transform.parent.gameObject.SetActive(false);
                }

                if (((a + 1) % buttonsInRow) == 0)
                {
                    keyTexts[a].transform.parent.parent.gameObject.SetActive(count != 0);
                    count = 0;
                }
            }
        }

        public void OnKeyPressed(Text text)
        {
            Debug.Log("KeyPressed: " + text.text);

            if (word.currSelectedLetterIndex == -1)
                word.SelectLetter(0);

            word.SetLetter(word.currSelectedLetterIndex, char.Parse(text.text));
            if (word.currSelectedLetterIndex < word.GetLettersCount() - 1)
                word.SelectLetter(word.currSelectedLetterIndex + 1);
        }
    }
}