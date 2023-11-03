using Application.Models;
using Domain.Entities;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Validador;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _serviceBase;
        public LoginController(IServiceLogin serviceBase)
        {
            _serviceBase = serviceBase;
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model == null)
                return NotFound();

            var token = _serviceBase.Login(model.Login, model.Senha);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost]
        [Route("Insert")]
        [AllowAnonymous]
        public IActionResult Insert([FromBody]  InsertUserModel modelo)
        {
            if (modelo == null)
                return NotFound();

            return Execute(() => _serviceBase.Insert<InsertUserModel, Usuario, ValidatorUsuario>(modelo));
        }
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro , entre em contato com administrador do sistema!");
            }
        }
    }
}
