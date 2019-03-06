using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MstYangin.Controllers
{
    public class HaberController : Controller
    {
        // GET: Haber
        UnitofWork.UnitofWork uow=new UnitofWork.UnitofWork();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id)
        {
            var haber = uow.HaberRepository.Get(id);
            if (haber!=null)
            {
                return View(haber);
            }

            return HttpNotFound();


        }

        public ActionResult Liste()
        {
            var haberler = uow.HaberRepository.GetAll();
            return View(haberler);
        }
    }
}