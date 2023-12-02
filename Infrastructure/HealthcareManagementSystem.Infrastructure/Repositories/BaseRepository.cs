using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly HealthcareManagementSystemDbContext Context;

        public BaseRepository(HealthcareManagementSystemDbContext context)
        {
            this.Context = context;
        }
        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);

            if (!result.IsSuccess)
                return Result<T>.Failure($"Entity with id {id} not found");

            Context.Set<T>().Remove(result.Value);
            await Context.SaveChangesAsync();
            return Result<T>.Success(result.Value);
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var result = await Context.Set<T>().FindAsync(id);
            if (result == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetPagedResponseAsync(int page, int size)
        {
            var result = await Context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetAllAsync()
        {
            var result = await Context.Set<T>().AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
    }
}
