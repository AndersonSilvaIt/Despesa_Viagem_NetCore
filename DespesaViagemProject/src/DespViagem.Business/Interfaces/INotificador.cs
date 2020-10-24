using DespViagem.Business.Notificacoes;
using System.Collections.Generic;

namespace DespViagem.Business.Interfaces
{
	public interface INotificador
	{
		bool TemNotificacao();
		List<Notificacao> ObterNotificacoes();
		void Handle(Notificacao notificacao);
	}
}
