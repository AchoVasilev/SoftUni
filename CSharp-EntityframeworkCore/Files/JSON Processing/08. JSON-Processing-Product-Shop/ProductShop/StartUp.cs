using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

using ProductShop.Data;
using ProductShop.Models;
using ProductShop.DataTransferObjects;

namespace ProductShop
{
    public class StartUp
    {
        private static IMapper mapper;
        static void Main(string[] args)
        {
            ProductShopContext dbContext = new ProductShopContext();

            //ResetDatabase(dbContext);

            //string usersJson = File.ReadAllText("../../../Datasets/users.json");
            //string productsJson = File.ReadAllText("../../../Datasets/products.json");
            //string categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            //string categoriesProductsJson = File.ReadAllText("../../../Datasets/categories-products.json");

            //ImportUsers(dbContext, usersJson);
            //ImportCategories(dbContext, categoriesJson);
            //ImportProducts(dbContext, productsJson);
            //MapperInitialize();

            string result = GetUsersWithProducts(dbContext);

            File.WriteAllText("../../../Datasets/users-and-products.json", result);

            Console.WriteLine(result);
        }

        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            InitializeAutomapper();
            IEnumerable<UserInputModel> dtoUsers = JsonConvert.DeserializeObject<IEnumerable<UserInputModel>>(inputJson);

            IEnumerable<User> users = mapper.Map<IEnumerable<User>>(dtoUsers);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        //Problem 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutomapper();
            IEnumerable<ProductInputModel> dtoProducts = JsonConvert.DeserializeObject<IEnumerable<ProductInputModel>>(inputJson);

            IEnumerable<Product> products = mapper.Map<IEnumerable<Product>>(dtoProducts);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        //Problem 3
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            InitializeAutomapper();

            CategoryInputModel[] dtoCategories = JsonConvert.DeserializeObject<CategoryInputModel[]>(inputJson);
            Category[] categories = mapper.Map<Category[]>(dtoCategories)
                                          .Where(c => c.Name != null)
                                          .ToArray();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        //Problem 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            InitializeAutomapper();

            CategoryProductInputModel[] dtoCategoriesProducts = JsonConvert.DeserializeObject<CategoryProductInputModel[]>(inputJson);
            CategoryProduct[] categoriesProducts = mapper.Map<CategoryProduct[]>(dtoCategoriesProducts);

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }

        //Problem 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + ' ' + p.Seller.LastName
                })
                .OrderBy(x => x.price)
                .ToArray();

            string productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            return productsJson;
        }

        //Problem 6
        public static string GetSoldProducts(ProductShopContext context)
        {
            InitializeAutomapper();

            //var users = context.Users
            //    .Where(x => x.ProductsSold.Any(y => y.Buyer != null))
            //    .OrderBy(x => x.LastName)
            //    .ThenBy(x => x.FirstName)
            //    .Select(x => new
            //    {
            //        firstName = x.FirstName,
            //        lastName = x.LastName,
            //        soldProducts = x.ProductsSold
            //                        .Where(y => y.Buyer != null)
            //                        .Select(y => new
            //                        {
            //                            name = y.Name,
            //                            price = y.Price,
            //                            buyerFirstName = y.Buyer.FirstName,
            //                            buyerLastName = y.Buyer.LastName
            //                        })
            //                        .ToArray()
            //    })
            //    .ToArray();

            UsersWithSoldProductsDto[] users = context.Users
                                                      .Where(x => x.ProductsSold.Any(y => y.Buyer != null))
                                                      .OrderBy(x => x.LastName)
                                                      .ThenBy(x => x.FirstName)
                                                      .ProjectTo<UsersWithSoldProductsDto>()
                                                      .ToArray();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        //Problem 7
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            //var categories = context.Categories
            //    .Select(x => new
            //    {
            //        category = x.Name,
            //        productsCount = x.CategoryProducts.Count(),
            //        averagePrice = x.CategoryProducts.Average(cp => cp.Product.Price).ToString("F2"),
            //        totalRevenue = x.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F2")
            //    })
            //    .OrderByDescending(x => x.productsCount)
            //    .ToArray();

            CategoriesByProductsCountDto[] categories = context.Categories
                                                                .ProjectTo<CategoriesByProductsCountDto>()
                                                                .OrderByDescending(x => x.ProductCount)
                                                                .ToArray();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        //Problem 8
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(x => x.ProductsSold)
                .ToList()
                .Where(x => x.ProductsSold.Any(y => y.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    age = x.Age,
                    soldProducts = new
                    {
                        count = x.ProductsSold
                                  .Count(p => p.Buyer != null),
                        products = x.ProductsSold
                                    .Where(p => p.Buyer != null)
                                    .Select(ps => new
                                    {
                                        name = ps.Name,
                                        price = ps.Price
                                    })
                                    .ToArray()
                    }
                })
                .OrderByDescending(x => x.soldProducts.count)
                .ToArray();

            var resultObj = new
            {
                usersCount = users.Length,
                users = users
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            string result = JsonConvert.SerializeObject(resultObj, settings);

            return result;
        }
        
        private static void MapperInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
        }
        private static void InitializeAutomapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            mapper = config.CreateMapper();
        }

        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.EnsureCreated();
        }
    }
}
