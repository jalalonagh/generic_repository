using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Entities.User;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);

        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);

        // additional methods
        Task<User> AddAsync(User entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<User> DeleteAsync(User entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<User> DeleteByIdAsync(int id, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<User>> DeleteRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<User>> DeleteRangeByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken, bool saveNow = true);
        Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken, int total = 0, int more = int.MaxValue);
        Task<User> UpdateAsync(User entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true);

        Task<IEnumerable<User>> FilterRangeAsync(User entity, CancellationToken cancel, int total = 0, int more = int.MaxValue);
        Task<IEnumerable<User>> SearchRangeAsync(User entity, string text, CancellationToken cancel, int total = 0, int more = int.MaxValue);
        Task<User> UpdateFieldRangeAsync(CancellationToken cancellation, User entity, params string[] fields);
        Task<User> UpdateFieldRangeAsync(CancellationToken cancellation, int Id, params KeyValuePair<string, dynamic>[] fields);

        Task<User> ItemSync(User Target, User Origin, CancellationToken cancel);
    }
}