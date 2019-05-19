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
    public class KiralamaController : ApiController
    {
        // GET api/<controller>
        //Kiralama İstekleri
        public IHttpActionResult Get()
        {
            using (var KiralamaBusiness = new RentCar.Business.KiralamaBusiness())
            {
                // Get customers from business layer (Core App)
                List<Kiralama> istekler = KiralamaBusiness.SelectAllKiralamaIstekleri();

                // Prepare a content
                var content = new ResponseContent<Kiralama>(istekler);

                // Return content as a json and proper http response
                return new StandartResult<Kiralama>(content, Request);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            using (var customerBusiness = new RentCar.Business.KiralamaBusiness())
            {
                List<Kiralama> customers = new List<Kiralama>();
                customers.Add(customerBusiness.SelectKiralamaById(id));
                // Prepare a content

                var content = new ResponseContent<Kiralama>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Kiralama>(content, Request);
            }
        }


        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Kiralama kiralama)
        {

            var content = new ResponseContent<Kiralama>(null);

            using (var kiralamaBusiness = new RentCar.Business.KiralamaBusiness())
            {              
                var kullaniciBusiness = new RentCar.Business.KullaniciBusiness();
                var aracBusiness = new RentCar.Business.AracBusiness();


                Kullanici k=kullaniciBusiness.SelectKullaniciById(kiralama.Musteri.Id);
                Arac a = aracBusiness.SelectAracById(kiralama.Arac.Id);
            

                kiralama.Musteri = k;
                kiralama.Arac = a;

                content.Result = kiralamaBusiness.InsertKiralama(kiralama).ToString();

                return new StandartResult<Kiralama>(content, Request);
            }
        }


        // PUT api/<controller>/5
        //İsteği Onayla
        public IHttpActionResult Put(int id, [FromBody]Kiralama kiralama)
        {
            var content = new ResponseContent<Kiralama>(null);

            var kullaniciBusiness = new RentCar.Business.KullaniciBusiness();
            
                var calisan = kullaniciBusiness.SelectKullaniciById(kiralama.Calisan.Id);          

                if (kiralama != null)
                {
                    using (var kiralamaBusiness = new RentCar.Business.KiralamaBusiness())
                    {
                        var kiralanacak = kiralamaBusiness.SelectKiralamaById(id);
                        kiralanacak.Calisan = calisan;
                        kiralanacak.kiralamaTarihi = kiralama.kiralamaTarihi;
                        Kiralama KiralamaResult = kiralamaBusiness.Update(id, kiralanacak);
                        if (KiralamaResult != null)
                        {
                        content.Result = "1";
                        }
                        else
                        {
                            content.Result = "0";
                        }


                        return new StandartResult<Kiralama>(content, Request);
                    }
                }
            
            content.Result = "0";

            return new StandartResult<Kiralama>(content, Request);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Kiralama>(null);

            using (var kiralamaBusiness = new RentCar.Business.KiralamaBusiness())
            {
                int KiralamaResult = kiralamaBusiness.DeleteKiralama(id);
                if (KiralamaResult > 0)
                {
                    content.Result = "1";
                }
                else
                {
                    content.Result = "0";
                }

                return new StandartResult<Kiralama>(content, Request);
            }
        }
    }
}