using System;

namespace DespViagem.Business.Models
{
	public class Endereco : Entity
	{
		public string Logradouro { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Cep { get; set; }
		public string Bairro { get; set; }

		public string Cidade { get; set; }
		public string Estado { get; set; }

		public Guid ViagemId { get; set; }

		/*ER Relation*/
		public Viagem Viagem { get; set; }
	}
}
