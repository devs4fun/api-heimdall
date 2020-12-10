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
    }
}
