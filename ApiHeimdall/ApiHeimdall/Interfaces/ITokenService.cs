using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface ITokenService
    {
        bool Criar(string email);
        string GerarValorHash(string email);
        bool Cadastrar(Token token);
    }
}
