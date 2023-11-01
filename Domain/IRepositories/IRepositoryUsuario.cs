

using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRepositoryUsuario : IRepositoryBase<Usuario>
    {
        Usuario Login(string username, string password);

        
    }
}
