using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Other
{
    public class LeaderboardPlayerView : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text place;
        public new TMP_Text name;
        public TMP_Text coins;
        public Image avatar;
        [Header("References - our player only")]
        public Image somethingElse;
    }
}
