using Data.Repositories;
using Entities;
using Services.BS.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.BS.Services
{
    public class BSService<TEntity> : IBSService<TEntity> where TEntity : class, IEntity
    {
        public IRepository<TEntity> repository { get; set; }

        public BSService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.AddAsync(entity, cancellationToken, saveNow);
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.AddRangeAsync(entities, cancellationToken, saveNow);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.DeleteAsync(entity, cancellationToken, saveNow);
        }

        public async Task<TEntity> DeleteByIdAsync(int id, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.DeleteByIdAsync(id, cancellationToken, saveNow);
        }

        public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.DeleteRangeAsync(entities, cancellationToken, saveNow);
        }

        public async Task<IEnumerable<TEntity>> DeleteRangeByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.DeleteRangeByIdsAsync(ids, cancellationToken, saveNow);
        }

        public async Task<IEnumerable<TEntity>> FilterRangeAsync(TEntity entity, CancellationToken cancel, int total = 0, int more = int.MaxValue)
        {
            return await repository.FilterRangeAsync(entity, cancel, total, more);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, int total = 0, int more = int.MaxValue)
        {
            return await repository.GetAllAsync(cancellationToken, total, more);
        }

        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await repository.GetByIdAsync(cancellationToken, ids);
        }

        public async Task<TEntity> ItemSync(TEntity Target, TEntity Origin, CancellationToken cancel)
        {
            return await ItemSync(Target, Origin, cancel);
        }

        public async Task<IEnumerable<TEntity>> SearchRangeAsync(TEntity entity, string text, CancellationToken cancel, int total = 0, int more = int.MaxValue)
        {
            return await repository.SearchRangeAsync(entity, text, cancel, total, more);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.UpdateAsync(entity, cancellationToken, saveNow);
        }

        public async Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, TEntity entity, params string[] fields)
        {
            return await repository.UpdateFieldRangeAsync(cancellation, entity, fields);
        }

        public async Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, int Id, params KeyValuePair<string, dynamic>[] fields)
        {
            return await repository.UpdateFieldRangeAsync(cancellation, Id, fields);
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            return await repository.UpdateRangeAsync(entities, cancellationToken, saveNow);
        }
    }
}
