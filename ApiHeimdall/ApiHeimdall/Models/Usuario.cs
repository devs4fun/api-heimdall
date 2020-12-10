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

        public bool eUsuarioValido()
        {
            if (String.IsNullOrEmpty(NomeCompleto) || String.IsNullOrEmpty(UserName) ||
                String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Senha) || Senha.Count() < 8)
            {
                return false;
            }
            return true;
        }

    }
}
