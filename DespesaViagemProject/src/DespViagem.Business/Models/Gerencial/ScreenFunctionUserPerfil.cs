namespace DespViagem.Business.Models.Gerencial
{
    public class TelaFuncaoPerfilUsuario : Entity
    {
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public int UserPerfilId { get; set; }
        public int IdScreen { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool List { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }

        public PerfilUsuario PerfilUsuario { get; set; }
    }
}
