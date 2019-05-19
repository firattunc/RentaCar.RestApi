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
    public class KullaniciController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var KullaniciBusiness = new RentCar.Business.KullaniciBusiness())
            {
                // Get customers from business layer (Core App)
                List<Kullanici> roller = KullaniciBusiness.listele();

                // Prepare a content
                var content = new ResponseContent<Kullanici>(roller);

                // Return content as a json and proper http response
                return new StandartResult<Kullanici>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.KullaniciBusiness())
            {
                List<Kullanici> customers = new List<Kullanici>();
                customers.Add(customerBusiness.SelectKullaniciById(id));
                // Prepare a content

                var content = new ResponseContent<Kullanici>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Kullanici>(content, Request);
            }
        }
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Kullanici kullanici)
        {
            var roleBusiness = new RentCar.Business.RoleBusiness();

            var role = roleBusiness.SelectRoleById(kullanici.RoleId);

            var content = new ResponseContent<Kullanici>(null);

            using (var customerBusiness = new RentCar.Business.KullaniciBusiness())
            {
                kullanici.role = role;
                content.Result = customerBusiness.InsertKullanici(kullanici).ToString();

                return new StandartResult<Kullanici>(content, Request);
            }
        }


        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]Kullanici Kullanici)
        {
            var content = new ResponseContent<Kullanici>(null);

            if (Kullanici != null)
            {
                using (var customerBusiness = new RentCar.Business.KullaniciBusiness())
                {
                    Kullanici KullaniciResult = customerBusiness.Update(id, Kullanici);
                    if (KullaniciResult != null)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }


                    return new StandartResult<Kullanici>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Kullanici>(content, Request);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Kullanici>(null);

            using (var customerBusiness = new RentCar.Business.KullaniciBusiness())
            {
                int KullaniciResult = customerBusiness.DeleteKullanici(id);
                if (KullaniciResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<Kullanici>(content, Request);
            }
        }
    }
}