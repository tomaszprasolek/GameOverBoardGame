using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class Card
    {
        public CardType Type { get; }
        public PlayerType? ChestOwner { get; }
        public Weapon? Weapon { get; }

        private Card(CardType type)
        {
            Type = type;
        }

        private Card(CardType type, Weapon weapon)
        {
            Type = type;
            Weapon = weapon;
        }

        private Card(CardType type, PlayerType chestOwner)
        {
            Type = type;
            ChestOwner = chestOwner;
        }

        public static Card CreateCard(CardType type)
        {
            return new Card(type);
        }

        public static Card CreateChestCard(PlayerType owner)
        {
            return new Card(CardType.Chest, owner);
        }

        public static Card CreateEnemyCard(Weapon Weapon)
        {
            return new Card(CardType.Enemy, Weapon);
        }

        public override string ToString()
        {
            var result = Type.ToString();
            if (Weapon != null)
                result += Weapon;
            if (ChestOwner != null)
                result += ChestOwner.Value;
            return result;
        }
    }
}
