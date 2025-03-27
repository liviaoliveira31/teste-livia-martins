using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Services.StatementPrinter.Response
{
    public class ResponseType
    {
        public ResponseType(string text, XDocument xmlText)
        {
            Text = text;
            XmlText = xmlText;
        }

        public string Text { get; set; }
        public XDocument XmlText { get; set; }
    }
}
