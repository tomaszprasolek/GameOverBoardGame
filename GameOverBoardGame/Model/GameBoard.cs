using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameOverBoardGame.Model
{
    public class GameBoard
    {
        public GameBoard()
        {
            Reset();
        }

        private void Reset()
        {
            List<Card> cards = new List<Card>(25);
            // 4 enemies with axe
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Axe)));
            }
            // 4 enemies with bow
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Bow)));
            }
            // 4 enemies with gun
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Gun)));
            }
            // 4 enemies with bomb
            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card(CardType.Enemy, new Enemy(Weapon.Bomb)));
            }
        }
    }

    public class GamePiece
    {

    }

    public enum PieceStyle
    {
        Empty,
        Player,
        HiddenCard,
    }
}
