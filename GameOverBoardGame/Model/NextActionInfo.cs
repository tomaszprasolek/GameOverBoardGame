using GameOverBoardGame.Enums;
using Microsoft.AspNetCore.Components;

namespace GameOverBoardGame.Model
{
    public class NextActionInfo
    {
        public NextAction NextAction { get; }
        public MarkupString Information { get; private set; }
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
                    info = "Twój ruch. Wybierz kartę.";
                    break;
                case NextAction.GameOver:
                    info = "Niestety przegrałeś. Game over";
                    break;
                case NextAction.GameWin:
                    info = "Brawo wygrałeś!!!";
                    break;
                case NextAction.Teleport:
                    info = "Znalazłeś drzwi.<br />Możesz ruszyć się na dowolne zakryte pole.";
                    break;
                case NextAction.GameOverAndReplaceDragonCard:
                    info = "Niestety przegrałeś. Game over.<br />";
                    info += "Smok ucieka.<br />";
                    info += "Wybierz zakryte pole, na które przeniesie się smok.";
                    break;
            }
            Information = (MarkupString)info;
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