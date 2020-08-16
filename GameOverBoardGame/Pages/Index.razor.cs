using GameOverBoardGame.Enums;
using GameOverBoardGame.Model;
using System;

namespace GameOverBoardGame.Pages
{
    public partial class Index
    {
        private GameManager game;
        private Weapon selectedWeapon;
        private NextActionInfo nextAction = new NextActionInfo(NextAction.Move);
        private int numbersOfPlayers = 2;

        public Index()
        {
            game = new GameManager();
            game.NewGame(numbersOfPlayers);
        }

        private void RadioButtonClicked(string weapon)
        {
            selectedWeapon = (Weapon)Enum.Parse(typeof(Weapon), weapon);
        }

        private void OnClick(int x, int y)
        {
            nextAction = game.PieceClicked(x, y, selectedWeapon);
        }

        private void NextPlayer()
        {
            nextAction = new NextActionInfo(NextAction.Move);
            game.NextPlayer();
        }
    }
}