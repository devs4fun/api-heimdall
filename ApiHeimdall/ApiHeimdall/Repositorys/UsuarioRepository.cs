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
        public Usuario BuscarPorEmail(string email)
        {
            return _aplicationcontext.usuarios.FirstOrDefault(usuario => usuario.Email == email);
        }
        public Usuario BuscarPorUserName(string username)
        {
            return _aplicationcontext.usuarios.FirstOrDefault(usuario => usuario.UserName == username);
        }
        public Usuario BuscarUsuario(Usuario usuario)
        {
            var usuarioBanco = _aplicationcontext.usuarios.FirstOrDefault(u => u.Email == usuario.Email || u.UserName == usuario.UserName);
            if(usuarioBanco != null)
            {
                if (usuario.Senha == usuarioBanco.Senha && usuario.Email == usuarioBanco.Email)
                {
                     return usuarioBanco;
                }
                if (usuario.Senha == usuarioBanco.Senha && usuario.UserName == usuarioBanco.UserName)
                {
                    return usuarioBanco;
                }
            }
            return null;
        }
        public void Cadastrar(Usuario novoUsuario)
        {
            _aplicationcontext.usuarios.Add(novoUsuario);
            _aplicationcontext.SaveChanges();
        }
        public void Atualizar(Usuario usuario)
        {
            _aplicationcontext.usuarios.Update(usuario);
            _aplicationcontext.SaveChanges();
        }

        public Usuario BuscarChave(string chave)
        {
           var usuario =_aplicationcontext.usuarios.FirstOrDefault(u => u.Chave == chave);
            return usuario;
        }
    }
}
