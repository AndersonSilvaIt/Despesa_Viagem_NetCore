using DespViagem.Business.Interfaces;
using DespViagem.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace DespViagem.UI.Controllers
{
	public abstract class BaseController : Controller
	{
		private readonly INotificador _notificador;

		public BaseController(INotificador notificador)
		{
			_notificador = notificador;
		}

		protected bool OperacaoValida()
		{
			return !_notificador.TemNotificacao();
		}

		protected void NotificarErro(string mensagem)
		{
			_notificador.Handle(new Notificacao(mensagem));
		}

	}
}