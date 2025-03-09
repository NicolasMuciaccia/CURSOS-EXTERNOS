using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context; // protected pois os repositorios usam ela com ": base (context)" herdando no constructor

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            // Set<T> puxa a tabela correspondente ao <T> na classe
            // AsNoTracking deixa de rastrear os dados, contudo, so para "Get"
            return _context.Set<T>().AsNoTracking().ToList();
        }
        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}
