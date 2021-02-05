using Data.Repositories;
using Entities.User;
using System.Linq;

namespace Services.DataInitializer
{
    public class RoleDataInitializer : IDataInitializer
    {
        private readonly IRepository<Role> repository;

        public RoleDataInitializer(IRepository<Role> repository)
        {
            this.repository = repository;
        }

        public void InitializeData()
        {
            if (!repository.Entities.Any())
            {
                repository.AddAsync(new Role { Name = "مدیر", description = "مدیر سایت" }, new System.Threading.CancellationToken(), false);
                repository.AddAsync(new Role { Name = "متقاضی", description = "متقاضی سایت" }, new System.Threading.CancellationToken(), false);
                repository.AddAsync(new Role { Name = "مرکز", description = "مرکز طف قرار داد سایت" }, new System.Threading.CancellationToken(), false);
            }
        }
    }
}
