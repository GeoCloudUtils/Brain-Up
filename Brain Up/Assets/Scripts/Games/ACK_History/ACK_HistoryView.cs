/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games.VM_Letters;
using System;
using UnityEngine;


namespace Assets.Scripts.Games.ACK_History
{
    public class ACK_HistoryView : VM_LettersView
    {
        private ACK_HistoryModel _model;
         
        public void Create(ACK_HistoryModel model)
        {
            this._model = model;
        }


        public override void StartGame(Action endCallback = null)
        {
            Debug.Log("HistoryController: Starting game...");

            int progress = Database.Instance.GetGameProgress((int)GameId.Acknowledge_History);
            CatWord wordInfo = _model.GetCurrentWord(); 
            description.text = wordInfo.description;

            string w = wordInfo.word.ToUpper();
            char[] letters = w.ToCharArray();
            word.SetText(w);
            word.HideAllLetters();
            Debug.Log("HistoryController: word - " + w);
            gameScreen.Show(true);
            endCallback?.Invoke();
            Debug.Log("HistoryController: Game started.");
        }

    }
}
