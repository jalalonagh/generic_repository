
using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext DbContext;
        private readonly List<string> skipProperties = new List<string>() { "deleted",
            "creationDateTime",
            "creationDay",
            "creationPersianDateTime",
            "modifiedDateTime",
            "modifiedDay",
            "modifiedPersianDateTime",
            "deletedDateTime",
            "deletedDay",
            "deletedPersianDateTime",
            "Id"};

        PersianCalendar pc = new PersianCalendar();

        public DbContext Database { get { return DbContext; } }

        public DbSet<TEntity> Entities { get; }
        public IQueryable<TEntity> Table => Entities;
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        #region Async Method
        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            var query = Entities.
                Where(w => w.Id == (int)ids.FirstOrDefault())
                .AsNoTracking()
                .AsQueryable();

            foreach (var property in DbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                query = query.Include(property.Name);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FetchByIdAsync(CancellationToken cancellationToken, int id)
        {
            var query = Entities.
                Where(w => w.Id == id)
                .AsNoTracking()
                .AsQueryable();

            foreach (var property in DbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                query = query.Include(property.Name);

            return await query.Take(1).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, int total = 0, int more = int.MaxValue)
        {
            var query = Entities
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .AsQueryable();

            foreach (var property in DbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                query = query.Include(property.Name);

            return SetOrder(await query
                .ToListAsync());
        }
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entity, nameof(entity));

            entity = FixPersianText(entity);

            entity = SetCreationTime(entity);

            if (IsDeletedOrNotActiveEntity(entity))
                return null;

            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            //if (saveNow)
            Result = await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entities, nameof(entities));

            entities = FixPersianText(entities);

            entities = SetCreationTimes(entities);

            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            //if (saveNow)
            Result = await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Assert.NotNull(entity, nameof(entity));

            entity = FixPersianText(entity);

            entity = SetModifiedTime(entity);

            Entities.Update(entity);
            //if (saveNow)
            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            Entities.Local.Clear();

            entities.ToList().ForEach(delegate (TEntity entity)
            {
                Assert.NotNull(entity, nameof(entity));
                Entities.Attach(entity).State = EntityState.Modified;
            });

            entities = FixPersianText(entities);

            entities = SetModifiedTimes(entities);

            Entities.UpdateRange(entities);
            //if (saveNow)
            Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return SetOrder(entities);

            return null;
        }
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
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

                //if (saveNow)
                Result = await DbContext.SaveChangesAsync();

                return entity;
            }

            //Entities.Remove(entity);
            //if (saveNow)
            //    Result = await DbContext.SaveChangesAsync(cancellationToken);

            if (Result > 0)
                return entity;

            return null;
        }
        public async Task<TEntity> DeleteByIdAsync(int id, CancellationToken cancellationToken, bool saveNow = true)
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
        public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
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
        public async Task<IEnumerable<TEntity>> DeleteRangeByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken, bool saveNow = true)
        {
            int Result = 0;

            IEnumerable<TEntity> entities = await Entities.Where(w => ids.Contains(w.Id)).ToListAsync();

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
        #endregion

        #region Attach & Detach
        public TEntity Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;

            return entity;
        }
        public TEntity Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);

            return entity;
        }
        #endregion

        #region JalalQuery
        public async Task<IEnumerable<TEntity>> FilterRangeAsync(TEntity entity, CancellationToken cancel, int total = 0, int more = int.MaxValue)
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

            var entityType = Database.Model.FindEntityType(typeof(TEntity));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var sql = $"select * from {schema}.{tableName} ";

            if (!properties.Equals(null) && properties.Count() > 0)
            {
                sql += "where ";
                properties.ForEach(delegate (PropertyInfo info)
                {
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
                //sql += ";";
            }

            var query = Entities.FromSqlRaw(sql)
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .AsQueryable();

            foreach (var property in DbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                query = query.Include(property.Name);

            IEnumerable<TEntity> data = await query
                .ToListAsync();

            return SetOrder(data);
        }
        public async Task<IEnumerable<TEntity>> SearchRangeAsync(TEntity entity, string text, CancellationToken cancel, int total = 0, int more = int.MaxValue)
        {
            var deletedProp = entity.GetType().GetProperty("deleted");
            if (deletedProp != null)
                deletedProp.SetValue(entity, null);

            var properties = entity.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(string))
                .ToList();

            var entityType = Database.Model.FindEntityType(typeof(TEntity));
            var schema = entityType.GetSchema();
            var tableName = entityType.GetTableName();
            var sql = $"select * from {schema}.{tableName} ";

            if (!properties.Equals(null) && properties.Count() > 0)
            {
                sql += "where (";
                properties.ForEach(delegate (PropertyInfo info)
                {
                    // check and deactivate default numbers like 0 into search and filter action
                    //if (info.PropertyType == typeof(string))
                    sql += $" [{info.Name}] like N'%{text}%' or";
                });

                sql = sql.Substring(0, sql.Length - 2);

                sql = sql + ") and ";

                sql += FixedSqlQueryParameters();

                sql = sql.Substring(0, sql.Length - 3);
                //sql += ";";
            }

            var query = Entities.FromSqlRaw(sql)
                .AsNoTracking()
                .Skip(total)
                .Take(more)
                .AsQueryable();

            foreach (var property in DbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
                query = query.Include(property.Name);

            IEnumerable<TEntity> data = await query
                .ToListAsync();

            return SetOrder(data);
        }
        public async Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, TEntity entity, params string[] fields)
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
                    //if (saveNow)
                    result = await DbContext.SaveChangesAsync(cancellation);

                    if (result > 0)
                        return data;

                    return null;
                }
            }
            return null;
        }
        public async Task<TEntity> UpdateFieldRangeAsync(CancellationToken cancellation, int Id, params KeyValuePair<string, dynamic>[] fields)
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
                    //if (saveNow)
                    result = await DbContext.SaveChangesAsync(cancellation);

                    if (result > 0)
                        return data;

                    return null;
                }
            }
            return null;
        }
        #endregion JalalQuery

        #region JalalTools
        public async Task<TEntity> ItemSync(TEntity Target, TEntity Origin, CancellationToken cancel)
        {
            try
            {
                var Properties = typeof(TEntity).GetProperties()
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
        private TEntity FixPersianText(TEntity entity)
        {
            var properties = entity.GetType()
                .GetProperties()
                .Where(x => x.GetValue(entity) != null && x.PropertyType == typeof(string))
                .ToList();

            properties.ForEach(delegate (PropertyInfo info)
            {
                info.SetValue(entity, info.GetValue(entity).ToString().FixPersianChars().En2Fa());
            });

            return entity;
        }
        private IEnumerable<TEntity> FixPersianText(IEnumerable<TEntity> entities)
        {
            List<TEntity> persianEntities = new List<TEntity>();
            entities.ToList().ForEach(delegate (TEntity entity)
            {
                entity = FixPersianText(entity);
                persianEntities.Add(entity);
            });
            return persianEntities;
        }
        private bool IsDeletedOrNotActiveEntity(TEntity entity)
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
        private IEnumerable<TEntity> GetUnDeleteds(IEnumerable<TEntity> entities)
        {
            List<TEntity> undeleteds = new List<TEntity>();
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
        private TEntity SetCreationTime(TEntity entity)
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
        private IEnumerable<TEntity> SetCreationTimes(IEnumerable<TEntity> entities)
        {
            IEnumerable<TEntity> newEntities = new List<TEntity>();

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
        private TEntity SetModifiedTime(TEntity entity)
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
        private IEnumerable<TEntity> SetModifiedTimes(IEnumerable<TEntity> entities)
        {
            IEnumerable<TEntity> newEntities = new List<TEntity>();

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
        private TEntity SetDeletedTime(TEntity entity)
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
        private IEnumerable<TEntity> SetDeletedTimes(IEnumerable<TEntity> entities)
        {
            IEnumerable<TEntity> newEntities = new List<TEntity>();

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
        private IEnumerable<TEntity> SetOrder(IEnumerable<TEntity> entities)
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
        #endregion JalalTools
    }
    public static class HackyDbSetGetContextTrick
    {
        public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            return (DbContext)dbSet
                .GetType().GetTypeInfo()
                .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
        }
    }
}
