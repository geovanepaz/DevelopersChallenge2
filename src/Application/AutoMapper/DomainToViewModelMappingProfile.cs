using Application.ViewModels.Transaction;
using AutoMapper;
using Domain;

namespace Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
