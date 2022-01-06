using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

using AutoMapper;

using CarDealer.Data;
using CarDealer.Models;
using CarDealer.DataTransferObjects.Import;
using CarDealer.DataTransferObjects.Export;
using System.Xml;
using System.Text;

namespace CarDealer
{
    public class StartUp
    {
        static IMapper mapper;
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();

            //ResetDatabase(context);

            //string suppliersInput = File.ReadAllText("../../../Datasets/suppliers.xml");
            ////string result = ImportSuppliers(context, suppliersInput);
            //ImportSuppliers(context, suppliersInput);

            //string partsInput = File.ReadAllText("../../../Datasets/parts.xml");
            ////string result = ImportParts(context, partsInput);
            //ImportParts(context, partsInput);

            //string carsInput = File.ReadAllText("../../../Datasets/cars.xml");
            ////string result = ImportCars(context, carsInput);
            //ImportCars(context, carsInput);

            //string customersInput = File.ReadAllText("../../../Datasets/customers.xml");
            ////string result = ImportCustomers(context, customersInput);
            //ImportCustomers(context, customersInput);

            //string salesInput = File.ReadAllText("../../../Datasets/sales.xml");
            //string result = ImportSales(context, salesInput);

            //Console.WriteLine(result);

            //string result = GetCarsWithDistance(context);
            //File.WriteAllText("../../../Datasets/Export/cars.xml", result);

            //string result = GetCarsFromMakeBmw(context);
            //File.WriteAllText("../../../Datasets/Export/bmw-cars.xml", result);

            //string result = GetLocalSuppliers(context);
            //File.WriteAllText("../../../Datasets/Export/local-suopliers.xml", result);

            //string result = GetCarsWithTheirListOfParts(context);
            //File.WriteAllText("../../../Datasets/Export/cars-and-parts.xml", result);

            string result = GetTotalSalesByCustomer(context);
            File.WriteAllText("../../../Datasets/Export/customers-total-sales.xml", result);

            //string result = GetSalesWithAppliedDiscount(context);
            //File.WriteAllText("../../../Datasets/Export/sales-discounts.xml", result);

            Console.WriteLine(result);
        }

        //Problem 09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            InitializeAutomapper();

            string rootElement = "Suppliers";
            XmlSerializer serializer = new XmlSerializer(typeof(ImportSuppliersDto[]), new XmlRootAttribute(rootElement));
            TextReader reader = new StringReader(inputXml);

            ImportSuppliersDto[] suppliersDto = serializer.Deserialize(reader) as ImportSuppliersDto[];
            Supplier[] suppliers = mapper.Map<Supplier[]>(suppliersDto);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        //Problem 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            string root = "Parts";
            XmlSerializer serializer = new XmlSerializer(typeof(ImportPartsDto[]), new XmlRootAttribute(root));
            TextReader reader = new StringReader(inputXml);

            ImportPartsDto[] partsDto = serializer.Deserialize(reader) as ImportPartsDto[];

            Part[] parts = partsDto.Where(x => context.Suppliers.Any(y => y.Id == x.SupplierId))
                                    .Select(x => new Part
                                    {
                                        Name = x.Name,
                                        Price = x.Price,
                                        Quantity = x.Quantity,
                                        SupplierId = x.SupplierId
                                    })
                                    .ToArray();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        //Problem 11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            string root = "Cars";

            XmlSerializer serializer = new XmlSerializer(typeof(ImportCarsDto[]), new XmlRootAttribute(root));
            TextReader reader = new StringReader(inputXml);

            ImportCarsDto[] carsDto = serializer.Deserialize(reader) as ImportCarsDto[];
            List<Car> cars = new List<Car>();
            List<int> allParts = context.Parts
                                         .Select(x => x.Id)
                                         .ToList();

            foreach (var currentCar in carsDto)
            {
                IEnumerable<int> distinctedParts = currentCar.Parts
                                                             .Select(x => x.PartId)
                                                             .Distinct();
                IEnumerable<int> parts = distinctedParts.Intersect(allParts);

                Car car = new Car
                {
                    Make = currentCar.Make,
                    Model = currentCar.Model,
                    TravelledDistance = currentCar.TravelledDistance
                };

                foreach (var part in parts)
                {
                    PartCar partCar = new PartCar
                    {
                        PartId = part
                    };

                    car.PartCars.Add(partCar);
                }

                cars.Add(car);
            }

            //List<int> allParts = context.Parts.Select(x => x.Id).Distinct().ToList();
            //List<Car> cars = carsDto
            //    .Select(x => new Car
            //    {
            //        Make = x.Make,
            //        Model = x.Model,
            //        TravelledDistance = x.TravelledDistance,
            //        PartCars = x.Parts.Select(x => x.PartId)
            //                      .Distinct()
            //                      .Intersect(allParts)
            //                      .Select(y => new PartCar
            //                      {
            //                          PartId = y
            //                      })
            //                      .ToList()
            //    })
            //    .ToList();


            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        //Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            InitializeAutomapper();

