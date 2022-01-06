using AutoMapper;

using ProductShop.Models;
using ProductShop.DataTransferObjects;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserInputModel, User>();

            this.CreateMap<ProductInputModel, Product>();

            this.CreateMap<CategoryInputModel, Category>();

            this.CreateMap<CategoryProductInputModel, CategoryProduct>();

            this.CreateMap<Product, UsersSoldProductsDto>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(x => x.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(x => x.Buyer.LastName));

            this.CreateMap<User, UsersWithSoldProductsDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x.ProductsSold.Where(p => p.Buyer != null)));

            this.CreateMap<Category, CategoriesByProductsCountDto>()
                .ForMember(x => x.ProductCount, y => y.MapFrom(x => x.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(x => x.CategoryProducts.Average(p => p.Product.Price).ToString("F2")))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(x => x.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F2")));
        }
    }
}
