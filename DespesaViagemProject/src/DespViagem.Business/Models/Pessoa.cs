using System;

namespace DespViagem.Business.Models
{
	public class Pessoa : Entity
	{
		public string Nome { get; set; }
		public string Documento { get; set; }
		public int Idade { get; set; }
		
	}
}
