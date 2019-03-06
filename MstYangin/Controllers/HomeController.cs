using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MstYangin.Models;

namespace MstYangin.Controllers
{
    public class HomeController : Controller
    {
        UnitofWork.UnitofWork uow=new UnitofWork.UnitofWork();

        // GET: Home
        public ActionResult Index()
        {
         AnasayfaViewModel anasayfa=new AnasayfaViewModel()
         {
             FotoGaleri = uow.FotoGaleriRepository.GetAll().ToList(),
             Urunler = uow.UrunRepository.GetAll().ToList(),
             Referanslar = uow.ReferansRepository.GetAll().Take(5).ToList(),
             Haberler = uow.HaberRepository.GetAll().Take(3).ToList(),
             Hakkimizda = uow.HakkimizdaRepository.GetAll().SingleOrDefault(),
             Adres = uow.AdresRepository.GetAll().SingleOrDefault(),
             Iletisim = new Iletisim(),
             Seo = uow.SeoRepository.GetAll().SingleOrDefault(),
             Mansetler = uow.MansetRepository.GetAll().ToList()

        };

            return View(anasayfa);
        }

        [HttpPost]
        public void Iletisim(Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {

                uow.IletisimRepository.Add(iletisim);
                uow.SaveChange();
            }

           
        }


        public ActionResult Manset()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Urunler()
        {
            return View();
        }
    }
}