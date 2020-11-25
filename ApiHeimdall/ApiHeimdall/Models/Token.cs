using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string Email { get; set; }
        public DateTime DataLimite { get; set; }
}
}
