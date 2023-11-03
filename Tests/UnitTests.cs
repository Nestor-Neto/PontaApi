using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enumerator;
using Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Moq;
using Service.Services;
using Servico.Validadores;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void ShouldinsertTask()
        {
            var mapperMock = new Mock<IMapper>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var repositoryTaskMock = new Mock<IRepositoryBase<Tarefas>>();
            var repositoryUsersMock = new Mock<IRepositoryUsuario>();

            var userLogged = new Usuario { Id = 1, Nome = "Nestor", Login = "nestor.neto", Senha = "123" };

            var Tarefas = new InsertTaskModel
            {
                Titulo = "Desenvolvimento tarefa 01",
                Descricao = "Tarefa 01 fazer cadastro",
                Data = DateTime.UtcNow,
                Status = 1
            };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(userLogged.Login);
            repositoryUsersMock.Setup(x => x.SelectText(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { userLogged });

            var ServiceTarefas = new ServiceTarefas(repositoryTaskMock.Object, mapperMock.Object, httpContextAccessorMock.Object, repositoryUsersMock.Object);

            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas { Titulo = "Desenvolvimento tarefa 01", Descricao = "Tarefa 01 fazer cadastro", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario = 1, Id = 1 });

            mapperMock.Setup(x => x.Map<Tarefas>(It.IsAny<InsertTaskModel>()))
                      .Returns(new Tarefas { IdUsuario = userLogged.Id, Titulo = Tarefas.Titulo, Descricao = Tarefas.Descricao, Data = Tarefas.Data, Status = Status.Pendente });

            mapperMock.Setup(x => x.Map<Tarefas>(It.IsAny<InsertTaskModel>()))
                     .Returns(new Tarefas { IdUsuario = userLogged.Id, Titulo = Tarefas.Titulo, Descricao = Tarefas.Descricao, Data = Tarefas.Data, Status = Status.Pendente });

            var modelo = new InsertTaskModel
            {
                Titulo = "Desenvolvimento tarefa 01",
                Descricao = "Tarefa 01 fazer cadastro",
                Data = DateTime.UtcNow,
                Status = 1
            };

            var result = ServiceTarefas.Insert<InsertTaskModel, Tarefas, ValidatorTarefas>(modelo);

            Assert.IsNotNull(result);
            Assert.AreEqual(userLogged.Id, result.IdUsuario);
        }
        [Test, Order(2)]
        public void ShouldDeleteTask()
        {
            var mapperMock = new Mock<IMapper>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var repositoryTaskMock = new Mock<IRepositoryBase<Tarefas>>();
            var repositoryUsersMock = new Mock<IRepositoryUsuario>();

            var userLogged = new Usuario { Id = 1, Nome = "Nestor", Login = "nestor.neto", Senha = "123" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(userLogged.Login);
            repositoryUsersMock.Setup(x => x.SelectText(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { userLogged });

            var ServiceTarefas = new ServiceTarefas(repositoryTaskMock.Object, mapperMock.Object, httpContextAccessorMock.Object, repositoryUsersMock.Object);

            repositoryTaskMock.Setup(x => x.SelectId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Nestor",
                                              Login = "nestor.neto"
                                          },
                                          Titulo = "Desenvolvimento tarefa 05",
                                          Descricao = "Fazer cadastro da Tarefa 05",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });

            var result = ServiceTarefas.Delete(1);
            Assert.IsTrue(result, "O result deve ser verdadeiro.");
        }

        [Test, Order(3)]
        public void ShouldAlterTask()
        {
            var mapperMock = new Mock<IMapper>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var repositoryTaskMock = new Mock<IRepositoryBase<Tarefas>>();
            var repositoryUsersMock = new Mock<IRepositoryUsuario>();

            var userLogged = new Usuario { Id = 1, Nome = "Nestor", Login = "nestor.neto", Senha = "123" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(userLogged.Login);
            repositoryUsersMock.Setup(x => x.SelectText(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { userLogged });
            var ServiceTarefas = new ServiceTarefas(repositoryTaskMock.Object, mapperMock.Object, httpContextAccessorMock.Object, repositoryUsersMock.Object);

            repositoryTaskMock.Setup(x => x.SelectId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Nestor",
                                              Login = "nestor.neto"
                                          },
                                          Titulo = "Desenvolvimento tarefa 01",
                                          Descricao = "Tarefa 01 fazer cadastro",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<AlterTaskModel>()))
                .Returns((AlterTaskModel input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = userLogged.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas { Titulo = "Desenvolvimento tarefa 01", Descricao = "Fazer Atualização da tarefa", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario = 1, Id = 1 });
            var modelo = new AlterTaskModel
            {
                Id = 1,
                Titulo = "Desenvolvimento tarefa 01",
                Descricao = "Fazer Atualização da tarefa",
                Data = DateTime.UtcNow,
                Status = 1
            };

            var result = ServiceTarefas.Update<AlterTaskModel, Tarefas, ValidatorTarefas>(modelo);

            Assert.IsNotNull(result, "Não pode ser nulo");
            Assert.AreEqual("Fazer Atualização da tarefa", result.Descricao, "Não foi atualizada!");
            //Assert.AreEqual("Descrição atualizada", resultado.Descricao, "A descrição não foi atualizada corretamente");
        }

        [Test, Order(4)]
        public void AlterWithUserDifferent()
        {
            var mapperMock = new Mock<IMapper>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var repositoryTaskMock = new Mock<IRepositoryBase<Tarefas>>();
            var repositoryUsersMock = new Mock<IRepositoryUsuario>();
            var userLogged = new Usuario { Id = 1, Nome = "teste", Login = "teste.teste", Senha = "123" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(userLogged.Login);
            repositoryUsersMock.Setup(x => x.SelectText(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { userLogged });

            var ServiceTarefas = new ServiceTarefas(repositoryTaskMock.Object, mapperMock.Object, httpContextAccessorMock.Object, repositoryUsersMock.Object);

            repositoryTaskMock.Setup(x => x.SelectId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Nestor",
                                              Login = "nestor.neto"
                                          },
                                          Titulo = "Desenvolvimento tarefa 01",
                                          Descricao = "Tarefa 01 fazer cadastro",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<AlterTaskModel>()))
                .Returns((AlterTaskModel input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = userLogged.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<Tarefas>()))
                .Returns((Tarefas input) => new Tarefas { Titulo = "Desenvolvimento tarefa 01", Descricao = "Fazer Alteração da tarefa na tarefa 01 para 02", Data = DateTime.UtcNow, Status = Status.Pendente, IdUsuario = 3, Id = 3 });
            var modelo = new AlterTaskModel
            {
                Id = 1,
                Titulo = "Desenvolvimento tarefa 02",
                Descricao = "Fazer Alteração da tarefa na tarefa 01 para 02",
                Data = DateTime.UtcNow,
                Status = 1
            };

            try
            {
                var result = ServiceTarefas.Update<AlterTaskModel, Tarefas, ValidatorTarefas>(modelo);
                Assert.Fail("Error, result esperado não foi alcançado.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("O Usuário não tem permissão para Alterar esta tarefa!", ex.Message);
            }
        }

        [Test, Order(5)]
        public void DeleteWithUserDifferent()
        {
            var mapperMock = new Mock<IMapper>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var repositoryTaskMock = new Mock<IRepositoryBase<Tarefas>>();
            var repositoryUsersMock = new Mock<IRepositoryUsuario>();

            var userLogged = new Usuario { Id = 1, Nome = "teste", Login = "teste.teste", Senha = "123" };

            httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns(userLogged.Login);
            repositoryUsersMock.Setup(x => x.SelectText(It.IsAny<Dictionary<string, string>>()))
                                      .Returns(new[] { userLogged });

            var ServiceTarefas = new ServiceTarefas(repositoryTaskMock.Object, mapperMock.Object, httpContextAccessorMock.Object, repositoryUsersMock.Object);

            repositoryTaskMock.Setup(x => x.SelectId(It.IsAny<int>()))
                                      .Returns((int id) => new Tarefas
                                      {
                                          Id = id,
                                          Usuario = new Usuario
                                          {
                                              Id = 1,
                                              Nome = "Nestor",
                                              Login = "nestor.neto"
                                          },
                                          Titulo = "Desenvolvimento tarefa 01",
                                          Descricao = "Tarefa 01 fazer cadastro",
                                          Data = DateTime.UtcNow,
                                          Status = Status.Pendente
                                      });
            mapperMock
                .Setup(x => x.Map<Tarefas>(It.IsAny<AlterTaskModel>()))
                .Returns((AlterTaskModel input) => new Tarefas
                {
                    Id = input.Id,
                    IdUsuario = userLogged.Id,
                    Titulo = input.Titulo,
                    Descricao = input.Descricao,
                    Data = input.Data,
                    Status = Status.Pendente
                });

            try
            {
                var result = ServiceTarefas.Delete(1);
                Assert.Fail("Error, result esperado não foi lançado.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Sem premissão para excluir a tarefa!", ex.Message);
            }
        }

        

    }
}