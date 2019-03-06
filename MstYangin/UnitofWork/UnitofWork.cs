using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MstYangin.Models;
using MstYangin.Repository.Abstract;
using MstYangin.Repository.Concrete.EntityFramework;


namespace MstYangin.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private DbContextTransaction transaction;

        private MstYanginContext _context = new MstYanginContext();
        private bool disposed = false;
        private EfGenericRepository<Urunler> _urunRepository;
        private EfGenericRepository<Kategori> _kategoriRepository;
        private EfGenericRepository<Adres> _adresRepository;
        private EfGenericRepository<FotoGaleri> _fotoRepository;
        private EfGenericRepository<Hakkimizda> _hakkimizdaRepository;
        private EfGenericRepository<Iletisim> _iletisRepository;
        private EfGenericRepository<Referanslar> _referansRepository;
        private EfGenericRepository<Haberler> _haberRepository;
        private EfGenericRepository<Manset> _mansetRepository;
        private EfGenericRepository<Seo> _seoRepository;


        //unit of work te hepsini newledik ve singleton aracıyla boş işe nesneyi yarattık öncede yaratılmış ise aynı nesneyi yolladık. Ayrıca Unit of work aracıyla hepsine ulabiliceğiz...
        public EfGenericRepository<Urunler> UrunRepository
        {
            get
            {
                if (_urunRepository == null)
                {
                    _urunRepository = new EfGenericRepository<Urunler>(_context);
                }

                return _urunRepository;
            }
        }

        public EfGenericRepository<Manset> MansetRepository
        {
            get
            {
                if (_mansetRepository == null)
                {
                    _mansetRepository = new EfGenericRepository<Manset>(_context);
                }

                return _mansetRepository;
            }
        }

        public EfGenericRepository<Seo> SeoRepository
        {

            get
            {
                if (_seoRepository == null)
                {
                    _seoRepository = new EfGenericRepository<Seo>(_context);
                }

                return _seoRepository;
            }
        }

        public EfGenericRepository<Kategori> KategoriRepository
        {
            get
            {
                if (_kategoriRepository == null)
                {
                    _kategoriRepository = new EfGenericRepository<Kategori>(_context);
                }

                return _kategoriRepository;
            }
        }

        public EfGenericRepository<Adres> AdresRepository
        {
            get
            {
                if (_adresRepository == null)
                {
                    _adresRepository = new EfGenericRepository<Adres>(_context);
                }

                return _adresRepository;
            }

        }

        public EfGenericRepository<FotoGaleri> FotoGaleriRepository
        {
            get
            {
                if (_fotoRepository == null)
                {
                    _fotoRepository = new EfGenericRepository<FotoGaleri>(_context);
                }

                return _fotoRepository;
            }
        }

        public EfGenericRepository<Hakkimizda> HakkimizdaRepository
        {
            get
            {
                if (_hakkimizdaRepository == null)
                {
                    _hakkimizdaRepository = new EfGenericRepository<Hakkimizda>(_context);
                }

                return _hakkimizdaRepository;
            }
        }

        public EfGenericRepository<Iletisim> IletisimRepository
        {
            get
            {
                if (_iletisRepository == null)
                {
                    _iletisRepository = new EfGenericRepository<Iletisim>(_context);
                }

                return _iletisRepository;
            }
        }

        public EfGenericRepository<Referanslar> ReferansRepository
        {
            get
            {
                if (_referansRepository == null)
                {
                    _referansRepository = new EfGenericRepository<Referanslar>(_context);
                }

                return _referansRepository;
            }
        }

        public EfGenericRepository<Haberler> HaberRepository
        {
            get
            {
                if (_haberRepository == null)
                {
                    _haberRepository = new EfGenericRepository<Haberler>(_context);
                }

                return _haberRepository;
            }
        }



        //public UnitofWork(Models.MstYangin context)
        //{
        //    Database.SetInitializer<Models.MstYangin>(null);
        //    if (context==null)
        //    {
        //        throw new ArgumentException("context is null ");
        //    }
        //    _context = context;
        //}
        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            //durdurma işlemi yapıyor...
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        public int SaveChange()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new EfGenericRepository<T>(_context);
        }
    }
}