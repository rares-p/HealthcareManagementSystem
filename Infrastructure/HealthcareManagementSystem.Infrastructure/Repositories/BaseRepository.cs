using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly HealthcareManagementSystemDbContext _context;

        public BaseRepository(HealthcareManagementSystemDbContext context)
        {
            this._context = context;
        }
        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);

            if (!result.IsSuccess)
                return Result<T>.Failure($"Entity with id {id} not found");

            _context.Set<T>().Remove(result.Value);
            await _context.SaveChangesAsync();
            return Result<T>.Success(result.Value);
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetPagedResponseAsync(int page, int size)
        {
            var result = await _context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetAllAsync()
        {
            var result = await _context.Set<T>().AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
    }
}
