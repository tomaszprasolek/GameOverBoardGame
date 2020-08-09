using GameOverBoardGame.Enums;
using System;
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

        private Point? previousPoint;

        private List<Point> AlreadyClickedPoints = new List<Point>(8);

        public GameBoard(int numberOfPlayers)
        {
            if (numberOfPlayers <= 1)
                throw new Exception("Za mało graczy!");
            if (numberOfPlayers > 4)
                throw new Exception("Za dużo graczy!");

            this.numberOfPlayers = numberOfPlayers;
            Reset();
        }


        public void HideAllCards()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Board[i, j].Style == PieceStyle.ShowedCard)
                        Board[i, j].Style = PieceStyle.Hidden;
                }
            }

            AlreadyClickedPoints = new List<Point>(8);
            previousPoint = null;
        }

        private void Reset()
        {
            ShuffleCards();
            DrawBoard();
        }

        private Dictionary<int, Player> GetPlayers()
        {
            var players = new Dictionary<int, Player>(numberOfPlayers)
            {
                { 1, new Player(PlayerType.Girl) },
                { 2, new Player(PlayerType.Handsome) }
            };

            if (numberOfPlayers >= 3)
                players.Add(3, new Player(PlayerType.Fat));
            if (numberOfPlayers >= 4)
                players.Add(4, new Player(PlayerType.Scared));

            return players;
        }

        private void DrawBoard()
        {
            Board = new GamePiece[7, 7];

            int cardIdx = 0;
            int playerIdx = 1;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (IsPlayerPoint(i, j))
                    {
                        Board[i, j] = new GamePiece(PieceStyle.Player, GetPlayers()[playerIdx]);
                        playerIdx++;
                        continue;
                    }
                    if (IsCardPoint(i, j))
                    {
                        Board[i, j] = new GamePiece(PieceStyle.Hidden, ShuffledCards[cardIdx]);
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
            cards.Add(new Card(CardType.Chest, PlayerType.Handsome));
            cards.Add(new Card(CardType.Chest, PlayerType.Scared));

            // https://improveandrepeat.com/2018/08/a-simple-way-to-shuffle-your-lists-in-c/
            ShuffledCards = cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public GamePiece PieceClicked(int x, int y, int playerIdx, bool isTeleporting = false, bool isDragonSwitch = false)
        {
            Console.WriteLine("PieceClicked method start");

            if (x < 1 || x > 5)
            {
                Console.WriteLine("X is to low or to high");
                return null;
            }
            if (y < 1 || y > 5)
            {
                Console.WriteLine("Y is to low or to high");
                return null;
            }

            Console.WriteLine($"AlreadyClickedPoints: {AlreadyClickedPoints.Count}");

            if (AlreadyClickedPoints.Count > 0)
            {
                if (AlreadyClickedPoints.Count(a => a == new Point(x, y)) == 1)
                    return null;
            }

            if (previousPoint == null)
                previousPoint = GetPlayersPointOnBoard()[playerIdx - 1];

            Console.WriteLine($"previousPoint: {previousPoint}");

            if (isTeleporting == false && isDragonSwitch == false)
            {
                if (CheckIfPlayerClickCorrectCard(previousPoint.Value.X, previousPoint.Value.Y,
                    x, y) == false)
                    return null;
            }

            if (isDragonSwitch)
            {
                // Cannot place dragon on first spot, next to player. Starting points.
                if (x == 1 && y == 1)
                    return null;
                if (x == 1 && y == 5)
                    return null;
                if (x == 5 && y == 1)
                    return null;
                if (x == 5 && y == 5)
                    return null;
            }
            else
            {
                AlreadyClickedPoints.Add(new Point(x, y));
                previousPoint = new Point(x, y);
            }

            Console.WriteLine("Correct game piece clicked");

            GamePiece piece = Board[x, y];
            if (isDragonSwitch)
                return piece;

            if (piece.Style == PieceStyle.Hidden)
                piece.Style = PieceStyle.ShowedCard;

            return piece;
        }

        private bool CheckIfPlayerClickCorrectCard(int previousX, int previousY, int x, int y)
        {
            int res = x - previousX + (y - previousY);
            if (res == 1 || res == -1)
                return true;
            else
                return false;
        }

        public void SwitchDragonCard(Point newPoint)
        {
            Console.WriteLine($"Prev point: {previousPoint.Value.X}, {previousPoint.Value.Y}");

            Board[previousPoint.Value.X, previousPoint.Value.Y] = Board[newPoint.X, newPoint.Y];
            Board[newPoint.X, newPoint.Y] = new GamePiece(PieceStyle.ShowedCard, new Card(CardType.Dragon));

            Console.WriteLine($"Old dragon place: [{previousPoint.Value.X},{previousPoint.Value.Y}] {Board[previousPoint.Value.X, previousPoint.Value.Y]}");
            Console.WriteLine($"Dragon new place: [{newPoint.X},{newPoint.Y}] {Board[newPoint.X, newPoint.Y]}");
        }
    }
}
