using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommandHandler : IRequestHandler<PrintInvoiceCommand, PrintInvoiceCommandResponse>
    {
        public async Task<PrintInvoiceCommandResponse> Handle(PrintInvoiceCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult( new PrintInvoiceCommandResponse());
        }
    }
}
