using ApiHeimdall.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface IUsuarioService
    {
        bool Cadastrar(UsuarioRequest usuarioRequest);
    }
}
