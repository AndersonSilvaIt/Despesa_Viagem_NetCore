using AutoMapper;
using DespViagem.Business.Models;
using DespViagem.Business.Models.Gerencial;
using DespViagem.UI.ViewModels;
using DespViagem.UI.ViewModels.App;

namespace DespViagem.UI.AutoMapper
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<Viagem, ViagemViewModel>().ReverseMap();
			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
			CreateMap<Despesa, DespesaViewModel>().ReverseMap();
			CreateMap<PerfilUsuario, PerfilUsuarioVM>().ReverseMap();
			CreateMap<DVUser, DVUserVM>().ReverseMap();
			CreateMap<TelaFuncaoPerfilUsuario, TelaFuncaoPerfilUsuarioVM>().ReverseMap();
		}
	}
}
