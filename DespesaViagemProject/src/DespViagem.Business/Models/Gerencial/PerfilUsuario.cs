using System.Collections.Generic;

namespace DespViagem.Business.Models.Gerencial
{
    public class PerfilUsuario : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }

        // 1 UserPerfil relaciona com N ScreensPerfil
        public IEnumerable<TelaFuncaoPerfilUsuario> Telas { get; set; }

        //public PWUser PWUser { get; set; }

        public IEnumerable<DVUser> Usuarios { get; set; }
    }
}
