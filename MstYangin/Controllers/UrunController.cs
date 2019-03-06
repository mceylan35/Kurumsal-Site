using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MstYangin.Controllers
{
    public class UrunController : Controller
    {
        UnitofWork.UnitofWork uow=new UnitofWork.UnitofWork();

        // GET: Urun
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id)
        {
          var urun = uow.UrunRepository.Get(id);
            if (urun!=null)
            {

                return View(urun);
                ViewBag.KategoriId =
                 new SelectList(uow.KategoriRepository.GetAll(), "Id", "KategoriAdi", urun.KategoriId);


            }

           
            return HttpNotFound();
        }

        public ActionResult Liste()
        {
            var urunler = uow.UrunRepository.GetAll();
            return View(urunler);
        }

    }
}