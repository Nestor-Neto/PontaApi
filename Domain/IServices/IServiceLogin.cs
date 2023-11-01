
using Domain.Entities;

namespace Domain.IServices
{
    public interface IServiceLogin: IServiceBase<Usuario>
    {
        string Login(string login, string senha);

    }
}
