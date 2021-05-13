using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHeimdall.Interfaces
{
    public interface IEmailService
    {
        bool Enviar(string email, string mensagem, string assunto);
    }
}
