using GreenStar.Entity.User;
using GreenStar.Service;
using GreenStar.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace GreenStar.API.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // POST: api/user
        [ResponseType(typeof(UserDetails))]
        [HttpPost]
        public IHttpActionResult Login(UserModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

               var user= _unitOfWork.Users.LoginCheck(model.email, Utility.Base64Encode(model.password));
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                   return  Unauthorized();
                }
               
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
    public class UserModel {
        public string email { get; set; }
        public string password { get; set; }
    }
}
