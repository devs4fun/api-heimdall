using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using ApiHeimdall.RequestResponse;
using AutoMapper;
using System;

namespace ApiHeimdall.Service
{
    public class UsuarioService : IUsuarioService
    {
        readonly IUsuarioRepository _usuarioRepository;
        readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }
        public bool Cadastrar(UsuarioRequest usuarioRequest)
        {
            var configure = new MapperConfiguration(user =>
            {
                user.CreateMap<UsuarioRequest, Usuario>();
            });

            var mapper = configure.CreateMapper();

            Usuario usuario = mapper.Map<Usuario>(usuarioRequest);

            //Usuario usuario = new Usuario()
            //{
            //    NomeCompleto = usuarioRequest.NomeCompleto,
            //    Email = usuarioRequest.Email,
            //    Senha = usuarioRequest.Senha,
            //    UserName = usuarioRequest.UserName
            //};

            if (usuario.EhValido())
            {
                
                if(this.BuscarPorEmail(usuario.Email) == null) 
                {
                    this.CriptografarSenha(usuario);
                    _usuarioRepository.Cadastrar(usuario);
                    _tokenService.Criar(usuario.Email);
                    return true;
                }
            }

            return false;
        }

        private void CriptografarSenha(Usuario usuario)
        {
            usuario.Senha = MD5Hash.Hash.Content(usuario.Senha);
        }

        private object BuscarPorEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return _usuarioRepository.BuscarPorEmail(email);
            }

            return null;
        }
    }
}
