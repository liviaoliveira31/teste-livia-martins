using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces.Services.StatementPrinter;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommandHandler : IRequestHandler<PrintInvoiceCommand, PrintInvoiceCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStatementPrinterService _statementPrinterService;

        public PrintInvoiceCommandHandler(
            IMapper mapper,
            IStatementPrinterService statementPrinterService
        )
        {
            _mapper = mapper;
            _statementPrinterService = statementPrinterService;
        }
        
        public async Task<PrintInvoiceCommandResponse> Handle(PrintInvoiceCommand request, CancellationToken cancellationToken)
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
                new Performance("richard-iii", 20)
                }
            );

            var serviceResponse = _statementPrinterService.Print(invoice, plays);

            return await Task.FromResult(_mapper.Map<PrintInvoiceCommandResponse>(serviceResponse));            
        }
    }
}
