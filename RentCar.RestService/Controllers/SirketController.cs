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
    public class SirketController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var SirketBusiness = new RentCar.Business.SirketBusiness())
            {
                // Get customers from business layer (Core App)
                List<Sirket> roller = SirketBusiness.listele();

                // Prepare a content
                var content = new ResponseContent<Sirket>(roller);

                // Return content as a json and proper http response
                return new StandartResult<Sirket>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.SirketBusiness())
            {
                List<Sirket> customers = new List<Sirket>();
                customers.Add(customerBusiness.SelectSirketById(id));
                // Prepare a content

                var content = new ResponseContent<Sirket>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Sirket>(content, Request);
            }
        }
      

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Sirket Sirket)
        {

            var content = new ResponseContent<Sirket>(null);

            using (var customerBusiness = new RentCar.Business.SirketBusiness())
            {
                content.Result = customerBusiness.InsertSirket(Sirket).ToString();

                return new StandartResult<Sirket>(content, Request);
            }
        }


        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]Sirket Sirket)
        {
            var content = new ResponseContent<Sirket>(null);

            if (Sirket != null)
            {
                using (var customerBusiness = new RentCar.Business.SirketBusiness())
                {
                    Sirket SirketResult = customerBusiness.Update(id, Sirket);
                    if (SirketResult != null)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }


                    return new StandartResult<Sirket>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Sirket>(content, Request);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Sirket>(null);

            using (var customerBusiness = new RentCar.Business.SirketBusiness())
            {
                int SirketResult = customerBusiness.DeleteSirket(id);
                if (SirketResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<Sirket>(content, Request);
            }
        }
    }
}