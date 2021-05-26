using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate.Mapping;
using Logica.Modelos;

namespace Logica.Mapping
{
    class mapUsuarios :ClassMap<ClUsuarios>
    {
        public mapUsuarios()
        {
            Id(x => x.idUser).Column("IDUSER");
            Map(x => x.idInscrito).Column("IDINSCRITO");
            Map(x => x.Usuario).Column("USUARIO");
            Map(x => x.Password).Column("PASSWORD");
            Map(x => x.Nombres).Column("NOMBRES");
            Map(x => x.Apellidos).Column("APELLIDOS");
            Map(x => x.tipoUser).Column("TIPOUSUARIO");   
            Table("USUARIOS");

        }
    }
}
