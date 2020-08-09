using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class GamePiece
    {
        public PieceStyle Style { get; set; }
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

        public override string ToString()
        {
            if (Style == PieceStyle.Hidden)
                return Style.ToString();

            string result = string.Empty;
            if (Style != PieceStyle.ShowedCard)
                result = Style.ToString();
            if (Player != null)
                result += Player.ToString();
            if (Card != null)
                result += Card.ToString();

            return result;
        }
    }
}
