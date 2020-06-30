/*
    Author: Ghercioglo Roman
 */

using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Colors;

namespace Assets.Scripts.Screens
{
    public class ColorsGameScreen : LettersGameScreen, AbstractScreen
    {
        private VM_ColorsController _controller;  

        protected new void Start()
        {
            base.Start();
            gameId = (int)GameId.VisualMemory_Colors;
            _controller = VM_ColorsController.Instance;
        }

        public new void OnCheckClicked()
        {
            if (_controller.Check() == true)
            {
                globalController.StopGame(GameEndReason.Win);
            }
        }

    }
}
