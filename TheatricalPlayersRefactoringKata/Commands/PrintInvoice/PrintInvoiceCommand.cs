using MediatR;

namespace TheatricalPlayersRefactoringKata.Commands.PrintInvoice
{
    public class PrintInvoiceCommand : IRequest<PrintInvoiceCommandResponse>
    {
    }
}
