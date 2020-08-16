using GameOverBoardGame.Enums;
using System;
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
            NextAction? result = NextAction.Move;

            if (card.Type == CardType.Dragon)
                result = NextAction.GameOverAndReplaceDragonCard;

            if (card.Type == CardType.Enemy)
            {
                bool fightResult = card.Weapon == ChoosenWeapon;
                if (fightResult == false)
                    result = NextAction.GameOver;
                else
                    result = NextAction.Move;
            }

            if (card.Type == CardType.Door)
            {
                result = NextAction.Teleport;
            }

            VisitedCards.Add(card);

            Console.WriteLine("Visited cards:");
            VisitedCards.ForEach(x => Console.WriteLine(x));

            if (VisitedCards.Count >= 2)
            {
                // Check if player win
                if (VisitedCards.Count(x => x.Type == CardType.Key) >= 1)
                    if (VisitedCards.Count(x => x.Type == CardType.Chest &&
                        x.ChestOwner == Type) == 1)
                        return NextAction.GameWin;
            }

            if (result == NextAction.GameOver ||
                result == NextAction.GameOverAndReplaceDragonCard ||
                result == NextAction.GameWin)
                VisitedCards.Clear();

            return result.Value;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
