using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Database
{
    public static class SQLServerClass
    {
        public static string GetConnectionString()
        {
            return Configuracao.StringConexao;
        }
    }
}
