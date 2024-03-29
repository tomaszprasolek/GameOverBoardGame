﻿using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class GameManager
    {
        private int PlayerIndexCurrentTurn = 1;
        private bool isGameOver = false;
        private NextAction? previousAction;
        private int numberOfPlayers;

        public GameBoard GameBoard { get; private set; }     
        public Player CurrentPlayer { get; private set; }    

        /// <summary>
        /// Nowa gra
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        public void NewGame(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;

            PlayerIndexCurrentTurn = 1;
            isGameOver = false;
            previousAction = NextAction.Move;
            GameBoard = new GameBoard(numberOfPlayers);
            SetCurrentPlayer();
        }

        public void NextPlayer()
        {
            isGameOver = false;
            GameBoard.HideAllCards();

            int currentPlayeridx = PlayerIndexCurrentTurn;
            int nextPlayerIdx = currentPlayeridx + 1;

            if (nextPlayerIdx > numberOfPlayers)
                nextPlayerIdx = 1;

            PlayerIndexCurrentTurn = nextPlayerIdx;
            SetCurrentPlayer();
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

        private void SetCurrentPlayer()
        {
            CurrentPlayer = GameBoard.GetCurrentPlayer(PlayerIndexCurrentTurn);
        }
    }
}