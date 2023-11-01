using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class RepositoryTarefa : RepositoryBase<Tarefas>
    {
        public RepositoryTarefa(PostgrSqlContext postgrSqlContext) : base(postgrSqlContext)
        {
        }

        public override Tarefas SelectId(int id)
        {
            return _postgrSqlContext.Set<Tarefas>()
                 .Include(prop => prop.Usuario).AsNoTracking().FirstOrDefault(prop => prop.Id == id);
        }
    }
    
}
