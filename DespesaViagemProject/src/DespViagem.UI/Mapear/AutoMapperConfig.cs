using AutoMapper;
using DespViagem.Business.Models;
using DespViagem.UI.ViewModels;

namespace DespViagem.UI.AutoMapper
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<Viagem, ViagemViewModel>().ReverseMap();
			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
			CreateMap<Despesa, DespesaViewModel>().ReverseMap();
		}
	}
}
