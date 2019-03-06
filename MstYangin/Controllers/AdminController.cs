using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MstYangin.Models;
using MstYangin.UnitofWork;

namespace MstYangin.Controllers
{
    public class AdminController : Controller
    {


        UnitofWork.UnitofWork uow = new UnitofWork.UnitofWork();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public void IletisimSil(int id)
        {
            var mesaj = uow.IletisimRepository.Get(id);
            if (mesaj!=null)
            {
                uow.IletisimRepository.Delete(mesaj);
                uow.SaveChange();
            }
        }

        public ActionResult Iletisim()
        {
            var iletisim = uow.IletisimRepository.GetAll();

            return View(iletisim);

        }

        public ActionResult UrunEkle()
        {
           
            ViewBag.KategoriId = new SelectList(uow.KategoriRepository.GetAll(), "Id","KategoriAdi");
         
            return View();
        }
   
  

    //          
    //            ViewData["Message"] = "Resim yüklendi.";
    //            return RedirectToAction("ReferansListe");

    //        }
    //        else
    //        {
    //            ViewData["Message"] = "Resim(Png,Jpg) dosyası seçiniz.";
    //        }

    //    }
    //    else
    //{
    //ViewData["Message"] = "Bir Dosya Seçiniz.";
    //}
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UrunEkle(Urunler urun, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength>0 || file!=null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    if (extention==".jpg" || extention==".png" ||extention==".jpeg")
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload"), filename);
                        file.SaveAs(path);
                        uow.UrunRepository.Add(new Urunler
                        {
                            Baslik = urun.Baslik,
                            Aciklama = urun.Aciklama,
                            UrunAd = urun.UrunAd,
                            Resim = filename,
                            KategoriId = urun.KategoriId,
                            Icerik = urun.Icerik,
                            UrunKod = urun.UrunKod
                        });
                        uow.SaveChange();
                    }
                }
                
