using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Microsoft.AspNetCore.Http;

namespace Service.Services
{
    public class ServiceTarefas : ServiceBase<Tarefas>, IServiceTarefas
    {
        private readonly IRepositoryBase<Tarefas> _repositoryBaseTarefas;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepositoryUsuario _repositoryBaseUsuario;
        public ServiceTarefas(IRepositoryBase<Tarefas> repositoryBaseTarefas, 
            IMapper mapper, IHttpContextAccessor httpContextAccessor, 
            IRepositoryUsuario repositoryBaseUsuario) 
            : base(repositoryBaseTarefas, mapper)
        {
            _repositoryBaseTarefas = repositoryBaseTarefas;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _repositoryBaseUsuario = repositoryBaseUsuario;
        }
        public override TOutputModel Insert<TInputModel, TOutputModel, TValidator>(TInputModel obj)
        {
            Tarefas entity = _mapper.Map<Tarefas>(obj);

            Validate(entity, Activator.CreateInstance<TValidator>());

            Dictionary<string, string> value = new()
            {
                { "Login", _httpContextAccessor.HttpContext.User.Identity.Name}
            };
            var userLogged = _repositoryBaseUsuario.SelectText(value).FirstOrDefault();

            entity.IdUsuario = userLogged.Id;
            Validate(entity, Activator.CreateInstance<TValidator>());
            _repositoryBaseTarefas.Insert(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public override TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel obj)
        {
            Tarefas entity = _mapper.Map<Tarefas>(obj);

            Tuple<int, string> usuarioBanco = userLogged(entity.Id);

            if (!usuarioBanco.Item2.ToUpper().Equals(_httpContextAccessor.HttpContext.User.Identity.Name.ToUpper()))
                throw new Exception("O Usuário não tem permissão para Alterar esta tarefa!");

            entity.IdUsuario = usuarioBanco.Item1;
            Validate(entity, Activator.CreateInstance<TValidator>());
            _repositoryBaseTarefas.Update(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public virtual IEnumerable<TOutputModel> SelectStatus<TOutputModel>(int? status) where TOutputModel : class
        {
            if (status == null)
                throw new Exception("O status é obrigatório.");

            if (status != 0 && status != 1 && status != 2)
                throw new Exception("Incorreto! Utilize \n 0 - Pendente \n 1 - Em Conclusão \n 2 - Concluido.");

            Dictionary<string, string> value = new ()
            {
                { "Status", status.ToString()}
            };

            var entities = _repositoryBaseTarefas.SelectText(value);
            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }
        public override bool Delete(int id)
        {

            Tuple<int, string> usuarioBanco = userLogged(id);
            if (!usuarioBanco.Item2.ToUpper().Equals(_httpContextAccessor.HttpContext.User.Identity.Name.ToUpper()))
                throw new Exception("Sem premissão para excluir a tarefa!");

            _repositoryBaseTarefas.Delete(id);
            return true;    

        }

        private Tuple<int, string> userLogged(int id)
        {
            Tarefas tarefas = _repositoryBaseTarefas.SelectId(id);
            if (tarefas == null)
                throw new Exception("Não encontrado.");

            return new Tuple<int, string>(tarefas.Usuario.Id, tarefas.Usuario.Login);
        }

    }
}
