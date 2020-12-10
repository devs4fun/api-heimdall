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
            Token token = new Token();
            if(token.CriarTokenEEnviarPorEmail(email, _usuarioRepository, _tokenRepository) == false)
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
