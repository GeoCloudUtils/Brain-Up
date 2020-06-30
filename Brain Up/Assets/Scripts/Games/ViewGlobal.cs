/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Framework.General;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Games
{
    public class ViewGlobal: MonoBehaviour
    {
        [Header("References")]
        public TMP_Text timer;
        //
        private ModelGlobal model;


        internal void Create(ModelGlobal model)
        {
            this.model = model;
        }

        internal void UpdateTimer(float currTime)
        {
            timer.text = TimeHelper.FormatMMSS(currTime);
        }
    }
}
