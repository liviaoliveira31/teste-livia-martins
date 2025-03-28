using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Services.StatementPrinter.Response
{
    public class ResponseType
    {
        public ResponseType(string statement, XDocument xmlStatement)
        {
            Statement = statement;
            XmlStatement = xmlStatement;
        }

        public string Statement { get; set; }
        public XDocument XmlStatement { get; set; }
    }
}
