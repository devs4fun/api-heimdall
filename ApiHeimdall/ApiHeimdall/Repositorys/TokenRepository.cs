using ApiHeimdall.Data;
using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Repositorys
{
    public class TokenRepository : ITokenRepository
    {
        private AplicationContext aplicationContext;

        public TokenRepository(AplicationContext _aplicationContext)
        {
            aplicationContext = _aplicationContext;
        }

        public Token BuscarToken(string key)
        {
            var tokenBanco = aplicationContext.tokens.FirstOrDefault(t => t.Valor == key);
            if (tokenBanco != null)
            {
                return tokenBanco;
            }
            return null;
        }

        public void Salvar(Token token)
        {
            aplicationContext.tokens.Add(token);
            aplicationContext.SaveChanges();
        }
    }
}
