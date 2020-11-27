using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ApiHeimdall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IUsuarioRepository usuarioRepository;
        public TokenController(ITokenRepository _tokenRepository, IUsuarioRepository _usuarioRepository)
        {
            tokenRepository = _tokenRepository;
            usuarioRepository = _usuarioRepository;
        }

        [HttpPost]
        public IActionResult Post(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Usuario usuarioDoBanco = usuarioRepository.BuscarPorEmail(email);
                if(usuarioDoBanco.Email != null)
                {
                    string tokenString = HashAlgorithm.Create(email + DateTime.Now).ToString();
                    Token token = new Token()
                    {
                        Valor = tokenString,
                        Email = email,
                        DataLimite = DateTime.Now.AddDays(2)
                    };
                    tokenRepository.Salvar(token);
                    //Enviar endpoint de validação com o ww.site.com/ControllerToken/5d2002abe8906a48e94c7ff0c111f0e8
                }
            } else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
