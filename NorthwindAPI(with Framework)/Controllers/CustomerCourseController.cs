
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NorthwindAPI_with_Framework_.ContextService;
using NorthwindAPI_with_Framework_.DTO.Models;
using NorthwindAPI_with_Framework_.Models;
using NorthwindAPI_with_Framework_.Utils.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NorthwindAPI_with_Framework_.Controllers
{
    public class CustomerCourseController : ApiController
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;


        /// <summary>
        ///     Course cutomer pair creating
        /// </summary>
        /// <param name="courseCustomerCastDTO">Course customer model</param>
        /// <returns>
        ///    Returns the result message of the request.
        /// </returns>
        public HttpResponseMessage Post([FromBody] CourseCustomerCastDTO courseCustomerCastDTO)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            ValidatorResult validatorResult = CourseCustomerValidator.IsValidCourseCustomer(courseCustomerCastDTO);
            if (!validatorResult.IsValid)
            {
                response.StatusCode = HttpStatusCode.PreconditionFailed;
                response.Content = new StringContent(JsonConvert.SerializeObject(validatorResult.ValidationMessage));
                return response;
            }

            var customer = northwindContext.Customers.Find(courseCustomerCastDTO.CustomerId);
            var course = northwindContext.Courses.Find(courseCustomerCastDTO.CourseId);
            if (customer == null || course == null)
            {
                response.StatusCode = HttpStatusCode.PreconditionFailed;
                response.Content = new StringContent(JsonConvert.SerializeObject("There is no course or customer with that id."));
                return response;
            }
            var courseCustomer = northwindContext.CourseCustomerCasts.Where(cc => cc.CourseId == courseCustomerCastDTO.CourseId && cc.CustomerId == courseCustomerCastDTO.CustomerId).FirstOrDefault();

            if (courseCustomer != null)
            {
                response.StatusCode = HttpStatusCode.PreconditionFailed;
                response.Content = new StringContent(JsonConvert.SerializeObject("This course customer pair is already exist."));
                return response;
            }

            CourseCustomerCast courseCustomerCast = new CourseCustomerCast()
            {
                CourseId = courseCustomerCastDTO.CourseId,
                CustomerId = courseCustomerCastDTO.CustomerId
            };

            //Add to DB
            try
            {
                northwindContext.Add(courseCustomerCast);
                northwindContext.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject("The courseCustomer is successfully saved."));
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(JsonConvert.SerializeObject(e.Message));
                return response;
            }

        }

        /// <summary>
        ///     Course cutomer pair getting
        /// </summary>
        /// <param name="CustomerId">Customer id</param>
        /// <returns>
        ///      Returns Course cutomer pair json.
        /// </returns>
        public HttpResponseMessage Get(string CustomerId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var customerCourses = northwindContext.CourseCustomerCasts.Where(cc => cc.CustomerId == CustomerId).ToList();
            var courses = new LinkedList<Course>();
            foreach (var item in customerCourses)
            {
                var course = northwindContext.Courses.Where(c => c.CourseId == item.CourseId)
                                                     .FirstOrDefault();
                if (course != null)
                {
                    courses.AddLast(course);
                }
            }
            response.Content = new StringContent(JsonConvert.SerializeObject(courses));
            return response;
        }
    }
}

