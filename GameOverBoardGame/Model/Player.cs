using System.Collections.Generic;
using System.Linq;

namespace GameOverBoardGame.Model
{
    public class Player
    {
        public PlayerType Type { get; }
        public Weapon ChoosenWeapon { get; set; }

        public List<Card> VisitedCards { get; private set; } = new List<Card>(8);

        public Player(PlayerType type)
        {
            Type = type;
        }

        public NextAction DoAction(Card card)
        {
            if (card.Type == CardType.Dragon)
                return NextAction.GameOverAndReplaceDragonCard;

            if (card.Type == CardType.Enemy)
            {
                bool fightResult = card.Enemy.Weapon == ChoosenWeapon;
                if (fightResult == false)
                    return NextAction.GameOver;
                else
                    return NextAction.Move;
            }

            if (card.Type == CardType.Door)
            {
                return NextAction.Teleport;
            }

            VisitedCards.Add(card);

            if (VisitedCards.Count >= 2)
            {
                // Check if player win
                if (VisitedCards.Count(x => x.Type == CardType.Key) >= 1)
                    if (VisitedCards.Count(x => x.Type == CardType.Chest &&
                        x.ChestOwner == Type) == 1)
                        return NextAction.GameWin;
            }
            return NextAction.Move;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
