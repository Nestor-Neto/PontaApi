using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Entities;
using Domain.IServices;
using Domain.IRepositories;

namespace Service.Services
{
    public class ServiceLogin : ServiceBase<Usuario>,IServiceLogin
    {

        private readonly IRepositoryUsuario _repositoryBaseUsuario;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ServiceLogin(IRepositoryUsuario repositoryBaseUsuario, IConfiguration configuration, IMapper mapper) : base(repositoryBaseUsuario, mapper)
        {
            _repositoryBaseUsuario = repositoryBaseUsuario;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        public string Login(string login, string senha)
        {
            var usuario = _repositoryBaseUsuario.Login(login, senha);
            if(usuario!= null)
                return GerarTokenJWT(usuario);

            return "Usuário não foi encontrado ou a senha e/ou login incorretos.";
        }
        private string GerarTokenJWT(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
           
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, usuario.Login));

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(4),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
