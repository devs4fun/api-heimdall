﻿using ApiHeimdall.Interfaces;
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

        [Route("api/[controller]/cadastro-usuario")]
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

        [Route("api/[controller]/login")]
        [HttpPost]
        public ActionResult Login([FromBody] Usuario usuarioLogin)
        {
            if (String.IsNullOrEmpty(usuarioLogin.UserName) && String.IsNullOrEmpty(usuarioLogin.Email) || String.IsNullOrEmpty(usuarioLogin.Senha))
            {
                return BadRequest();
            }

            Usuario usuarioBanco = _usuarioRepository.BuscarUsuario(usuarioLogin);
            if(usuarioBanco == null)
            {
                return NotFound();
            }

            if(usuarioBanco.Ativo == false)
            {
                Token novoToken = new Token();
                novoToken.CriarTokenEEnviarPorEmail(usuarioBanco.Email, _usuarioRepository, _tokenRepository);
                return BadRequest();
            }

            string chaveString = MD5Hash.Hash.Content(usuarioLogin.Email + DateTime.Now);
            usuarioBanco.Chave = chaveString;
            _usuarioRepository.Atualizar(usuarioBanco);
            

            return Ok(new {chave = chaveString });
        }
    }
}