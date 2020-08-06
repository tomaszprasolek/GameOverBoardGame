namespace GameOverBoardGame.Model
{
    public class Card
    {
        public CardType Type { get; }
        public PlayerType ChestOwner { get; }
        public Enemy Enemy { get; }

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
    }
}
