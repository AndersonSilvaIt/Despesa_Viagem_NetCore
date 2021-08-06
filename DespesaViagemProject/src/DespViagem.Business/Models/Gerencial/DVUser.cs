using System;

namespace DespViagem.Business.Models.Gerencial
{
    public class DVUser : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Guid IdentityRerefenceId { get; set; }

        public int PerfilUsuarioId { get; set; }
        public PerfilUsuario PerfilUsuario { get; set; }
    }
}
