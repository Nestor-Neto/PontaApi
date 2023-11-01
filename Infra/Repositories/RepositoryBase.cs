
using Domain.Entities;
using Domain.Enumerator;
using Domain.IRepositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly PostgrSqlContext _postgrSqlContext;

        public RepositoryBase(PostgrSqlContext postgrSqlContext)
        {
            _postgrSqlContext = postgrSqlContext;
        }

        public virtual void Insert(TEntity obj)
        {
            _postgrSqlContext.Set<TEntity>().Add(obj);
            _postgrSqlContext.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            _postgrSqlContext.Entry(obj).State = EntityState.Modified;
            _postgrSqlContext.SaveChanges();
        }

        public virtual bool Delete(int id)
        {
            var obj = SelectId(id);
            if (obj == null)
                throw new Exception("Não encontrado na base de dados.");
            _postgrSqlContext.Set<TEntity>().Remove(obj);
            _postgrSqlContext.SaveChanges();
            return true;
        }

        public virtual IList<TEntity> ListAll()
        {

            return _postgrSqlContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public virtual TEntity SelectId(int id)
        {
            return _postgrSqlContext.Set<TEntity>().AsNoTracking().FirstOrDefault(prop => prop.Id == id);
        }
        public virtual IList<TEntity> SelectText(Dictionary<string, string> campoDados)
        {
            IQueryable<TEntity> query = _postgrSqlContext.Set<TEntity>().AsNoTracking();

            foreach (var item in campoDados)
            {
                var propriedade = typeof(TEntity).GetProperty(item.Key);
                if (propriedade != null)
                {
                    if (propriedade.Name == "Status" && Enum.TryParse(item.Value, out Status parsedStatus))
                        query = query.Where(prop => EF.Property<Status>(prop, propriedade.Name) == parsedStatus);
                    else
                        query = query.Where(prop => EF.Property<string>(prop, propriedade.Name).ToLower().Contains(item.Value.ToLower()));
                }
            }

            return query.ToList();
        }
    }
}
