
using Newtonsoft.Json;
using NorthwindAPI_with_Framework_.ContextService;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthwindAPI_with_Framework_.Controllers
{
    public class OrderController : ApiController
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        /// <summary>
        ///     The getting is performed by OrderId.
        /// </summary>
        /// <param name="OrderId">Order id</param>
        /// <returns>
        ///     Returns Order json with Employee,Customer,OrderDetails with their Product
        ///     and Product with their Supplier.
        /// </returns>
        public HttpResponseMessage Get(int OrderId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var order = northwindContext.Orders.Where(o => o.OrderId == OrderId).Join(
                northwindContext.Employees,
                o => o.EmployeeId,
                e => e.EmployeeId,
                (o, e) => new
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    EmployeeId = o.EmployeeId,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    ShipVia = o.ShipVia,
                    Freight = o.Freight,
                    ShipName = o.ShipName,
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipRegion = o.ShipRegion,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
                    Employee = new
                    {
                        EmployeeId = e.EmployeeId,
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        Title = e.Title,
                        TitleOfCourtesy = e.TitleOfCourtesy,
                        BirthDate = e.BirthDate,
                        HireDate = e.HireDate,
                    },
                }
                ).Join(
                 northwindContext.Customers,
                 o => o.CustomerId,
                 c => c.CustomerId,
                 (o, c) => new
                 {
                     OrderId = o.OrderId,
                     CustomerId = o.CustomerId,
                     EmployeeId = o.EmployeeId,
                     OrderDate = o.OrderDate,
                     RequiredDate = o.RequiredDate,
                     ShippedDate = o.ShippedDate,
                     ShipVia = o.ShipVia,
                     Freight = o.Freight,
                     ShipName = o.ShipName,
                     ShipAddress = o.ShipAddress,
                     ShipCity = o.ShipCity,
                     ShipRegion = o.ShipRegion,
                     ShipPostalCode = o.ShipPostalCode,
                     ShipCountry = o.ShipCountry,
                     Employee = o.Employee,
                     Customer = new
                     {
                         CustomerId = c.CustomerId,
                         CompanyName = c.CompanyName,
                         ContactName = c.ContactName,
                         ContactTitle = c.ContactTitle,
                         Fax = c.Fax
                     },
                     OrderDetails = northwindContext.OrderDetails.Where(od => od.OrderId == OrderId).Join(
                         northwindContext.Products,
                         od => od.ProductId,
                         p => p.ProductId,
                         (od, p) => new
                         {
                             OrderId = od.OrderId,
                             ProductId = od.ProductId,
                             UnitPrice = od.UnitPrice,
                             Quantity = od.Quantity,
                             Discount = od.Discount,
                             Product = northwindContext.Products.Where(pr => pr.ProductId == p.ProductId).Join(
                                 northwindContext.Suppliers,
                                 prod=> prod.SupplierId,
                                 s => s.SupplierId,
                                 (prod, s) => new
                                 {
                                     ProductId = prod.ProductId,
                                     ProductName = prod.ProductName,
                                     SupplierId = prod.SupplierId,
                                     CategoryId = prod.CategoryId,
                                     QuantityPerUnit = prod.QuantityPerUnit,
                                     UnitPrice = prod.UnitPrice,
                                     UnitsInStock = prod.UnitsInStock,
                                     UnitsOnOrder = prod.UnitsOnOrder,
                                     ReorderLevel = prod.ReorderLevel,
                                     Discontinued = prod.Discontinued,
                                     Supplier = s
                                 }
                                 ).FirstOrDefault()

                         }
                         ).ToList()
                 }
                ).ToList().FirstOrDefault();


            if (order == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(JsonConvert.SerializeObject("Order is not found."));
                return response;
            }
            response.Content = new StringContent(JsonConvert.SerializeObject(order));
            return response;
        }
    }
}

