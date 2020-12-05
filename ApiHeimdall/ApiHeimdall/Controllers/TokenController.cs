using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using ApiHeimdall.Services;
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
        private readonly ITokenRepository _tokenRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailSender _emailSender;

        public TokenController(ITokenRepository tokenRepository, IUsuarioRepository usuarioRepository, IEmailSender emailSender)
        {
            _tokenRepository = tokenRepository;
            _usuarioRepository = usuarioRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public IActionResult Post(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Usuario usuarioDoBanco = _usuarioRepository.BuscarPorEmail(email);
                if(usuarioDoBanco.Email != null)
                {
                    string tokenString = HashAlgorithm.Create(email + DateTime.Now).ToString();
                    Token token = new Token()
                    {
                        Valor = tokenString,
                        Email = email,
                        DataLimite = DateTime.Now.AddDays(2)
                    };
                    _tokenRepository.Salvar(token);
                    //Enviar endpoint de validação com o ww.site.com/ControllerToken/5d2002abe8906a48e94c7ff0c111f0e8
                    string assunto = "Validação do cadastro";
                    string mensagem = "Clique no link para ativar sua conta <a>localhost.com/ControllerToken/</a> " + token.Valor;
                    _emailSender.SendEmailAsync(token.Email, assunto, mensagem);
                }
            } else
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
