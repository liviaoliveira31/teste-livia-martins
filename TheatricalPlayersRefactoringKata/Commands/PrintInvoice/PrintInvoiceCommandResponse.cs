using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommandResponse
    {
        public string Statement { get; set; }
        public XDocument XmlStatement { get; set; }
    }
}
