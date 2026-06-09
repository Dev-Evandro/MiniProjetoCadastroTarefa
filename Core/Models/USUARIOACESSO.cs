using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class USUARIOACESSO
    {
        public Guid UsuarioAcessoId { get; set; }

        public Guid UsuarioId { get; set; }

        public string PermissoesJson { get; set; } = string.Empty;
    }

    #region Tabela USUARIOACESSO

    //CREATE TABLE USUARIOACESSO
    //(
    //    UsuarioAcessoId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    //    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    //    PermissoesJson NVARCHAR(MAX) NOT NULL,

    //    CONSTRAINT FK_USUARIOACESSO_USUARIO
    //        FOREIGN KEY (UsuarioId)
    //        REFERENCES USUARIO(UsuarioId)
    //)

    #endregion
}
