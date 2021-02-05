using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public interface IRepository<TEntity> : IScopedDependency
        where TEntity : class, IEntity
    {

        DbContext Database { get; }
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<TEntity> DeleteByIdAsync(int id, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> DeleteRangeByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken, bool saveNow = true);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<IEnumerable<TEntity>> FetchByIdAsync(CancellationToken cancellationToken, int id);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, int total = 0, int more = int.MaxValue);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

        Task<IEnumerable<TEntity>> FilterRangeAsync(TEntity entity, CancellationToken cancel, int total = 0, int more = int.MaxValue);
        Task<IEnumerable<TEntity>> SearchRangeAsync(TEntity entity, string text, CancellationToken cancel, int total = 0, int more = int.MaxValue);
        Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, TEntity entity, params string[] fields);
        Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, int Id, params KeyValuePair<string, dynamic>[] fields);

        Task<TEntity> ItemSync(TEntity Target, TEntity Origin, CancellationToken cancel);
    }
}
