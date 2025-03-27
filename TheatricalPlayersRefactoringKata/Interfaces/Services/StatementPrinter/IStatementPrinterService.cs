using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.StatementPrinter.Response;

namespace TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter
{
    public interface IStatementPrinterService
    {
        public ResponseType Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
