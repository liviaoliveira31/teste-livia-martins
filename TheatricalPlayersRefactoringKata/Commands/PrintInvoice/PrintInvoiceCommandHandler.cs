using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommandHandler : IRequestHandler<PrintInvoiceCommand, string>
    {
        private readonly IStatementPrinterService _statementPrinterService;

        public PrintInvoiceCommandHandler(
            IStatementPrinterService statementPrinterService 
        ) =>
            _statementPrinterService = statementPrinterService;
        
        public async Task<string> Handle(PrintInvoiceCommand request, CancellationToken cancellationToken)
        {

            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
            plays.Add("othello", new Play("Othello", 3560, "tragedy"));
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));
            plays.Add("john", new Play("King John", 2648, "history"));
            plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
                }
            );

            return await Task.FromResult(_statementPrinterService.Print(invoice , plays));
            
        }
    }
}