                return RedirectToAction("UrunListe");
            }

            ViewBag.KategoriId = new SelectList(uow.KategoriRepository.GetAll(), "Id", "KategoriAdi");
            return View(urun);
        }

        public ActionResult UrunListe()
        {
            var urunler = uow.UrunRepository.GetAll();
            return View(urunler);
        }

        public void UrunSil(int id)
        {
            var urun = uow.UrunRepository.Get(id);
            if (urun!=null)
            {
                uow.UrunRepository.Delete(urun);
                uow.SaveChange();

            }
        }

        public ActionResult UrunDuzenle(int id)
        {
           

            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var urun = uow.UrunRepository.Get(id);

            if (urun==null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriId = new SelectList(uow.KategoriRepository.GetAll(), "Id", "KategoriAdi", urun.Id);

        
            return View(urun);
        }

        //var extensition = Path.GetExtension(file.FileName);
        //    if (extensition == ".jpg" || extensition == ".png" || extensition == ".jpeg")
        //{

        //    var filename = Path.GetFileName(file.FileName);
        //    var path = Path.Combine(Server.MapPath("~/upload"), filename); //yolu aldık
        //    file.SaveAs(path);


        [HttpPost]
        [ValidateInput(false)]
        
        [ValidateAntiForgeryToken]
        public ActionResult UrunDuzenle(Urunler urun, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var extention = Path.GetExtension(file.FileName);
                    if (extention == ".jpg" || extention == ".png" || extention == ".jpeg")
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload"), filename);
                        file.SaveAs(path);
                    }
                    uow.UrunRepository.Edit(new Urunler
                    {
                        Baslik = urun.Baslik,
                        Icerik = urun.Icerik,
                        Id = urun.Id,
                        Aciklama = urun.Aciklama,
                        UrunAd = urun.UrunAd,
                        UrunKod = urun.UrunKod,
                        KategoriId = urun.KategoriId,
                        Resim = file.FileName
                    });
                    uow.SaveChange();
                    return RedirectToAction("UrunListe");
                }
                catch (Exception e)
                {
                  
                    throw;
                }
              
            }
            ViewBag.KategoriId=new SelectList(uow.KategoriRepository.GetAll(),"Id","KategoriAdi",urun.Id);
            return View(urun);
        }

        public ActionResult KategoriEkle()
        {
            var model = new Kategori();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KategoriEkle(Kategori kat)
        {
            Thread.Sleep(1000);
            if (ModelState.IsValid)
            {

                uow.KategoriRepository.Add(kat);

                uow.SaveChange();
                return RedirectToAction("KategoriListe");

            }

            return HttpNotFound();
        }

        public void KategoriSil(int id)
        {
            Thread.Sleep(1000);
            var kategori = uow.KategoriRepository.Get(id);
            if (kategori != null)
            {
                uow.KategoriRepository.Delete(kategori);
                uow.SaveChange();
            }


        }

        public ActionResult KategoriListe()
        {
            var kategoriler = uow.KategoriRepository.GetAll();
            if (kategoriler == null)
            {
                return HttpNotFound();
            }

            return PartialView(kategoriler);
        }

        public ActionResult KategoriDuzenle(int id)
        {
            var kategori = uow.KategoriRepository.Get(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }

            uow.KategoriRepository.Edit(kategori);
            return RedirectToAction("KategoriListe");
        }

        public ActionResult Hakkimizda()
        {

            var hakkimizda = uow.HakkimizdaRepository.GetAll().SingleOrDefault();

            return View(hakkimizda);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Hakkimizda(Hakkimizda hakkimizda)
        {
            if (hakkimizda!=null)
            {
                
          
            if (ModelState.IsValid)
            {

                uow.HakkimizdaRepository.Edit(hakkimizda);
                uow.SaveChange();

                }
            }

            return RedirectToAction("Hakkimizda");
        }

        public ActionResult Seo()
        {
            var seo = uow.SeoRepository.GetAll().SingleOrDefault();
            if (seo!=null)
            {
                return View(seo);
            }

            return View(new Seo());
        }
        [HttpPost]
        public ActionResult Seo(Seo seo)
        {
            if (ModelState.IsValid)
            {
                uow.SeoRepository.Edit(seo);
                uow.SaveChange();

            }

            return View(seo);
        }
        public ActionResult Adres()
        {
            var adres = uow.AdresRepository.GetAll().SingleOrDefault();
            if (adres == null)
            {
                return View(new Adres());
            }

            return View(adres);
        }

        [HttpPost]

        public ActionResult Adres(Adres adres)
        {
            if (adres!=null)
            {
                
           
            if (ModelState.IsValid)
            {
                uow.AdresRepository.Edit(adres);
                uow.SaveChange();
                }
            }

            return View(adres);
        }

      

        public ActionResult HaberEkle()
        {
            var model = new Haberler();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberEkle(Haberler haber)
        {
            if (haber!=null)
            {
                
           
            Thread.Sleep(1000);
            if (ModelState.IsValid)
            {
                uow.HaberRepository.Add(haber);


                uow.SaveChange();
                
                return RedirectToAction("HaberListe");

                }
            }

            return HttpNotFound();
        }

        public void HaberSil(int id)
        {
            Thread.Sleep(1000);
            var haber = uow.HaberRepository.Get(id);
            if (haber != null)
            {
                uow.HaberRepository.Delete(haber);
                uow.SaveChange();
            }


        }

        public ActionResult HaberDuzenle(int id)
        {
            if (ModelState.IsValid)
            {
                var haber = uow.HaberRepository.Get(id);
                if (haber != null)
                {
                    return View(haber);
                }
            }
           

            return HttpNotFound();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberDuzenle(Haberler haber)
        {

             
          
            if (ModelState.IsValid)
            {
                
           
            if (haber!=null)
            {
                uow.HaberRepository.Edit(haber);
                uow.SaveChange();
                return RedirectToAction("HaberListe");
            }
                }
            
            return View();
        }

        public ActionResult HaberListe()
        {
            var haberler = uow.HaberRepository.GetAll();
            if (haberler == null)
            {
                HttpNotFound();
            }

            return View(haberler);

        }

        public ActionResult ReferansEkle()
        {
            var model = new Referanslar();
            return View(model);
        }

        [HttpPost]
        public ActionResult ReferansEkle(HttpPostedFileBase file, Referanslar referans)
        {
            if (referans!=null)
            {
                
            
                
            if (file != null && file.ContentLength > 0)
            {
                var extensition = Path.GetExtension(file.FileName);
                if (extensition == ".jpg" || extensition == ".png" || extensition == ".jpeg")
                {

                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/upload"), filename); //yolu aldık
                    file.SaveAs(path);
                    uow.ReferansRepository.Add(new Referanslar
                    {
                        ReferansAd = referans.ReferansAd,
                        ReferansResim = filename
                    });
                    uow.SaveChange();
                    ViewData["Message"] = "Resim yüklendi.";
                    return RedirectToAction("ReferansListe");

                }
                else
                {
                    ViewData["Message"] = "Resim(Png,Jpg) dosyası seçiniz.";
                }

            }
            else
            {
                ViewData["Message"] = "Bir Dosya Seçiniz.";
            }
            }

            return View();
        }

        public ActionResult ReferansListe()
        {
            var referanslar = uow.ReferansRepository.GetAll();
            return PartialView(referanslar);

        }

        public void ReferansSil(int id)
        {
            Thread.Sleep(1000);
            var referans = uow.ReferansRepository.Get(id);
            if (referans != null)
            {
                uow.ReferansRepository.Delete(referans);
                uow.SaveChange();
            }

        }

        public ActionResult MansetListe()
        {
            var mansetler = uow.MansetRepository.GetAll();
            return PartialView(mansetler);
        }

        public void MansetSil(int id)
        {
            Thread.Sleep(1000);
            var manset = uow.MansetRepository.Get(id);
            if (manset!=null)
            {
                uow.MansetRepository.Delete(manset);
                uow.SaveChange();
            }

        }

        public ActionResult MansetEkle()
        {
            var model=new Manset();
            return View(model);
        }
        [HttpPost]
        public ActionResult MansetEkle(HttpPostedFileBase file,Manset manset)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength>0 || file.FileName!=null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    if (extention==".jpeg" || extention== ".jpg" || extention== ".png")
                    {
                        var filename = Path.GetFileName(file.FileName);
                       var path= Path.Combine(Server.MapPath("~/upload"),filename);
                        file.SaveAs(path);
                        uow.MansetRepository.Add(new Manset
                        {
                            ResimAciklama = manset.ResimAciklama,
                            ResimAdresi = filename
                        });
                        uow.SaveChange();
                        ViewData["Message"] = "Resim Yüklendi.";
                    }
                    else
                    {
                        ViewData["Message"] = "Resim(jpg,png) dosyası seçiniz!";
                    }
                }
                else
                {
                    ViewData["Message"] = "Bir dosya Seçiniz!";
                }
            }

            return RedirectToAction("MansetListe");
        }

        public ActionResult FotoGaleriEkle()
        {
            var model = new FotoGaleri();
            return View(model);
        }

        [HttpPost]
        public ActionResult FotoGaleriEkle(HttpPostedFileBase file, FotoGaleri fg)
        {
            if (fg!=null)
            {
                
     
            if (file != null && file.ContentLength > 0)
            {
                var extention = Path.GetExtension(file.FileName);
                if (extention == ".jpg" || extention == ".png" || extention == "jpeg")
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/upload"), filename);
                    file.SaveAs(path);
                    uow.FotoGaleriRepository.Add(new FotoGaleri
                    {
                        ResimAciklama = fg.ResimAciklama,
                        ResimUrl = file.FileName
                    });
                    uow.SaveChange();
                    ViewData["Message"] = "Resim Yüklendi.";

                }
                else
                {
                    ViewData["Message"] = "Resim(jpg,png) dosyası seçiniz!";
                }
            }
            else
            {
                ViewData["Message"] = "Bir dosya Seçiniz!";
            }
            }

            return RedirectToAction("FotoGaleriListe");
        }

        public ActionResult FotoGaleriListe()
        {
            var fg=uow.FotoGaleriRepository.GetAll();
            return PartialView(fg);
        }

        public void FotoGaleriSil(int id)
        {
            Thread.Sleep(1000);
            var foto = uow.FotoGaleriRepository.Get(id);

            if (foto!=null)
            {
                uow.FotoGaleriRepository.Delete(foto);
                uow.SaveChange();
            }
        }



    }


}