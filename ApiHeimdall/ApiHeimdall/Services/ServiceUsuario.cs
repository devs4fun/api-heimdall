using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Services
{
    public class ServiceUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenRepository _tokenRepository;
        public ServiceUsuario(IUsuarioRepository usuarioRepository, ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool Cadastrar(Usuario usuario)
        {
            if (!(usuario.EhValido()))
            {
                return false;
            }

            var UsuarioBuscadoNoBanco = _usuarioRepository.BuscarPorEmail(usuario.Email);

            if (UsuarioBuscadoNoBanco.UserName == usuario.UserName)
            {
                return false;
            }
            if (UsuarioBuscadoNoBanco != null)
            {
                return false;
            }
            else
            {
                _usuarioRepository.Cadastrar(usuario);

                Token token = new Token();
                token.CriarTokenEEnviarPorEmail(usuario.Email, _usuarioRepository, _tokenRepository);
            }

            return true;
        }
    }
}
