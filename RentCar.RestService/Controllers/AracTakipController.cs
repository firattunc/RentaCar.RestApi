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
    public class AracTakipController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var AracTakipBusiness = new RentCar.Business.AracTakipBusiness())
            {
                // Get customers from business layer (Core App)
                List<AracTakip> roller = AracTakipBusiness.listele();

                // Prepare a content
                var content = new ResponseContent<AracTakip>(roller);

                // Return content as a json and proper http response
                return new StandartResult<AracTakip>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.AracTakipBusiness())
            {
                List<AracTakip> customers = new List<AracTakip>();
                customers.Add(customerBusiness.SelectAracTakipById(id));
                // Prepare a content

                var content = new ResponseContent<AracTakip>(customers);

                // Return content as a json and proper http response
                return new StandartResult<AracTakip>(content, Request);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]AracTakip AracTakip)
        {
            var aracBusiness = new RentCar.Business.AracBusiness();
            var kullaniciBusiness = new RentCar.Business.KullaniciBusiness();
            AracTakip.Musteri = kullaniciBusiness.SelectKullaniciById(AracTakip.Musteri.Id);
            AracTakip.Arac = aracBusiness.SelectAracById(AracTakip.Arac.Id);

            var content = new ResponseContent<AracTakip>(null);

            using (var customerBusiness = new RentCar.Business.AracTakipBusiness())
            {
                content.Result = customerBusiness.InsertAracTakip(AracTakip).ToString();

                return new StandartResult<AracTakip>(content, Request);
            }
        }


        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]AracTakip AracTakip)
        {
            var content = new ResponseContent<AracTakip>(null);

            if (AracTakip != null)
            {
                using (var customerBusiness = new RentCar.Business.AracTakipBusiness())
                {
                    AracTakip AracTakipResult = customerBusiness.Update(id, AracTakip);
                    if (AracTakipResult != null)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }


                    return new StandartResult<AracTakip>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<AracTakip>(content, Request);
        }

        // DELETE api/<controller>/5
        //Verilen Musteri Id parametresine göre kayıtları siler
        public IHttpActionResult Delete(int id)
        {

            var content = new ResponseContent<AracTakip>(null);
            
            using (var aracTakipBusiness = new RentCar.Business.AracTakipBusiness())
            {
                int AracTakipResult=1;
                foreach (var item in aracTakipBusiness.SelectAracTakiplerByMusteriId(id))
                {
                     AracTakipResult = aracTakipBusiness.DeleteAracTakip(item.Id);
                }
                
                if (AracTakipResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<AracTakip>(content, Request);
            }
        }
    }
}