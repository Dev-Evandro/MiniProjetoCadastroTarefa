using Infrastruture.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ConfiguracaoService
    {
        public void Carregar()
        {
            Configuracao.Carregar();
        }

        public string ObterStringConexao()
        {
            return Configuracao.StringConexao;
        }
    }
}
