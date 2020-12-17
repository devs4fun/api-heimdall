using ApiHeimdall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string Email { get; set; }
        public DateTime DataLimite { get; set; }

        public bool CriarTokenEEnviarPorEmail(string email, IUsuarioRepository _usuarioRepository, ITokenRepository _tokenRepository)
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
                    string mensagem = "<a href='https://localhost:44357/api/token/" + tokenString + " ' >Clique no link para ativar sua conta</a>";
                    EnviarEmail enviaremail = new EnviarEmail();
                    enviaremail.enviar(token.Email, mensagem, assunto);
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
