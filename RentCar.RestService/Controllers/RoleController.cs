using RentCar.Entities;
using RentCar.Models;
using RentCar.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RentCar.RestService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]  
    public class RoleController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var RoleBusiness = new RentCar.Business.RoleBusiness())
            {
                // Get customers from business layer (Core App)
                List<Role> roller = RoleBusiness.listele();

                // Prepare a content
                var content = new ResponseContent<Role>(roller);

                // Return content as a json and proper http response
                return new StandartResult<Role>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.RoleBusiness())
            {
                List<Role> customers = new List<Role>();
                customers.Add(customerBusiness.SelectRoleById(id));
                // Prepare a content

                var content = new ResponseContent<Role>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Role>(content, Request);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Role role)
        {

            var content = new ResponseContent<Role>(null);

            using (var customerBusiness = new RentCar.Business.RoleBusiness())
            {
                content.Result = customerBusiness.InsertRole(role).ToString();

                return new StandartResult<Role>(content, Request);
            }
        }


        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]Role role)
        {
            var content = new ResponseContent<Role>(null);

            if (role != null)
            {
                using (var customerBusiness = new RentCar.Business.RoleBusiness())
                {
                    Role roleResult = customerBusiness.Update(id, role);
                    if (roleResult != null)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }


                    return new StandartResult<Role>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Role>(content, Request);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Role>(null);

            using (var customerBusiness = new RentCar.Business.RoleBusiness())
            {
                int roleResult = customerBusiness.DeleteRole(id);
                if (roleResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<Role>(content, Request);
            }
        }
    }
}