using Hotel_Listing.Data;
using Hotel_Listing.IRepository;
using Hotel_Listing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using X.PagedList;

namespace Hotel_Listing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }
        public async Task Delete(int id)
        {
            T entity = await _db.FindAsync(id);
            _db.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {

            IQueryable<T> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public Task<IPagedList<T>> GetPageList(RequestParams requestParams = null, List<string> includes = null )
        {
            IQueryable<T> query = _db;
           

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
           
            return Task.FromResult(query.AsNoTracking().ToPagedList(requestParams.PageNumber,requestParams.PageSize));
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRange(IEnumerable<T> entity)
        {
            await _db.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }


    }
}
