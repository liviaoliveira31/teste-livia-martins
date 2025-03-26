using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter
{
    public interface IStatementPrinterService
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
