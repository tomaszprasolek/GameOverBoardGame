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

        public Card(CardType type, PlayerType chestOwner)
        {
            Type = type;
            ChestOwner = chestOwner;
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
