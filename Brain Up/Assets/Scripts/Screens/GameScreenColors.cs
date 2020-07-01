/*
    Author: Ghercioglo Roman
 */

using Assets.Scripts.Games;
using Assets.Scripts.Games.RepeatColorsGame;

namespace Assets.Scripts.Screens
{
    public class GameScreenColors : GameScreenLetters, GameScreenAbstract
    {
        private ControllerRepeatColors _controller;  

        protected new void Start()
        {
            base.Start();
            gameId = (int)GameId.RepeatColors;
            _controller = ControllerRepeatColors.Instance;
        }

        public void OnCheckClicked()
        {
            if (_controller.Check() == true)
            {
                globalController.StopGame(GameEndReason.Win);
            }
        }

    }
}
