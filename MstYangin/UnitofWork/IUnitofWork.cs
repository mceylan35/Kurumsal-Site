using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MstYangin.Repository.Abstract;

namespace MstYangin.UnitofWork
{
    public interface IUnitofWork:IDisposable
    {

        //generic yapı için veri çekmek için kullanıcaz
        //context burdan çekilecel
        IGenericRepository<T> GetRepository<T>() where T : class;


        /// <summary>
        /// Değişiklikleri Kaydet.
        /// </summary>
        /// <returns></returns>
        int SaveChange();
        /// <summary>
        /// transaction başlat
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// kayıt esnasında bir sorun olmaz ise
        /// transaction durdur.
        /// </summary>
        void Commit();
        /// <summary>
        /// kayıt esnasında bir sorun olursa değişikleri geri al.
        /// </summary>
        void RollBack();


    }
}
