using AutoMapper;

using CarDealer.Models;
using CarDealer.DataTransferObjects.Import;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSuppliersDto, Supplier>();

            this.CreateMap<ImportCustomersDto, Customer>();
        }
    }
}
