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
            .ForMember(dest => dest.Statement, map => map.MapFrom(src => src.Statement))
            .ForMember(dest => dest.XmlStatement, map => map.MapFrom(src => src.XmlStatement));
        }
    }
}