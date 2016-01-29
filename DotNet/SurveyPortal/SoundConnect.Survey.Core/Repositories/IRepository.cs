using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
