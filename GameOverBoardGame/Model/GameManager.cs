using System;
using System.Collections.Generic;

namespace GameOverBoardGame.Model
{
    public class GameManager
    {
        public GameBoard GameBoard { get; private set; }

        private readonly int numberOfPlayers;
        private Dictionary<int, Player> Players;
        public int PlayerIndexCurrentTurn { get; private set; } = 1;

        private Player CurrentPlayer => Players[PlayerIndexCurrentTurn];

        private bool isGameOver = false;
        private bool isPlayerWin = false;

        public GameManager(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
            Reset();
        }

        public void Reset()
        {
            isGameOver = false;
            isPlayerWin = false;
            GameBoard = new GameBoard(numberOfPlayers);
            SetPlayers();
        }

        private void SetPlayers()
        {
            Players = new Dictionary<int, Player>(numberOfPlayers);

            if (numberOfPlayers == 2)
            {
                Players.Add(1, new Player(PlayerType.Girl));
                Players.Add(2, new Player(PlayerType.Handsom));
                return;
            }

            throw new NotImplementedException();
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

        public NextAction PieceClicked(int x, int y, Weapon selectedWeapon)
        {
            if (isGameOver)
                return NextAction.GameOver;

            GamePiece piece = GameBoard.PieceClicked(x, y, PlayerIndexCurrentTurn);
            if (piece == null)
                return NextAction.Move;

            CurrentPlayer.ChoosenWeapon = selectedWeapon;
            NextAction res = CurrentPlayer.DoAction(piece.Card);

            switch (res)
            {
                case NextAction.Move:
                    break;
                case NextAction.GameOver:
                    isGameOver = true;
                    //GameBoard.HideAllCards();
                    break;
                case NextAction.GameWin:
                    isPlayerWin = true;
                    break;
                case NextAction.Teleport:
                    break;
                case NextAction.GameOverAndReplaceDragonCard:
                    break;
            }

            return res;
        }
    }
}