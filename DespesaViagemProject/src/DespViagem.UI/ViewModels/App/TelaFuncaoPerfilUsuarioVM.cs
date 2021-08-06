namespace DespViagem.UI.ViewModels.App
{
    // TelaFuncaoPerfilUsuarioVM
    public class TelaFuncaoPerfilUsuarioVM
    {
        //[DisplayNamePW(nameof(Description))]
        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public int Id { get; set; }
        public int UserPerfilId { get; set; }
        public int IdScreen { get; set; }

        //[DisplayNamePW(nameof(Insert))]
        public bool Insert { get; set; }

        //[DisplayNamePW(nameof(Update))]
        public bool Update { get; set; }

        //[DisplayNamePW(nameof(Delete))]
        public bool Delete { get; set; }

        //[DisplayNamePW(nameof(View))]
        public bool View { get; set; }

        //[DisplayNamePW(nameof(List))]
        public bool List { get; set; }

        //[DisplayNamePW(nameof(Import))]
        public bool Import { get; set; }

        //[DisplayNamePW(nameof(Export))]
        public bool Export { get; set; }

        public PerfilUsuarioVM PerfilUsuarioVM { get; set; }

    }
}
