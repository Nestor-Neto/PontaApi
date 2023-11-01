

using Domain.Entities;

namespace Domain.IServices
{
    public interface IServiceTarefas : IServiceBase<Tarefas>
    {
        IEnumerable<TOutputModel> SelectStatus<TOutputModel>(int? status) where TOutputModel : class;
    }
}
