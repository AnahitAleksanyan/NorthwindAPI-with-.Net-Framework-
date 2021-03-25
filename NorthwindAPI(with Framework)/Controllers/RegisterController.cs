
using Newtonsoft.Json;
using NorthwindAPI_with_Framework_.ContextService;
using NorthwindAPI_with_Framework_.DTO.Models;
using NorthwindAPI_with_Framework_.Models;
using NorthwindAPI_with_Framework_.Utils;
using NorthwindAPI_with_Framework_.Utils.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NorthwindAPI_with_Framework_.Controllers
{
    public class RegisterController : ApiController
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        /// <summary>
        ///     Registration with username, password and Customer id.
        /// </summary>
        /// <param name="userRegisterDTO">User model</param>
        /// <returns>
        ///    Returns the result message of the request.
        /// </returns>
        public HttpResponseMessage Post([FromBody] UserRegisterDTO userRegisterDTO)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            //check User have existing in database
            if (!(northwindContext.TbIuser.Any(u => u.Username.Equals(userRegisterDTO.Username)))
                && !(northwindContext.TbIuser.Any(u => u.CustomerId.Equals(userRegisterDTO.CustomerId))))
            {
                ValidatorResult validatorResult = UserValidator.IsValidUser(userRegisterDTO);
                if (!validatorResult.IsValid)
                {
                    response.StatusCode = HttpStatusCode.PreconditionFailed;
                    response.Content = new StringContent(JsonConvert.SerializeObject(validatorResult.ValidationMessage));
                    return response;
                }

                TbIuser user = new TbIuser();
                user.Username = userRegisterDTO.Username.Trim();
                var customer = northwindContext.Customers.Find(userRegisterDTO.CustomerId);
                if (customer == null) {
                    response.StatusCode = HttpStatusCode.PreconditionFailed;
                    response.Content = new StringContent(JsonConvert.SerializeObject("Wrong CustomerId."));
                    return response;
                }
                user.CustomerId = userRegisterDTO.CustomerId;
                user.Salt = Convert.ToBase64String(Common.GetRandomSalt(16));
                user.Password = Convert.ToBase64String(Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(userRegisterDTO.Password),
                    Convert.FromBase64String(user.Salt)));
                user.FullName = userRegisterDTO.FullName.Trim();

                //Add to DB
                try
                {
                    northwindContext.Add(user);
                    northwindContext.SaveChanges();
                    response.Content = new StringContent(JsonConvert.SerializeObject("Register successfully"));
                    return response;
                }
                catch (Exception e)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Content = new StringContent(JsonConvert.SerializeObject(e.Message));
                    return response;
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.PreconditionFailed;
                response.Content = new StringContent(JsonConvert.SerializeObject("User is existing in Database."));
                return response;
            }
        }
    }
}
