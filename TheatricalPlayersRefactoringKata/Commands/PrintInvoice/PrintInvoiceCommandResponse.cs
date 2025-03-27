using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommandResponse
    {
        public string Text { get; set; }
        public XDocument XmlText { get; set; }
    }
}
