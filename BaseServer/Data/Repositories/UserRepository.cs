using Common;
using Common.Exceptions;
using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        private readonly List<string> skipProperties = new List<string>() { "deleted",
            "creationDateTime",
            "creationDay",
            "creationPersianDateTime",
            "modifiedDateTime",
            "modifiedDay",
            "modifiedPersianDateTime",
            "deletedDateTime",
            "deletedDay",
            "deletedPersianDateTime" };

        PersianCalendar pc = new PersianCalendar();

        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }
        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            return UpdateAsync(user, cancellationToken);
        }
        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.lastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.PasswordHash = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }

        // additional methods
        public async Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await Entities.
                Where(w => w.Id == (int)ids.FirstOrDefault())
                .Include(i => i.messages)
                .Include(i => i.invoices)
                .Include(i => i.comments)
                .Include(i => i.banks)
                .Include(i => i.addresses)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken, int total = 0, int more = int.MaxValue)
        {
            return SetOrder(await Entities
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .ToListAsync());
        }
        public async Task<User> AddAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entity, nameof(entity));

            entity = FixPersianText(entity);

            entity = SetCreationTime(entity);

            if (IsDeletedOrNotActiveEntity(entity))
                return null;

            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            Result = await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entities, nameof(entities));

            entities = FixPersianText(entities);

            entities = SetCreationTimes(entities);

            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

            Result = await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }
        public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entity, nameof(entity));

            entity = FixPersianText(entity);

            entity = SetModifiedTime(entity);

            Entities.Update(entity);

            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<IEnumerable<User>> UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Entities.Local.Clear();

            entities.ToList().ForEach(delegate (User entity)
            {
                Assert.NotNull(entity, nameof(entity));
                Entities.Attach(entity).State = EntityState.Modified;
            });

            entities = FixPersianText(entities);

            entities = SetModifiedTimes(entities);

            Entities.UpdateRange(entities);

            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }
        public async Task<User> DeleteAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entity, nameof(entity));

            entity = SetDeletedTime(entity);

            var deleteProperty = entity.GetType().GetProperty("deleted");
            if (deleteProperty != null)
            {
                deleteProperty.SetValue(entity, true);
                Entities.Attach(entity).State = EntityState.Modified;
                Entities.Update(entity);

                Result = await DbContext.SaveChangesAsync();

                return entity;
            }

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<User> DeleteByIdAsync(int id, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            var entity = await Entities.Where(w => w.Id == id).FirstOrDefaultAsync();

            entity = SetDeletedTime(entity);

            var deleteProperty = entity.GetType().GetProperty("deleted");
            if (deleteProperty != null)
            {
                deleteProperty.SetValue(entity, true);
                Entities.Attach(entity).State = EntityState.Modified;
                Entities.Update(entity);

                //if (saveNow)
                Result = await DbContext.SaveChangesAsync();

                return entity;
            }

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<IEnumerable<User>> DeleteRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entities, nameof(entities));

            entities = SetDeletedTimes(entities);

            foreach (var entity in entities)
            {
                var deleteProperty = entity.GetType().GetProperty("deleted");
                if (deleteProperty != null)
                {
                    deleteProperty.SetValue(entity, true);
                    Entities.Attach(entity).State = EntityState.Modified;
                    Entities.Update(entity);
                }
            }

            //Entities.RemoveRange(entities);
            //if (saveNow)
            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }
        public async Task<IEnumerable<User>> DeleteRangeByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            IEnumerable<User> entities = await Entities.Where(w => ids.Contains(w.Id)).ToListAsync();

            entities = SetDeletedTimes(entities);

            foreach (var entity in entities)
            {
                var deleteProperty = entity.GetType().GetProperty("deleted");
                if (deleteProperty != null)
                {
                    deleteProperty.SetValue(entity, true);
                    Entities.Attach(entity).State = EntityState.Modified;
                    Entities.Update(entity);
                }
            }

            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }

        public User Detach(User entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;

            return entity;
        }
        public User Attach(User entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);

            return entity;
        }

        public async Task<IEnumerable<User>> FilterRangeAsync(User entity, CancellationToken cancel, int total = 0, int more = int.MaxValue)
        {
            var deletedProp = entity.GetType().GetProperty("deleted");
            if (deletedProp != null)
                deletedProp.SetValue(entity, null);

            var properties = entity.GetType()
                .GetProperties()
                .Where(x => x.GetValue(entity) != null
                && (x.PropertyType != typeof(DateTime) || (x.PropertyType == typeof(DateTime) && ((DateTime)x.GetValue(entity)).Year > 10))
                && (x.PropertyType != typeof(DateTime?) || (x.PropertyType == typeof(DateTime?) && ((DateTime)x.GetValue(entity)).Year > 10))
                && (x.PropertyType != typeof(TimeSpan) || (x.PropertyType == typeof(TimeSpan) && ((TimeSpan)x.GetValue(entity)).Hours > 0))
                && (x.PropertyType != typeof(TimeSpan?) || (x.PropertyType == typeof(TimeSpan?) && ((TimeSpan)x.GetValue(entity)).Hours > 0))
                && (x.PropertyType != typeof(Int32) || (x.PropertyType == typeof(Int32) && ((Int32)x.GetValue(entity)) > 0))
                && (x.PropertyType != typeof(long) || (x.PropertyType == typeof(long) && ((long)x.GetValue(entity)) > 0))
                && (x.PropertyType != typeof(string) || (x.PropertyType == typeof(string) && !string.IsNullOrEmpty((string)x.GetValue(entity))))
                && !x.PropertyType.IsGenericType
                && x.PropertyType != typeof(DayOfWeek)
                && x.PropertyType != typeof(DayOfWeek?))
                .ToList();

            var entityType = Database.Model.FindEntityType(typeof(User));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var sql = $"select * from {schema}.{tableName} ";

            if (!properties.Equals(null) && properties.Count() > 0)
            {
                sql += "where ";
                properties.ForEach(delegate (PropertyInfo info)
                {
                    // check and deactivate default numbers like 0 into search and filter action
                    if (info.PropertyType == typeof(bool))
                        sql += $" {info.Name} = {Convert.ToInt32(info.GetValue(entity))} and";
                    else if (info.PropertyType == typeof(string))
                        sql += $" {info.Name} = '{info.GetValue(entity)}' and";
                    else if (info.PropertyType == typeof(DateTime) || info.PropertyType == typeof(DateTime?))
                        sql += $" datediff(day, {info.Name}, '{Convert.ToDateTime(info.GetValue(entity)).ToString("yyyy/MM/dd")}') = 0 and";
                    else
                        sql += $" {info.Name} = {info.GetValue(entity)} and";
                });

                sql += FixedSqlQueryParameters();

                sql = sql.Substring(0, sql.Length - 3);
            }

            IEnumerable<User> data = await Entities.FromSqlRaw(sql)
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .ToListAsync();

            return SetOrder(data);
        }
        public async Task<IEnumerable<User>> SearchRangeAsync(User entity, string text, CancellationToken cancel, int total = 0, int more = int.MaxValue)
        {
            var deletedProp = entity.GetType().GetProperty("deleted");
            if (deletedProp != null)
                deletedProp.SetValue(entity, null);

            var properties = entity.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(string))
                .ToList();

            var entityType = Database.Model.FindEntityType(typeof(User));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var sql = $"select * from {schema}.{tableName} ";

            if (!properties.Equals(null) && properties.Count() > 0)
            {
                sql += "where (";
                properties.ForEach(delegate (PropertyInfo info)
                {
                    sql += $" [{info.Name}] like N'%{text}%' or";
                });

                sql = sql.Substring(0, sql.Length - 2);

                sql = sql + ") and ";

                sql += FixedSqlQueryParameters();

                sql = sql.Substring(0, sql.Length - 3);
            }

            IEnumerable<User> data = await Entities.FromSqlRaw(sql)
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .ToListAsync();

            return SetOrder(data);
        }
        public async Task<User> UpdateFieldRangeAsync(CancellationToken cancellation, User entity, params string[] fields)
        {
            bool saveNow = true;
            if (entity != null && entity.Id > 0 && fields.Length > 0)
            {
                var properties = entity.GetType().GetProperties();

                if (properties != null && properties.Length > 0)
                {
                    var data = await Entities
                        .Where(w => w.Id == entity.Id)
                        .FirstOrDefaultAsync();

                    if (data != null)
                    {
                        foreach (var field in fields)
                        {
                            var prop = properties.Where(w => w.Name == field).FirstOrDefault();
                            if (prop != null)
                            {
                                prop.SetValue(data, prop.GetValue(entity));
                            }
                        }
                    }

                    var result = -1;

                    Entities.Update(data);

                    result = await DbContext.SaveChangesAsync(cancellation);

                    if (result > 0)
                        return data;

                    return null;
                }
            }
            return null;
        }
        public async Task<User> UpdateFieldRangeAsync(CancellationToken cancellation, int Id, params KeyValuePair<string, dynamic>[] fields)
        {
            bool saveNow = true;
            if (fields != null && Id > 0 && fields.Length > 0)
            {
                var data = await Entities
                    .Where(w => w.Id == Id)
                    .FirstOrDefaultAsync();

                var properties = data.GetType().GetProperties();

                if (properties != null && properties.Length > 0)
                {
                    if (data != null)
                    {
                        foreach (var field in fields)
                        {
                            var prop = properties.Where(w => w.Name == field.Key).FirstOrDefault();
                            if (prop != null)
                            {
                                prop.SetValue(data, field.Value);
                            }
                        }
                    }

                    var result = -1;

                    Entities.Update(data);

                    result = await DbContext.SaveChangesAsync(cancellation);

                    if (result > 0)
                        return data;

                    return null;
                }
            }
            return null;
        }

        public async Task<User> ItemSync(User Target, User Origin, CancellationToken cancel)
        {
            try
            {
                var Properties = typeof(User).GetProperties()
                    .Where(x => x.GetValue(Target) != null)
                    .ToList();

                Properties.ForEach(delegate (PropertyInfo info)
                {
                    if (info.PropertyType == typeof(Int64) || info.PropertyType == typeof(Int64?))
                    {
                        if ((long)info.GetValue(Target) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(bool) || info.PropertyType == typeof(bool?))
                    {
                        if (info.GetValue(Target) != null && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(Int32) || info.PropertyType == typeof(Int32?))
                    {
                        if ((int)info.GetValue(Target) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(float) || info.PropertyType == typeof(float?))
                    {
                        if ((float)info.GetValue(Target) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(string))
                    {
                        var value = info.GetValue(Target);
                        if (!string.IsNullOrEmpty(Convert.ToString(value)) && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(DateTime) || info.PropertyType == typeof(DateTime?))
                    {
                        if (((DateTime)info.GetValue(Target)).Year > DateTime.MinValue.Year && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(TimeSpan) || info.PropertyType == typeof(TimeSpan?))
                    {
                        if (((TimeSpan)info.GetValue(Target)).TotalSeconds > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(double) || info.PropertyType == typeof(double?))
                    {
                        if (((double)info.GetValue(Target)) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(decimal) || info.PropertyType == typeof(decimal?))
                    {
                        if (((decimal)info.GetValue(Target)) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                    else if (info.PropertyType == typeof(byte) || info.PropertyType == typeof(byte?))
                    {
                        if (((byte)info.GetValue(Target)) > 0 && info.GetValue(Target) != info.GetValue(Origin))
                            info.SetValue(Origin, info.GetValue(Target));
                    }
                });

                return Origin;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        private User FixPersianText(User entity)
        {
            var properties = entity.GetType()
                .GetProperties()
                .Where(x => x.GetValue(entity) != null && x.PropertyType == typeof(string))
                .ToList();

            properties.ForEach(delegate (PropertyInfo info)
            {
                info.SetValue(entity, info.GetValue(entity).ToString().FixPersianChars());
            });

            return entity;
        }
        private IEnumerable<User> FixPersianText(IEnumerable<User> entities)
        {
            List<User> persianEntities = new List<User>();
            entities.ToList().ForEach(delegate (User entity)
            {
                entity = FixPersianText(entity);
                persianEntities.Add(entity);
            });
            return persianEntities;
        }
        private bool IsDeletedOrNotActiveEntity(User entity)
        {
            try
            {
                var info = entity.GetType().GetProperty("deleted");
                var infoActivation = entity.GetType().GetProperty("isActive");

                if (info.GetValue(entity) == null)
                {
                    return false;
                }

                if (infoActivation.GetValue(entity) == null)
                {
                    return false;
                }

                if ((info != null && (bool)info.GetValue(entity) == true) || (infoActivation != null && (infoActivation.GetValue(entity) == null || (bool)infoActivation.GetValue(entity) == false)))
                    return true;
            }
            catch (Exception Ex)
            {

            }

            return false;
        }
        private IEnumerable<User> GetUnDeleteds(IEnumerable<User> entities)
        {
            List<User> undeleteds = new List<User>();
            if (entities != null && entities.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    if (!IsDeletedOrNotActiveEntity(entity))
                        undeleteds.Add(entity);
                }
            }

            return undeleteds;
        }
        private string FixedSqlQueryParameters(bool or = false)
        {
            if (or)
            {
                return $" ([deleted] is null or [deleted] = 0) or ([isActive] is null or [isActive] = 1) or";
            }

            return $" ([deleted] is null or [deleted] = 0) and ([isActive] is null or [isActive] = 1) and";
        }
        private User SetCreationTime(User entity)
        {
            var nowDT = DateTime.Now;
            var propertyCreate = entity.GetType().GetProperty("creationDateTime");
            if (propertyCreate != null)
            {
                propertyCreate.SetValue(entity, nowDT);
            }

            var propertyPersianCreate = entity.GetType().GetProperty("creationPersianDateTime");
            if (propertyPersianCreate != null)
            {
                var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                propertyPersianCreate.SetValue(entity, dt);
            }

            return entity;
        }
        private IEnumerable<User> SetCreationTimes(IEnumerable<User> entities)
        {
            IEnumerable<User> newEntities = new List<User>();

            foreach (var entity in entities)
            {
                var nowDT = DateTime.Now;
                var propertyCreate = entity.GetType().GetProperty("creationDateTime");
                if (propertyCreate != null)
                {
                    propertyCreate.SetValue(entity, nowDT);
                }

                var propertyPersianCreate = entity.GetType().GetProperty("creationPersianDateTime");
                if (propertyPersianCreate != null)
                {
                    var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                    propertyPersianCreate.SetValue(entity, dt);
                }

                newEntities = newEntities.Append(entity);
            }

            return newEntities;
        }
        private User SetModifiedTime(User entity)
        {
            var nowDT = DateTime.Now;
            var propertyCreate = entity.GetType().GetProperty("modifiedDateTime");
            if (propertyCreate != null)
            {
                propertyCreate.SetValue(entity, nowDT);
            }

            var propertyPersianCreate = entity.GetType().GetProperty("modifiedPersianDateTime");
            if (propertyPersianCreate != null)
            {
                var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                propertyPersianCreate.SetValue(entity, dt);
            }

            return entity;
        }
        private IEnumerable<User> SetModifiedTimes(IEnumerable<User> entities)
        {
            IEnumerable<User> newEntities = new List<User>();

            foreach (var entity in entities)
            {
                var nowDT = DateTime.Now;
                var propertyCreate = entity.GetType().GetProperty("modifiedDateTime");
                if (propertyCreate != null)
                {
                    propertyCreate.SetValue(entity, nowDT);
                }

                var propertyPersianCreate = entity.GetType().GetProperty("modifiedPersianDateTime");
                if (propertyPersianCreate != null)
                {
                    var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                    propertyPersianCreate.SetValue(entity, dt);
                }

                newEntities = newEntities.Append(entity);
            }

            return newEntities;
        }
        private User SetDeletedTime(User entity)
        {
            var nowDT = DateTime.Now;
            var propertyCreate = entity.GetType().GetProperty("deletedDateTime");
            if (propertyCreate != null)
            {
                propertyCreate.SetValue(entity, nowDT);
            }

            var propertyPersianCreate = entity.GetType().GetProperty("deletedPersianDateTime");
            if (propertyPersianCreate != null)
            {
                var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                propertyPersianCreate.SetValue(entity, dt);
            }

            return entity;
        }
        private IEnumerable<User> SetDeletedTimes(IEnumerable<User> entities)
        {
            IEnumerable<User> newEntities = new List<User>();

            foreach (var entity in entities)
            {
                var nowDT = DateTime.Now;
                var propertyCreate = entity.GetType().GetProperty("deletedDateTime");
                if (propertyCreate != null)
                {
                    propertyCreate.SetValue(entity, nowDT);
                }

                var propertyPersianCreate = entity.GetType().GetProperty("deletedPersianDateTime");
                if (propertyPersianCreate != null)
                {
                    var dt = $"{pc.GetYear(nowDT)}/{pc.GetMonth(nowDT)}/{pc.GetDayOfMonth(nowDT)} {nowDT.Hour}:{nowDT.Minute}:{nowDT.Second}";
                    propertyPersianCreate.SetValue(entity, dt);
                }

                newEntities = newEntities.Append(entity);
            }

            return newEntities;
        }
        private IEnumerable<User> SetOrder(IEnumerable<User> entities)
        {
            try
            {
                return entities.OrderByDescending(item => item.important)
                    .ThenByDescending(item => item.priority)
                    .ThenByDescending(item => item.userCreatedId)
                    .ThenByDescending(item => item.Id);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}
