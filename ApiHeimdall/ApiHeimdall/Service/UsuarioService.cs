using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using ApiHeimdall.RequestResponse;

namespace ApiHeimdall.Service
{
    public class UsuarioService : IUsuarioService
    {
        readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public bool Cadastrar(UsuarioRequest usuarioRequest)
        {
            Usuario usuario = new Usuario()
            {
                NomeCompleto = usuarioRequest.NomeCompleto,
                Email = usuarioRequest.Email,
                Senha = usuarioRequest.Senha,
                UserName = usuarioRequest.UserName
            };

            if (usuario.EhValido())
            {
                
                if(this.BuscarPorEmail(usuario.Email) == null) 
                {
                    _usuarioRepository.Cadastrar(usuario);
                    return true;
                }
            }

            return false;
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
