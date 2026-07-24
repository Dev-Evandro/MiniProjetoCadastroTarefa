using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISenhaService
    {
        string GerarHash(string senha);

        bool VerificarSenha(
            string senha,
            string senhaHash);
    }
}
