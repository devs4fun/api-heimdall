using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiHeimdall.Controllers;

namespace ApiHeimdall.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenRepository _tokenRepository;
        
        public UsuarioController(IUsuarioRepository usuarioRepository, ITokenRepository tokenRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenRepository = tokenRepository;
        }

        [Route("api/[controller]")]
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            
            if (!(usuario.eUsuarioValido()))
            {
                return BadRequest();
            }

            var uTemp = _usuarioRepository.Buscar(usuario.Id);
            if (uTemp != null)
            {
                return BadRequest();
            }
            else
            {
                _usuarioRepository.Cadastrar(usuario);
                Token token = new Token();
                token.CriarTokenEEnviarPorEmail(usuario.Email, _usuarioRepository, _tokenRepository);
            }
            return Ok(usuario);
        }

        [Route("api/[controller]/ValidarChave")]
        [HttpPost]
        public ActionResult Post([FromBody] string chave)
        {
            Usuario usuario;

            if (string.IsNullOrEmpty(chave))
            {
                return BadRequest();
            } else
            {
                usuario = _usuarioRepository.BuscarChave(chave);
                if(usuario == null)
                {
                    return BadRequest();
                }
            }

            return Ok(usuario);
        }
    }
}