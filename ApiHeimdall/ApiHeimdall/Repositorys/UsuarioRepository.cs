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
        AplicationContext _aplicationcontext;
        public UsuarioRepository(AplicationContext aplicationContext)
        {
            _aplicationcontext = aplicationContext;
        }
        public Usuario Buscar(int id)
        {
            var usuario = _aplicationcontext.usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
                return usuario;
            else
                return null;
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            _aplicationcontext.usuarios.Add(novoUsuario);
            _aplicationcontext.SaveChanges();
        }

        public Usuario BuscarPorEmail(string email)
        {
            return _aplicationcontext.usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }

        public void Atualizar(Usuario usuario)
        {
            _aplicationcontext.usuarios.Update(usuario);
            _aplicationcontext.SaveChanges();
        }
    }
}
