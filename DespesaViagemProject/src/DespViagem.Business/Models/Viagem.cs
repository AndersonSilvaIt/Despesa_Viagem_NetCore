using System.Collections.Generic;

namespace DespViagem.Business.Models
{
	public class Viagem : Entity
	{

		public string Descricao { get; set; }
		public string Cliente { get; set; }
		public Endereco Endereco { get; set; }

		/* EF Relation 1 Viaggem tem N Despesas */
		public IEnumerable<Despesa> Despesas { get; set; }
	}
}
