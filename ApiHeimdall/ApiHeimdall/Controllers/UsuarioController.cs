using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Controllers
{
    public class UsuarioController : Controller
    {
        readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Route("api/[controller]")]
        [HttpPost]
        public ActionResult CadastroUsuario([FromBody] Usuario usuario)
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
            }
            return Ok(usuario);
        }
    }
}