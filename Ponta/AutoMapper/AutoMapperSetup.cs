using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Infra.RepositoriesBase;
using Infra.Repositorios;
using Service.Services;

namespace Application.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryBase<Tarefas>, RepositoryTarefa>();
            services.AddScoped<IServiceTarefas, ServiceTarefas>();

            services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();
            services.AddScoped<IServiceLogin, ServiceLogin>();


            return services;
        }

        public static IServiceCollection AddModels(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<EntradaUsuarioModel, Usuario>();

                config.CreateMap<EntradaTarefaModel, Tarefas>();
                config.CreateMap<AtualizacaoTarefaModel, Tarefas>();
                config.CreateMap<Tarefas, SaidaTarefaModel>();
                


            }).CreateMapper());
            
            return services;

		}

    }
}
