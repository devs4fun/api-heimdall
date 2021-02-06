using ApiHeimdall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Chave { get; set; }
        public string NomeCompleto  { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario(string _NomeCompleto, string _UserName, string _Email, string _Senha)
        {
            NomeCompleto = _NomeCompleto;
            UserName = _UserName;
            Email = _Email;
            Senha = _Senha;
        }

        public bool EhValido()
        {
            if (String.IsNullOrEmpty(NomeCompleto) || String.IsNullOrEmpty(UserName) ||
                String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Senha) || Senha.Count() < 8)
            {
                return false;
            }

            CriptografarSenha();
            return true;
        }

        private void CriptografarSenha()
        {
            string criptografiaSenha = MD5Hash.Hash.Content(this.Senha);
            this.Senha = criptografiaSenha;
        }
    }
}
