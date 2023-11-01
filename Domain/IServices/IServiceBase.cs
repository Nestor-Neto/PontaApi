
using Domain.Entities;
using FluentValidation;


namespace Domain.IServices
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        TOutputModel Insert<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel obj) where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
        bool Delete(int id);
        IEnumerable<TOutputModel> ListAll<TOutputModel>() where TOutputModel : class;
        TOutputModel SelectId<TOutputModel>(int id) where TOutputModel : class;
        
    }
}
