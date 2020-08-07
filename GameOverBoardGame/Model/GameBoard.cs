﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOverBoardGame.Model
{
    public class GameBoard
    {
        private readonly int numberOfPlayers;

        public List<Card> ShuffledCards { get; private set; }

        public GamePiece[,] Board { get; set; }

        public GameBoard(int numberOfPlayers)
        {
            if (numberOfPlayers <= 1)
                throw new Exception("Za mało graczy!");
            if (numberOfPlayers > 4)
                throw new Exception("Za dużo graczy!");

            this.numberOfPlayers = numberOfPlayers;
            Reset();
        }

        private void Reset()
        {
            ShuffleCards();
            DrawBoard();
        }

        private void DrawBoard()
        {
            Board = new GamePiece[7, 7];

            int cardIdx = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (IsPlayerPoint(i, j))
                    {
                        Board[i, j] = new GamePiece(PieceStyle.Player);
                        continue;
                    }
                    if (IsCardPoint(i, j))
                    {
                        Board[i, j] = new GamePiece(PieceStyle.HiddenCard, ShuffledCards[cardIdx]);
                        cardIdx++;
                        continue;
                    }

                    Board[i, j] = new GamePiece(PieceStyle.Empty);
                }
            }
        }

        private List<Point> GetPlayersPointOnBoard()
        {
            if (numberOfPlayers == 2)
            {
                return new List<Point>
                {
                    new Point(1, 0),
                    new Point(5, 6)
                };
            }

            return new List<Point>
            {
                new Point(1, 0),
                new Point(0, 5),
                new Point(5, 6),
                new Point(6, 1),
            };
        }

        private bool IsCardPoint(int x, int y)
        {
            if (x >= 1 && x <= 5)
                if (y >= 1 && y <= 5)
                    return true;

            return false;
        }

        private bool IsPlayerPoint(int x, int y)
        {
            List<Point> playersPointOnBoard = GetPlayersPointOnBoard();

            var newPoint = new Point(x, y);

            if (playersPointOnBoard.Count(x => x == newPoint) == 1)
                return true;
            return false;
        }

        private void ShuffleCards()
        {
            List<Card> cards = new List<Card>(25);

            // 4 enemies with axe
            for (int i = 0; i < 4; i++)
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Axe)));
            // 4 enemies with bow
            for (int i = 0; i < 4; i++)
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Bow)));
            // 4 enemies with gun
            for (int i = 0; i < 4; i++)
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Gun)));
            // 4 enemies with bomb
            for (int i = 0; i < 4; i++)
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Bomb)));
            // 2 keys
            cards.Add(new Card(CardType.Key));
            cards.Add(new Card(CardType.Key));
            // 2 dragons
            cards.Add(new Card(CardType.Dragon));
            cards.Add(new Card(CardType.Dragon));
            // 1 door
            cards.Add(new Card(CardType.Door));
            // 4 chests
            cards.Add(new Card(CardType.Chest, PlayerType.Fat));
            cards.Add(new Card(CardType.Chest, PlayerType.Girl));
            cards.Add(new Card(CardType.Chest, PlayerType.Handsom));
            cards.Add(new Card(CardType.Chest, PlayerType.Scared));

            // https://improveandrepeat.com/2018/08/a-simple-way-to-shuffle-your-lists-in-c/
            ShuffledCards = cards.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }

    public class GamePiece
    {
        public PieceStyle Style { get; }
        public Player Player { get; }
        public Card Card { get; }

        public GamePiece(PieceStyle style)
        {
            Style = style;
        }

        public GamePiece(PieceStyle style, Player player)
        {
            Style = style;
            Player = player;
        }

        public GamePiece(PieceStyle style, Card card)
        {
            Style = style;
            Card = card;
        }
    }

    public enum PieceStyle
    {
        Empty,
        Player,
        HiddenCard,
        ShowedCard
    }
}
