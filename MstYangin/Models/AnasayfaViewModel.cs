using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MstYangin.Models
{
    public class AnasayfaViewModel
    {
        public List<Urunler> Urunler { get; set; }
        public List<FotoGaleri> FotoGaleri { get; set; }
        public List<Referanslar> Referanslar { get; set; }
        public List<Haberler> Haberler { get; set; }
        public Iletisim Iletisim { get; set; }
        public Hakkimizda Hakkimizda { get; set; }
        public Adres Adres { get; set; }
        public Seo Seo { get; set; }
        public List<Manset> Mansetler { get; set; }
    }
}