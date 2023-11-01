
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        bool Delete(int entities);
        IList<TEntity> ListAll();
        TEntity SelectId(int id);
        IList<TEntity> SelectText(Dictionary<string, string> campoDados);
    }
}
