using AutoMapper;
using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DespViagem.UI.Controllers
{
    [Authorize]
    public class ViagemController : BaseController
    {
        private readonly IViagemRepository _viagemRepository;
        private readonly IViagemService _viagemService;
        private readonly IMapper _mapper;

        public ViagemController(IViagemRepository viagemRepository,
                               IMapper mapper,
                               IViagemService viagemService, INotificador notificador) : base(notificador)
        {
            _viagemRepository = viagemRepository;
            _mapper = mapper;
            _viagemService = viagemService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var viagens = _mapper.Map<IEnumerable<ViagemViewModel>>(await _viagemRepository.ObterTodos());

            return View("Index", viagens);
        }

        [Route("dados-da-viagem/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var viagemViewModel = await ObterViagemDespesaEndereco(id);

            if (viagemViewModel == null)
            {
                return NotFound();
            }

            return View(viagemViewModel);
        }

        public IActionResult Start()
        {
            TempData["ViagemCache"] = null;

            return View("Create");
        }

        [Route("nova-viagem")]
        public IActionResult Create()
        {
            ViagemViewModel viagemViewModel = TratarCacheViagemViewModel();


            return View(viagemViewModel);
        }

        [Route("nova-viagem")]
        [HttpPost]
        public async Task<IActionResult> Create(ViagemViewModel viagemViewModel)
        {
            if (!ModelState.IsValid) return View(viagemViewModel);

            viagemViewModel.Despesas = TratarCacheListaDespesa();

            var viagem = _mapper.Map<Viagem>(viagemViewModel);
            await _viagemService.Adicionar(viagem);

            if (!OperacaoValida()) return View(viagemViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterViagemEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [Route("editar-viagem/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ViagemViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Viagem>(fornecedorViewModel);
            await _viagemService.Atualizar(fornecedor);

            if (!OperacaoValida()) return View(await ObterViagemDespesaEndereco(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-viagem/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var viagemViewModel = await ObterViagemEndereco(id);

            if (viagemViewModel == null) return NotFound();

            return View(viagemViewModel);
        }

        [Route("excluir-viagem/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var viagemViewModel = await ObterViagemEndereco(id);

            if (viagemViewModel == null)
                return NotFound();

            await _viagemService.Remover(id);

            if (!OperacaoValida()) return View(viagemViewModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var viagem = await ObterViagemEndereco(id);
            if (viagem == null)
                return NotFound();

            return PartialView("_DetalhesEndereco", viagem); // ele vai atualizar somente a partial view _DetalhesEndereco que está no edit
        }

        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var viagem = await ObterViagemEndereco(id);
            if (viagem == null)
            {
                return NotFound();
            }
            return PartialView("_AtualizarEndereco", new ViagemViewModel { Endereco = viagem.Endereco });
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(ViagemViewModel viagemViewModel)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Descricao");

            if (!ModelState.IsValid)
                return PartialView("_AtualizarEndereco", viagemViewModel);

            await _viagemService.AtualizarEndereco(_mapper.Map<Endereco>(viagemViewModel.Endereco));

            if (!OperacaoValida())
                return PartialView("_AtualizarEndereco", viagemViewModel);

            var url = Url.Action("ObterEndereco", "Viagem", new { id = viagemViewModel.Endereco.ViagemId });
            return Json(new { success = true, url });
        }

        public IActionResult AdicionarDespesa()
        {
            var result = new ViagemViewModel() { Despesa = new DespesaViewModel() };

            return PartialView("_AdicionarDespesa", result);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarDespesa(ViagemViewModel viagemViewModel)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Descricao");
            ModelState.Remove("Id");
            ModelState.Remove("ViagemId");
            ModelState.Remove("Despesa.Id");
            ModelState.Remove("Despesa.ViagemId");
            List<DespesaViewModel> listaCache = new List<DespesaViewModel>();
            if(viagemViewModel.JsonList != null)
                listaCache = DeserializarObjetoResponse<List<DespesaViewModel>>(viagemViewModel.JsonList);

            if (!ModelState.IsValid)
                return PartialView("_AdicionarDespesa", viagemViewModel);

            _viagemService.PreAdicionar(_mapper.Map<Despesa>(viagemViewModel.Despesa));

            var dtNow = DateTime.Now;
            var data = viagemViewModel.Despesa.DataCadastro;
            var dataDespesa = new DateTime(data.Year, data.Month, data.Day, dtNow.Hour, dtNow.Minute, dtNow.Second);
            
            viagemViewModel.Despesa.DataCadastro = dataDespesa;

            if (!OperacaoValida())
                return PartialView("_AdicionarDespesa", viagemViewModel);

            //viagemViewModel.Despesas = TratarCacheListaDespesa();
            viagemViewModel.Despesas.AddRange(listaCache);
            viagemViewModel.Despesas.Add(viagemViewModel.Despesa);

            TratarCacheListaDespesa(viagemViewModel.Despesas);

            TratarCacheViagemViewModel(viagemViewModel);

            //var listaDespesa = JsonSerializer.Deserialize<List<DespesaViewModel>>(TempData["DespesasViagem"].ToString(), null);
            
            var serialize = JsonSerializer.Serialize(viagemViewModel.Despesas, null);
            
            var dataSerialize = serialize;
            //var url = "";
            var url = Url.Action("ObterDespesas", "Viagem");
            return Json(new { success = true, url, data = dataSerialize });
        }

        [HttpGet()]
        public IActionResult AtualizarDespesaGet(Guid id, string descricao)
        {
            List<DespesaViewModel> listaDespesa = TratarCacheListaDespesa();

            var viagemCache = TratarCacheViagemViewModel();

            DespesaViewModel despesaAtualizar = listaDespesa.First(x => x.Id == id);

            viagemCache.Despesa = new DespesaViewModel
            {
                Id = id,
                Descricao = descricao,
                DataCadastro = despesaAtualizar.DataCadastro,
                Local = despesaAtualizar.Local,
                Valor = despesaAtualizar.Valor,
                Observacao = despesaAtualizar.Observacao
            };

            return PartialView("_AtualizarDespesa", viagemCache);
        }

        [HttpPost()]
        public IActionResult AtualizarDespesa(ViagemViewModel viagemViewModel)
        {
            List<DespesaViewModel> listaDespesa = TratarCacheListaDespesa();
            var viagemCache = TratarCacheViagemViewModel();
            
            DespesaViewModel despesaAtualizar = viagemCache.Despesas.First(x => x.Id == viagemViewModel.Despesa.Id);

            var dtNow = DateTime.Now;
            var data = viagemViewModel.Despesa.DataCadastro;
            var dataDespesa = new DateTime(data.Year, data.Month, data.Day, 
                viagemCache.Despesa.DataCadastro.Hour, 
                viagemCache.Despesa.DataCadastro.Minute, 
                viagemCache.Despesa.DataCadastro.Second);

            despesaAtualizar.Descricao = viagemViewModel.Despesa.Descricao;

            despesaAtualizar.DataCadastro = dataDespesa;

            despesaAtualizar.Local = viagemViewModel.Despesa.Local;
            despesaAtualizar.Observacao = viagemViewModel.Despesa.Observacao;
            despesaAtualizar.Valor = viagemViewModel.Despesa.Valor;

            var serialize = JsonSerializer.Serialize(viagemCache.Despesas, null);

            var dataSerialize = serialize;
            //var url = "";
            var url = Url.Action("ObterDespesas", "Viagem");
            return Json(new { success = true, url, data = dataSerialize });
        }

        private async Task<ViagemViewModel> ObterViagemEndereco(Guid id)
        {
            return _mapper.Map<ViagemViewModel>(await _viagemRepository.ObterViagemEndereco(id));
        }
        private async Task<ViagemViewModel> ObterViagemDespesaEndereco(Guid id)
        {
            return _mapper.Map<ViagemViewModel>(await _viagemRepository.ObterViagemEnderecoDespesa(id));
        }

        private List<DespesaViewModel> TratarCacheListaDespesa(List<DespesaViewModel> listaDespesaCache = null)
        {
            List<DespesaViewModel> listaDespesa = new List<DespesaViewModel>();
            try
            {
                if(listaDespesaCache != null)
                {
                    var serialize = JsonSerializer.Serialize(listaDespesaCache, null);

                    TempData["DespesasViagem"] = serialize;
                    listaDespesa = listaDespesaCache;
                }
                else if (TempData["DespesasViagem"] != null)
                {
                    listaDespesa = JsonSerializer.Deserialize<List<DespesaViewModel>>(TempData["DespesasViagem"].ToString(), null);

                    var serialize = JsonSerializer.Serialize(listaDespesa, null);
                    TempData["DespesasViagem"] = serialize;
                }
            }
            catch (Exception ex) { }

            return listaDespesa;
        }

        private ViagemViewModel TratarCacheViagemViewModel(ViagemViewModel viagemCache = null)
        {
            ViagemViewModel viagem = new ViagemViewModel();
            try
            {
                if (viagemCache != null)
                {
                    var serialize = JsonSerializer.Serialize(viagemCache, null);

                    TempData["ViagemCache"] = serialize;
                    viagem = viagemCache;
                }
                else if (TempData["ViagemCache"] != null)
                {
                    viagem = JsonSerializer.Deserialize<ViagemViewModel>(TempData["ViagemCache"].ToString(), null);

                    var serialize = JsonSerializer.Serialize(viagem, null);
                    TempData["ViagemCache"] = serialize;
                }
            }
            catch (Exception ex) { }

            return viagem;
        }
    }
}