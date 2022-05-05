using Microsoft.EntityFrameworkCore;
using Shop.Dal.Abstract.IRepository;
using Shop.Dal.Concrete.EntityFramework.ContextBase;
using System.Linq.Expressions;

namespace Shop.Dal.Concrete.EntityFramework.Repository
{
    public class EFEntityRepository<T> : IEntityRepository<T> where T : class
    {
        #region Veriables
        protected DataContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Constructor
        public EFEntityRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region Custom Methods
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.AsNoTracking().Where(filter);

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
                return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.AsNoTracking().Where(filter);

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.AsNoTracking().Where(filter);

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.AsNoTracking().Where(filter);

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
                return query.FirstOrDefault();
        }
        public void Add(T entity) => _context.Add(entity);
        #endregion
    }
}