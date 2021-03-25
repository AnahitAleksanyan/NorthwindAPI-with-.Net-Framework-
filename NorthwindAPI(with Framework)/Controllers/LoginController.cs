
using Newtonsoft.Json;
using NorthwindAPI_with_Framework_.ContextService;
using NorthwindAPI_with_Framework_.DTO.Models;
using NorthwindAPI_with_Framework_.Models;
using NorthwindAPI_with_Framework_.Utils;
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
    public class LoginController : ApiController
    {

        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;


        /// <summary>
        ///     Login with username and password
        /// </summary>
        /// <param name="userLoginDTO">User model</param>
        /// <returns>
        ///    Returns the result message of the request.
        /// </returns>
        public HttpResponseMessage Post([FromBody] UserLoginDTO userLoginDTO)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            //Check existing
            if (northwindContext.TbIuser.Any(user => user.Username.Equals(userLoginDTO.Username)))
            {
                TbIuser user = northwindContext.TbIuser.Where(u => u.Username.Equals(userLoginDTO.Username)).FirstOrDefault();

                //Calculate hash password from data of Client and compare with hash in server with salt.
                var client_post_hash_password = Convert.ToBase64String(
                    Common.SaltHashPassword(
                        Encoding.ASCII.GetBytes(userLoginDTO.Password),
                        Convert.FromBase64String(user.Salt)));

                if (client_post_hash_password.Equals(user.Password))
                {
                    UserRegisterDTO userModel = new UserRegisterDTO()
                    {
                        FullName = user.FullName,
                        Username = user.Username,
                        Password = user.Password,
                        CustomerId = user.CustomerId
                    };
                    response.Content = new StringContent(JsonConvert.SerializeObject(userModel));
                    return response;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.PreconditionFailed;
                    response.Content = new StringContent(JsonConvert.SerializeObject("Wrong Password"));
                    return response;
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Content = new StringContent(JsonConvert.SerializeObject("User is not existing in Database"));
                return response;
            }
        }
    }
}
