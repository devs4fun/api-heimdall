using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiHeimdall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TokenController(ITokenRepository tokenRepository, IUsuarioRepository usuarioRepository)
        {
            _tokenRepository = tokenRepository;
            _usuarioRepository = usuarioRepository;
        }

        //ENVIA E-MAIL PARA ATIVAÇÃO DO USUÁRIO
        [HttpPost]
        public IActionResult Post([FromBody] string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Usuario usuarioDoBanco = _usuarioRepository.BuscarPorEmail(email);
                if (usuarioDoBanco.Email != null)
                {
                    string tokenString = MD5Hash.Hash.Content(email + DateTime.Now);

                    Token token = new Token()
                    {
                        Valor = tokenString.ToString(),
                        Email = email,
                        DataLimite = DateTime.Now.AddDays(2)
                    };

                    _tokenRepository.Salvar(token);

                    string assunto = "Validação do cadastro";
                    string mensagem = "<a href='https://localhost:44309/api/token/" + tokenString + " ' >Clique aqui para ativar sua conta</a>";
                    EnviarEmail enviaremail = new EnviarEmail();
                    enviaremail.enviar(token.Email, mensagem, assunto);
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok("Ok");
        }

        //ATIVA O USUÁRIO POR MEIO DO LINK QUE FOI ENVIADO PARA O SEU E-MAIL
        [Route("{token}")]
        [HttpGet]
        public IActionResult Get([FromRoute] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            Token tokenDoBanco = _tokenRepository.BuscarToken(token);
            var usuarioDoBanco = _usuarioRepository.BuscarPorEmail(tokenDoBanco.Email);
           
            if(tokenDoBanco == null)
            {
                return NotFound("token não existe!");
            }
            else if(tokenDoBanco.DataLimite < DateTime.Now)
            {
                Post(tokenDoBanco.Email);
                return BadRequest("Token expirado, um novo link foi enviado para o seu e-mail!");
            }

            else if(usuarioDoBanco.Ativo == true)
            {
                return Ok("Usuário já está ativo!");
            }
           

            usuarioDoBanco.Ativo = true;
            _usuarioRepository.Atualizar(usuarioDoBanco);

            return Ok("Ok");
        }
    }
}
