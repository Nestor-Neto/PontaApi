using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using FluentValidation;


namespace Service.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryBase<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public ServiceBase(IRepositoryBase<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual TOutputModel Insert<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class

        {

            TEntity entity = _mapper.Map<TEntity>(obj);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }
        public virtual TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {

            TEntity entity = _mapper.Map<TEntity>(obj);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }

        public virtual bool Delete(int id)
        {

            _baseRepository.Delete(id);
            return true;

        }
        public TOutputModel SelectId<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = _baseRepository.SelectId(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public virtual IEnumerable<TOutputModel> ListAll<TOutputModel>() where TOutputModel : class
        {
            var entities = _baseRepository.ListAll();

            var outputModel = entities.Select(entity => _mapper.Map<TOutputModel>(entity));

            return outputModel;
        }

        public static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Não encontrado!");

            validator.ValidateAndThrow(obj);
        }
    }
}
