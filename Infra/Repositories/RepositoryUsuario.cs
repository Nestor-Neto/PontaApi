using Domain.Entities;
using Domain.IRepositories;
using Infra.Context;
using Infra.Repositories;

namespace Infra.RepositoriesBase
{
    public class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
    {
        public RepositoryUsuario(PostgrSqlContext postgrSqlContext) : base(postgrSqlContext)
        {
        }

        public Usuario Login(string login,string senha)
        {
            return _postgrSqlContext.Set<Usuario>().Where(prop=> prop.Login.Equals(login) && prop.Senha.Equals(senha)).FirstOrDefault();

        }

        
    }
}
