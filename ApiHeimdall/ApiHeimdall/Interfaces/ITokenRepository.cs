using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface ITokenRepository
    {
        void Salvar(Token token);
        Token BuscarToken(string key);
    }
}
