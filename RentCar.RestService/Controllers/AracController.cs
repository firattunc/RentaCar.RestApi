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
    public class AracController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            using (var AracBusiness = new RentCar.Business.AracBusiness())
            {
                // Get customers from business layer (Core App)
                List<Arac> roller = AracBusiness.listele();

                // Prepare a content
                var content = new ResponseContent<Arac>(roller);

                // Return content as a json and proper http response
                return new StandartResult<Arac>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.AracBusiness())
            {
                List<Arac> customers = new List<Arac>();
                customers.Add(customerBusiness.SelectAracById(id));
                // Prepare a content

                var content = new ResponseContent<Arac>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Arac>(content, Request);
            }
        }
        // POST api/<controller>
        //ŞirketId'ye göre aracları listele
        public IHttpActionResult Post(int id)
        {

            using (var AracBusiness = new RentCar.Business.AracBusiness())
            {
                // Get customers from business layer (Core App)
                List<Arac> araclar = AracBusiness.SelectAraclarBySirketId(id);

                // Prepare a content
                var content = new ResponseContent<Arac>(araclar);

                // Return content as a json and proper http response
                return new StandartResult<Arac>(content, Request);
            }


        }
        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Arac arac)
        {
            var content = new ResponseContent<Arac>(null);
            var sirketBusiness = new RentCar.Business.SirketBusiness();

            var sirket = sirketBusiness.SelectSirketById(arac.Sirket.Id);

            using (var customerBusiness = new RentCar.Business.AracBusiness())
            {
                arac.Sirket = sirket;
                content.Result = customerBusiness.InsertArac(arac).ToString();

                return new StandartResult<Arac>(content, Request);
            }
        }


        // PUT api/<controller>/5
        //Arac Durumunu Güncelle
        public IHttpActionResult Put(int id, [FromBody]Arac arac)
        {
            var content = new ResponseContent<Arac>(null);
            var aracBusiness = new RentCar.Business.AracBusiness();
            

            if (arac != null)
            {
                using (var customerBusiness = new RentCar.Business.AracBusiness())
                {
                    var durum = arac.aracDurum;
                    arac = aracBusiness.SelectAracById(arac.Id);
                    arac.aracDurum = durum;
                    Arac AracResult = customerBusiness.Update(id, arac);
                    if (AracResult != null)
                    {
                        content.Result = "1";
                    }
                    else
                    {
                        content.Result = "0";
                    }


                    return new StandartResult<Arac>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Arac>(content, Request);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Arac>(null);

            using (var customerBusiness = new RentCar.Business.AracBusiness())
            {
                int AracResult = customerBusiness.DeleteArac(id);
                if (AracResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<Arac>(content, Request);
            }
        }
    }
}