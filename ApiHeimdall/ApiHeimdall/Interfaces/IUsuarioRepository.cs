using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario BuscarPorEmail(string email);
    }
}
