using GameOverBoardGame.Enums;
using System.Collections.Generic;

namespace GameOverBoardGame.Model
{
    public class GameManager
    {
        public GameBoard GameBoard { get; private set; }

        private Dictionary<int, Player> Players;
        private int PlayerIndexCurrentTurn = 1;

        public Player CurrentPlayer => Players[PlayerIndexCurrentTurn];

        private bool isGameOver = false;

        private NextAction? previousAction;

        /// <summary>
        /// Nowa gra
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        public void NewGame(int numberOfPlayers)
        {
            PlayerIndexCurrentTurn = 1;
            isGameOver = false;
            previousAction = NextAction.Move;
            GameBoard = new GameBoard(numberOfPlayers);
            SetPlayers(numberOfPlayers);
        }

        private void SetPlayers(int numberOfPlayers)
        {
            Players = new Dictionary<int, Player>(numberOfPlayers);
            Players.Add(1, new Player(PlayerType.Girl));
            Players.Add(2, new Player(PlayerType.Handsome));
            if (numberOfPlayers >= 3)
                Players.Add(3, new Player(PlayerType.Fat));
            if (numberOfPlayers >= 4)
                Players.Add(4, new Player(PlayerType.Scared));
        }

        public void NextPlayer()
        {
            isGameOver = false;
            GameBoard.HideAllCards();

            int currentPlayeridx = PlayerIndexCurrentTurn;
            int nextPlayerIdx = currentPlayeridx + 1;

            if (nextPlayerIdx > Players.Count)
                nextPlayerIdx = 1;

            PlayerIndexCurrentTurn = nextPlayerIdx;
        }

        public NextActionInfo PieceClicked(int x, int y, Weapon selectedWeapon)
        {
            if (previousAction == NextAction.GameWin)
                return new NextActionInfo(NextAction.GameWin);
            if (isGameOver)
                return new NextActionInfo(NextAction.GameOver);

            bool isTeleporting = previousAction != null && previousAction == NextAction.Teleport;
            bool isDragonSwitch = previousAction == NextAction.GameOverAndReplaceDragonCard;

            GamePiece piece = GameBoard.PieceClicked(x, y, PlayerIndexCurrentTurn, isTeleporting, isDragonSwitch);
            if (piece == null)
            {
                // Czyli wybrano pole w któe nie można smoka przenieść
                if (isDragonSwitch) 
                    return new NextActionInfo(NextAction.GameOverAndReplaceDragonCard);

                previousAction = NextAction.Move;
                return new NextActionInfo(NextAction.Move);
            }

            if (isDragonSwitch)
            {
                GameBoard.SwitchDragonCard(new System.Drawing.Point(x, y));
                previousAction = NextAction.GameOver;
                isGameOver = true;
                return new NextActionInfo(NextAction.GameOver);
            }

            CurrentPlayer.ChoosenWeapon = selectedWeapon;
            NextAction res = CurrentPlayer.DoAction(piece.Card);

            switch (res)
            {
                case NextAction.GameOver:
                    isGameOver = true;
                    break;
            }

            previousAction = res;
            return new NextActionInfo(res);
        }
    }
}