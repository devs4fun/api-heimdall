using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Service
{
    public class TokenService : ITokenService
    {
        readonly ITokenRepository _tokenRepository;
        readonly IEmailService _emailService;
        public TokenService(ITokenRepository tokenRepository, IEmailService emailService)
        {
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }
        public bool Criar(string email)
        {
            string valor = this.GerarValorHash(email);
            Token token = new Token() 
            {
                Valor = valor,
                Email = email,
                DataLimite = DateTime.Now.AddDays(2)
            };

            if (this.Cadastrar(token))
            {
                string assunto = "Validação do cadastro";
                string mensagem = "<a href='https://localhost:44357/api/token/" + valor + " ' >Clique no link para ativar sua conta</a>";

                _emailService.Enviar(email, mensagem, assunto);
                return true;
            }

            return false;
        }

        public bool Cadastrar(Token token)
        {
            if (token.EhValido())
            {
                _tokenRepository.Salvar(token);
                return true;
            }

            return false;
        }

        public string GerarValorHash(string email)
        {
            return MD5Hash.Hash.Content(email + DateTime.Now);
        }
    }
}
