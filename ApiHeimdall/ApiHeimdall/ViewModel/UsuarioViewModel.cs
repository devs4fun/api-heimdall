using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Chave { get; set; }
        public string NomeCompleto { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
