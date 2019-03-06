using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MstYangin.Controllers
{
    public class FotoGaleriController : Controller
    {
        // GET: FotoGaleri
        UnitofWork.UnitofWork uow =new UnitofWork.UnitofWork();
        public ActionResult Index()
        {
            var galeri = uow.UrunRepository.GetAll();
            return View(galeri);
        }

        public ActionResult Liste()
        {
            var galeri = uow.UrunRepository.GetAll();
            return View(galeri);
        }
    }
}