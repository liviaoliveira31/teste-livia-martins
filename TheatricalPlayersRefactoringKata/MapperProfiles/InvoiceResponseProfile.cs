using AutoMapper;
using TheatricalPlayersRefactoringKata.Commands.PrintInvoice;
using TheatricalPlayersRefactoringKata.Services.StatementPrinter.Response;

namespace TheatricalPlayersRefactoringKata.MapperProfiles
{
    public class InvoiceResponseProfile : Profile
    {
        public InvoiceResponseProfile()
        {
            CreateMap<ResponseType, PrintInvoiceCommandResponse>(MemberList.None)
            .ForMember(dest => dest.Text, map => map.MapFrom(src => src.Text))
            .ForMember(dest => dest.XmlText, map => map.MapFrom(src => src.XmlText));
        }
    }
}