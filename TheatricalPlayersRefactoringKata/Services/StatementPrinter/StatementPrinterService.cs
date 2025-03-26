using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.StatementPrinter
{
    public class StatementPrinterService : IStatementPrinterService
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];

                var lines = play.Lines; 
                ValidatePlayLines(play.Lines); 

                var baseValue = lines * 10;

                switch (play.Type)
                {
                    case "tragedy":
                            baseValue = GetAmountByPlayTypeTragedy(performance.Audience, baseValue);                        
                        break;

                    case "comedy":
                        baseValue = GetAmountByPlayTypeComedy(performance.Audience, baseValue);
                        break;

                    case "history":
                        baseValue = GetAmountByPlayTypeHistory(performance.Audience, baseValue);
                        break;

                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(performance.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);

                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(baseValue / 100), performance.Audience);
                totalAmount += baseValue;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private int GetAmountByPlayTypeTragedy(int performanceAudience, int baseValue)
        {
            if (performanceAudience > 30)
                return baseValue += 1000 * (performanceAudience - 30);

            return baseValue;
        }

        private int GetAmountByPlayTypeComedy(int performanceAudience, int baseValue)
        {
            if (performanceAudience > 20)
                return baseValue += 10000 + 500 * (performanceAudience - 20);
            
            return baseValue += 300 * performanceAudience;
        }

        private int GetAmountByPlayTypeHistory(int performanceAudience, int baseValue)
        {
            var baseValueTragedy = GetAmountByPlayTypeTragedy(performanceAudience, baseValue);
            var baseValueComedy = GetAmountByPlayTypeComedy(performanceAudience, baseValue);

            return baseValueTragedy + baseValueComedy;
        }

        private void ValidatePlayLines(int playLines)
        {
            if (playLines > 4000 || playLines < 1000)
                throw new Exception("the number of lines must be between 1000 and 4000 ");
        }
    }
}
