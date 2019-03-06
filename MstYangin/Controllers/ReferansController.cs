using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MstYangin.Controllers
{
    public class ReferansController : Controller
    {
        UnitofWork.UnitofWork uow =new UnitofWork.UnitofWork();
        // GET: Referans
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Liste()
        {
            var referanslars = uow.ReferansRepository.GetAll();
            return View(referanslars);
        }
    }
}