using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DespViagem.UI.ViewModels.App
{
    public class PerfilUsuarioVM
    {
        public int Id { get; set; }

        //[RequiredPW(nameof(Description)), DisplayNamePW(nameof(Description))]
        public string Description { get; set; }

        //[DisplayNamePW(nameof(Active))]
        public bool Active { get; set; }

        public IEnumerable<TelaFuncaoPerfilUsuarioVM> Telas { get; set; }

        public IEnumerable<DVUserVM> Usuarios { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string JsonList { get; set; }
    }
}
