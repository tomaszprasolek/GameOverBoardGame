using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class Enemy
    {
        public Weapon Weapon { get; }

        public Enemy(Weapon weapon)
        {
            Weapon = weapon;
        }
    }
}
