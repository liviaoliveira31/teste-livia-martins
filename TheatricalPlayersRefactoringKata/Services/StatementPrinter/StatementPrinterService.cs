using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.StatementPrinter.Response;

namespace TheatricalPlayersRefactoringKata.Services.StatementPrinter
{
    public class StatementPrinterService : IStatementPrinterService
    {
        public ResponseType Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var volumeCredits = 0;
            var totalAmount = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            XElement statement = GetXmlHeader(invoice.Customer);

            var items = new XElement("Items");

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];

                var lines = play.Lines; 
                lines = ValidatePlayLines(play.Lines); 

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

                volumeCredits = AddVolumeCredits(play.Type, performance.Audience, volumeCredits);

                result += AddOrderLine(play.Name, baseValue, performance.Audience, cultureInfo);

                items.Add(GenerateXML(invoice.Customer, play, baseValue, AddVolumeCredits(play.Type, performance.Audience, 0), performance.Audience));

                totalAmount += baseValue;
            }            

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount/100));
            result += String.Format("You earned {0} credits\n", volumeCredits);

            var xml = SaveXmlStatement(result, statement, items, totalAmount, volumeCredits);
            SaveTextStatement(result);

            return new ResponseType(result, xml);
        }

        private XElement GetXmlHeader(string customer)
        {
            XElement statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", customer)

            );

            return statement;
        }

        public int ValidatePlayLines(int playLines)
        {
            if (playLines > 4000)
                return playLines = 4000;

            if (playLines < 1000)
                return playLines = 1000;

            return playLines;
        }

        public int GetAmountByPlayTypeTragedy(int performanceAudience, int baseValue)
        {
            if (performanceAudience <= 30)
            {
                return baseValue; 
            }

            if (performanceAudience > 30)
            {                
                return baseValue += 1000 * (performanceAudience - 30); 
            }

            return baseValue;
        }

        public int GetAmountByPlayTypeComedy(int performanceAudience, int baseValue)
        {
            if (performanceAudience > 20)
                return baseValue += 10000 + 500 * (performanceAudience - 20); 

            return baseValue += 300 * performanceAudience; 
        }

        public int GetAmountByPlayTypeHistory(int performanceAudience, int baseValue)
        {
            var baseValueTragedy = GetAmountByPlayTypeTragedy(performanceAudience, baseValue);
            var baseValueComedy = GetAmountByPlayTypeComedy(performanceAudience, baseValue);

            return baseValueTragedy + baseValueComedy;
        }

        public int AddVolumeCredits(
            string playType, 
            int performanceAudience, 
            int volumeCredits 
        )
        {
            if (performanceAudience > 30)
            {
                volumeCredits += (performanceAudience - 30);
            }

            if (playType == "comedy") 
                volumeCredits += (int)Math.Floor((decimal)performanceAudience / 5);

            return volumeCredits;
        }

        private string AddOrderLine(string playName, int baseValue, int performanceAudience, CultureInfo cultureInfo)
        {
            return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, Convert.ToDecimal(baseValue / 100), performanceAudience);
        }

        private XElement GenerateXML(string customer, Play play, int baseValue, int volumeCredits, int audience)
        {
            return new XElement(
            new XElement("Item",
            new XElement("AmountOwed", baseValue / 100),
            new XElement("EarnedCredits", volumeCredits),
            new XElement("Seats", audience)));
        }

        private XDocument SaveXmlStatement(string result, XElement statement, XElement items, int totalAmount, int volumeCredits)
        {
            statement.Add(items);
            statement.Add(new XElement("AmountOwed", Convert.ToDecimal(totalAmount / 100)));
            statement.Add(new XElement("EarnedCredits", volumeCredits));
            var xml = new XDocument(statement);
            xml.Save("C:/Dev/teste-livia-martins/TheatricalPlayersRefactoringKata/Services/StatementPrinter/Response/XML/result.xml");
            
            return xml;
        }

        private void SaveTextStatement(string result)
        {
            StreamWriter streamWriter = new StreamWriter("C:/Dev/teste-livia-martins/TheatricalPlayersRefactoringKata/Services/StatementPrinter/Response/Text/result.txt");
            streamWriter.Write(result);
            streamWriter.Close();
        }
    }
}
