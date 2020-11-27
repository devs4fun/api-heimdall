using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Buscar(int id);
        void Cadastrar(Usuario novoUsuario);
    }
}
