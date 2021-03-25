
using Newtonsoft.Json;
using NorthwindAPI_with_Framework_.ContextService;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthwindAPI_with_Framework_.Controllers
{
    public class SearchOrderController : ApiController
    {

        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        /// <summary>
        ///     The search is performed by CustomerId,EmployeeId.
        ///</summary>
        /// <param name="CustomerId">Customer id</param>
        /// <param name="EmployeeId">Employee id</param>
        ///<returns>
        ///     Returns Orders json with Employees and Customers,
        ///     that EmloyeeId is equal to the EmployeeId of the parameters
        ///     and that CustomerId is equal to the CustomerId of the parameters.
        ///</returns>
        [HttpGet]
        [Route("SearchOrder/{CustomerId}/{EmployeeId}")]
        public HttpResponseMessage Get(string CustomerId, int EmployeeId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var order = northwindContext.Orders.Where(o => o.CustomerId == CustomerId && o.EmployeeId == EmployeeId).Join(
                    northwindContext.Employees,
                    o => o.EmployeeId,
                    e => e.EmployeeId,
                    (o, e) => new
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        CustomerId = o.CustomerId,
                        ShipName = o.ShipName,
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
                     OrderDate = o.OrderDate,
                     Employee = o.Employee,
                     ShipName = o.ShipName,
                     Customer = new
                     {
                         CustomerId = c.CustomerId,
                         CompanyName = c.CompanyName,
                         ContactName = c.ContactName,
                         ContactTitle = c.ContactTitle,
                         Fax = c.Fax
                     }
                 }
                );
            if (order.FirstOrDefault() == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(JsonConvert.SerializeObject("Order is not found."));
                return response;
            }

            response.Content = new StringContent(JsonConvert.SerializeObject(order));
            return response;
        }

        /// <summary> 
        /// The search is performed by ShipName,CustomerId,EmployeeId.
        /// </summary>
        ///    <param name="CustomerId">Customer id</param>
        ///    <param name="EmployeeId">Employee id</param>
        ///    <param name="ShipName">Ship name</param>
        /// <returns>
        ///     Returns Orders json with Employees and Customers,
        ///     that contains a ShipName in their ShipNames and that EmloyeeId is equal to the EmployeeId of the parameters
        ///     and that CustomerId is equal to the CustomerId of the parameters.
        /// </returns>
        [HttpGet]
        [Route("SearchOrder/{ShipName}/{CustomerId}/{EmployeeId}")]
        public HttpResponseMessage Get(string ShipName, string CustomerId, int EmployeeId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var order = northwindContext.Orders.Where(o => o.CustomerId == CustomerId &&
                                                      o.EmployeeId == EmployeeId &&
                                                      o.ShipName.Contains(ShipName))
                                               .Join(
                                                    northwindContext.Employees,
                                                    o => o.EmployeeId,
                                                    e => e.EmployeeId,
                                                    (o, e) => new
                                                    {
                                                        OrderId = o.OrderId,
                                                        OrderDate = o.OrderDate,
                                                        ShipName = o.ShipName,
                                                        CustomerId = o.CustomerId,
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
                                                     OrderDate = o.OrderDate,
                                                     ShipName = o.ShipName,
                                                     Employee = o.Employee,
                                                     Customer = new
                                                     {
                                                         CustomerId = c.CustomerId,
                                                         CompanyName = c.CompanyName,
                                                         ContactName = c.ContactName,
                                                         ContactTitle = c.ContactTitle,
                                                         Fax = c.Fax
                                                     }
                                                 }
                                                );
            if (order.FirstOrDefault() == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(JsonConvert.SerializeObject("Order is not found."));
                return response;
            }
            response.Content = new StringContent(JsonConvert.SerializeObject(order));
            return response;
        }

        /// <summary>
        ///      The search is performed by ShipName.
        /// </summary>
        ///<param name="ShipName">Ship name</param>
        ///<returns>
        ///      Returns Orders json with Employees and Customers,
        ///      that contains a ShipName in their ShipNames  
        ///</returns>
        [HttpGet]
        [Route("SearchOrder/{ShipName}")]
        public HttpResponseMessage Get(string ShipName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var order = northwindContext.Orders.Where(o => o.ShipName.Contains(ShipName))
                                               .Join(
                                                    northwindContext.Employees,
                                                    o => o.EmployeeId,
                                                    e => e.EmployeeId,
                                                    (o, e) => new
                                                    {
                                                        OrderId = o.OrderId,
                                                        OrderDate = o.OrderDate,
                                                        ShipName = o.ShipName,
                                                        CustomerId = o.CustomerId,
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
                                                     OrderDate = o.OrderDate,
                                                     ShipName = o.ShipName,
                                                     Employee = o.Employee,
                                                     Customer = new
                                                     {
                                                         CustomerId = c.CustomerId,
                                                         CompanyName = c.CompanyName,
                                                         ContactName = c.ContactName,
                                                         ContactTitle = c.ContactTitle,
                                                         Fax = c.Fax
                                                     }
                                                 }
                                                );
            if (order.FirstOrDefault() == null)
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