            string root = "Customers";
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCustomersDto[]), new XmlRootAttribute(root));
            TextReader reader = new StringReader(inputXml);
            ImportCustomersDto[] customersDto = serializer.Deserialize(reader) as ImportCustomersDto[];

            Customer[] customers = mapper.Map<Customer[]>(customersDto);
            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        //Problem 13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            InitializeAutomapper();

            string root = "Sales";
            XmlSerializer serializer = new XmlSerializer(typeof(ImportSalesDto[]), new XmlRootAttribute(root));
            TextReader reader = new StringReader(inputXml);

            ImportSalesDto[] salesDto = serializer.Deserialize(reader) as ImportSalesDto[];

            List<int> carsId = context.Cars.Select(x => x.Id).ToList();
            Sale[] sales = salesDto
                                    .Where(x => carsId.Contains(x.CarId))
                                    .Select(x => new Sale
                                    {
                                        CarId = x.CarId,
                                        CustomerId = x.CustomerId,
                                        Discount = x.Discount
                                    })
                                    .ToArray();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        //Problem 14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            string root = "cars";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarsDistanceDto[]), new XmlRootAttribute(root));

            ExportCarsDistanceDto[] carsDto = context.Cars
                                                     .Where(x => x.TravelledDistance >= 2_000_000)
                                                     .Select(x => new ExportCarsDistanceDto
                                                     {
                                                         Make = x.Make,
                                                         Model = x.Model,
                                                         TravelledDistance = x.TravelledDistance
                                                     })
                                                     .OrderBy(x => x.Make)
                                                     .ThenBy(x => x.Model)
                                                     .Take(10)
                                                     .ToArray();
            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(writer, carsDto, namespaces);

            return writer.ToString();
        }

        //Problem 15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            string root = "cars";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportBmwCarsDto[]), new XmlRootAttribute(root));

            ExportBmwCarsDto[] bmwCarsDto = context.Cars
                                                   .Where(x => x.Make == "BMW")
                                                   .Select(x => new ExportBmwCarsDto
                                                   {
                                                       Id = x.Id,
                                                       Model = x.Model,
                                                       TravelledDistance = x.TravelledDistance
                                                   })
                                                   .OrderBy(x => x.Model)
                                                   .ThenByDescending(x => x.TravelledDistance)
                                                   .ToArray();

            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            serializer.Serialize(writer, bmwCarsDto, namespaces);

            return writer.ToString();
        }

        //Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            string root = "suppliers";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportLocalSuppliersDto[]), new XmlRootAttribute(root));

            ExportLocalSuppliersDto[] localSuppliersDto = context.Suppliers
                                                                 .Where(x => x.IsImporter == false)
                                                                 .Select(x => new ExportLocalSuppliersDto
                                                                 {
                                                                     Name = x.Name,
                                                                     SupplierId = x.Id,
                                                                     PartsCount = x.Parts.Count()
                                                                 })
                                                                 .ToArray();
            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, localSuppliersDto, namespaces);

            return writer.ToString();
        }

        //Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            string root = "cars";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarsPartsDto[]), new XmlRootAttribute(root));

            ExportCarsPartsDto[] carsPartsDto = context.Cars
                                                       .Select(x => new ExportCarsPartsDto
                                                       {
                                                           Make = x.Make,
                                                           Model = x.Model,
                                                           TravelledDistance = x.TravelledDistance,
                                                           PartsExport = x.PartCars
                                                                          .Select(y => new PartsDto
                                                                          {
                                                                              Name = y.Part.Name,
                                                                              Price = y.Part.Price
                                                                          })
                                                                          .OrderByDescending(x => x.Price)
                                                                          .ToArray()
                                                       })
                                                       .OrderByDescending(x => x.TravelledDistance)
                                                       .ThenBy(x => x.Model)
                                                       .Take(5)
                                                       .ToArray();

            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, carsPartsDto, namespaces);

            return writer.ToString();
        }

        //Problem 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            string root = "customers";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCustomerDto[]), new XmlRootAttribute(root));

            ExportCustomerDto[] customersDto = context.Customers
                .Where(x => x.Sales.Any())
                .Select(x => new ExportCustomerDto
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales
                                        .Select(y => y.Car)
                                        .SelectMany(y => y.PartCars)
                                        .Sum(z => z.Part.Price)
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, customersDto, namespaces);

            return writer.ToString();
        }

        //Problem 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            string root = "sales";
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCarSaleDto[]), new XmlRootAttribute(root));

            ExportCarSaleDto[] carSaleDto = context.Sales
                .Select(x => new ExportCarSaleDto
                {
                    Car = new CarDto
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },

                    Discount = x.Discount,
                    CustomerName = x.Customer.Name,
                    Price = x.Car.PartCars.Sum(x => x.Part.Price),
                    PriceWithDiscount = x.Car.PartCars.Sum(x => x.Part.Price) - x.Car.PartCars.Sum(x => x.Part.Price) * x.Discount / 100
                })
                .ToArray();

            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, carSaleDto, namespaces);

            return writer.ToString();
        }

        private static void InitializeAutomapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            mapper = configuration.CreateMapper();
        }
        private static void ResetDatabase(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}