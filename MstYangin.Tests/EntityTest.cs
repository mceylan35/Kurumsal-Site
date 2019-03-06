using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MstYangin.Models;
using MstYangin.Repository.Abstract;
using MstYangin.Repository.Concrete.EntityFramework;
using MstYangin.UnitofWork;

namespace MstYangin.Tests
{
    [TestClass]
    public class EntityTest
    {
        private IUnitofWork _uow;
        private Models.MstYangin _dbcontext;
        private IGenericRepository<Urunler> _urunRepository;
        private IGenericRepository<Kategori> _kategoriRepository;
        private IGenericRepository<Haberler> _haberRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbcontext =new Models.MstYangin();
            _uow=new UnitofWork.UnitofWork(_dbcontext);
            _urunRepository=new EfGenericRepository<Urunler>(_dbcontext);
            _kategoriRepository=new EfGenericRepository<Kategori>(_dbcontext);
            _haberRepository=new EfGenericRepository<Haberler>(_dbcontext);
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbcontext = null;
            _uow.Dispose();
            
        }

        [TestMethod]
        public void AddUser()
        {
            Urunler urun=new Urunler()
            {
                UrunAd = "ali",

                Id = 1
            };
            _urunRepository.Add(urun);
            int process = _uow.SaveChange();
            Assert.AreNotEqual(-1,process);
        }
    }
}
