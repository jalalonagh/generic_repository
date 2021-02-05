using Data.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.BS.Contracts
{
    public interface IBSService<TEntity> where TEntity : class, IEntity
    {
        IRepository<TEntity> repository { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
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
