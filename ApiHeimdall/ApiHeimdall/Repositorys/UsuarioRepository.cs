using ApiHeimdall.Data;
using ApiHeimdall.Interfaces;
using ApiHeimdall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private AplicationContext  aplicationContext;
        public UsuarioRepository(AplicationContext _aplicationContext)
        {
            aplicationContext = _aplicationContext;
        }
        public Usuario BuscarPorEmail(string email)
        {
           return aplicationContext.usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }
    }
}
