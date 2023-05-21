using AutoMapper;
using Dev.App.ViewModels;
using Dev.Business.Models;

namespace Dev.App.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
