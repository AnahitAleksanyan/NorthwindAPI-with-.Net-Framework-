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
    public class CourseController : ApiController
    {

        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        /// <summary>
        ///     Course creating
        /// </summary>
        /// <param name="courseDTO">Course model</param>
        /// <returns>
        ///    Returns the result message of the request.
        /// </returns>
        public HttpResponseMessage Post([FromBody] CourseDTO courseDTO)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            ValidatorResult validatorResult = CourseValidator.IsValidCourse(courseDTO);
            if (!validatorResult.IsValid)
            {
                response.StatusCode = HttpStatusCode.PreconditionFailed;
                response.Content = new StringContent(JsonConvert.SerializeObject(validatorResult.ValidationMessage));
                return response;
            }

            Course course = new Course()
            {
                Name = courseDTO.Name,
                Description = courseDTO.Description,
                Length = courseDTO.Length,
                StartDate = courseDTO.StartDate,
                EndDate = courseDTO.EndDate
            };

            //Add to DB
            try
            {
                northwindContext.Add(course);
                northwindContext.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject("The course is successfully saved."));
                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Content = new StringContent(JsonConvert.SerializeObject(e.Message));
                return response;
            }

        }
    }
}
