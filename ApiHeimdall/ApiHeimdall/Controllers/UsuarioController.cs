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
                TokenController tokenController = new TokenController(_tokenRepository, _usuarioRepository);
                tokenController.Post(usuario.Email);
            }
            return Ok(usuario);
        }
    }
}