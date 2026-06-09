using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class USUARIO
    {
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public bool Inativo { get; set; }
    }

    #region Tabela usuario
    //CREATE TABLE USUARIO
    //(
    //UsuarioId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    //Nome VARCHAR(100) NOT NULL,
    //Login VARCHAR(50) NOT NULL,
    //SenhaHash VARCHAR(255) NOT NULL,
    //DataCadastro DATETIME NOT NULL,
    //DataAtualizacao DATETIME NULL,
    //Inativo BIT NOT NULL
    //)

    #endregion
}
