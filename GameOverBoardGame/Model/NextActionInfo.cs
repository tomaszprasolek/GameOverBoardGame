using GameOverBoardGame.Enums;

namespace GameOverBoardGame.Model
{
    public class NextActionInfo
    {
        public NextAction NextAction { get; }
        public string Information { get; private set; }
        public string AlertType { get; private set; }           

        public NextActionInfo(NextAction nextAction)
        {
            NextAction = nextAction;
            SetCurrentActionInformation();
            SetCurrentActionAlertType();
        }

        private void SetCurrentActionInformation()
        {
            string info = string.Empty;

            switch (NextAction)
            {
                case NextAction.Move:
                    info = "Rusz się. Wybierz kartę.";
                    break;
                case NextAction.GameOver:
                    info = "Niestety przegrałeś. Game over";
                    break;
                case NextAction.GameWin:
                    info = "Brawo wygrałeś";
                    break;
                case NextAction.Teleport:
                    info = "Znalazłeś drzwi. Możesz ruszyć się na dowolne zakryte pole.";
                    break;
                case NextAction.GameOverAndReplaceDragonCard:
                    info = "Niestety przegrałeś. Game over.<br />";
                    info += "Smok ucieka.";
                    info += "Wybierz zakryte pole, na które przeniesie się smok.";
                    break;
            }
            Information = info;
        }

        private void SetCurrentActionAlertType()
        {
            if (NextAction == NextAction.GameOver || NextAction == NextAction.GameOverAndReplaceDragonCard)
                AlertType = "danger";
            else if (NextAction == NextAction.GameWin)
                AlertType = "success";
            else
                AlertType = "primary";
        }
    }
}