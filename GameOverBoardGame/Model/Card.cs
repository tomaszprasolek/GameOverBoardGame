using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class Card
    {
        public CardType Type { get; }
        public PlayerType? ChestOwner { get; }
        public Enemy Enemy { get; }

        public Card(CardType type)
        {
            Type = type;
        }

        public Card(CardType type, Enemy enemy)
        {
            Type = type;
            Enemy = enemy;
        }

        private Card(CardType type, PlayerType chestOwner)
        {
            Type = type;
            ChestOwner = chestOwner;
        }

        public static Card CreateChestCard(PlayerType owner)
        {
            return new Card(CardType.Chest, owner);
        }

        public override string ToString()
        {
            var result = Type.ToString();
            if (Enemy != null)
                result += Enemy.Weapon;
            if (ChestOwner != null)
                result += ChestOwner.Value;
            return result;
        }
    }
}
