using System;

namespace DespViagem.Business.Models
{
	public class Despesa : Entity
	{
		public Guid ViagemId { get; set; }
		public string Descricao { get; set; }
		public string Local { get; set; }
		public DateTime DataDespesa { get; set; }
		public decimal Valor { get; set; }
		public string Observacao { get; set; }
		
		/*EF Relation*/
		public Viagem Viagem { get; set; }
	}
}
