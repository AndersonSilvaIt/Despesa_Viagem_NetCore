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

        //[AllowAnonymous]
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

        //[ClaimsAuthorize("viagem", "Adicionar")]
        [Route("nova-viagem")]
        public IActionResult Create()
        {
            return View();
        }


        //[ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("nova-viagem")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViagemViewModel viagemViewModel)
        {
            if (!ModelState.IsValid) return View(viagemViewModel);

            viagemViewModel.Despesas = TratarCacheListaDespesa();

            var viagem = _mapper.Map<Viagem>(viagemViewModel);
            await _viagemService.Adicionar(viagem);

            if (!OperacaoValida()) return View(viagemViewModel);

            return RedirectToAction("Index");
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterViagemEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-viagem/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-viagem/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var viagemViewModel = await ObterViagemEndereco(id);

            if (viagemViewModel == null) return NotFound();

            return View(viagemViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-viagem/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var viagemViewModel = await ObterViagemEndereco(id);

            if (viagemViewModel == null)
                return NotFound();

            await _viagemService.Remover(id);

            if (!OperacaoValida()) return View(viagemViewModel);

            return RedirectToAction("Index");
        }

        //[AllowAnonymous]
        //[Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var viagem = await ObterViagemEndereco(id);
            if (viagem == null)
                return NotFound();

            return PartialView("_DetalhesEndereco", viagem); // ele vai atualizar somente a partial view _DetalhesEndereco que está no edit
        }

        //[ClaimsAuthorize("Fornecedor", "Atualizar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var viagem = await ObterViagemEndereco(id);
            if (viagem == null)
            {
                return NotFound();
            }
            return PartialView("_AtualizarEndereco", new ViagemViewModel { Endereco = viagem.Endereco });
        }

        //[ClaimsAuthorize("Fornecedor", "Atualizar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        //[ClaimsAuthorize("Fornecedor", "Atualizar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        public IActionResult AdicionarDespesa()
        {
            var result = new ViagemViewModel() { Despesa = new DespesaViewModel() };

            return PartialView("_AdicionarDespesa", result);
        }

        //[ClaimsAuthorize("Fornecedor", "Atualizar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarDespesa(ViagemViewModel viagemViewModel)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Descricao");
            ModelState.Remove("Id");
            ModelState.Remove("ViagemId");
            ModelState.Remove("Despesa.Id");
            ModelState.Remove("Despesa.ViagemId");

            if (!ModelState.IsValid)
                return PartialView("_AdicionarDespesa", viagemViewModel);

            _viagemService.PreAdicionar(_mapper.Map<Despesa>(viagemViewModel.Despesa));

            if (!OperacaoValida())
                return PartialView("_AdicionarDespesa", viagemViewModel);

            viagemViewModel.Despesas = TratarCacheListaDespesa();

            viagemViewModel.Despesas.Add(viagemViewModel.Despesa);

            TratarCacheListaDespesa(viagemViewModel.Despesas);

            TratarCacheViagemViewModel(viagemViewModel);

            var url = Url.Action("ObterDespesas", "Viagem");
            return Json(new { success = true, url });
        }

        [HttpGet()]
        public IActionResult AtualizarDespesa(Guid Id)
        {
            List<DespesaViewModel> listaDespesa = TratarCacheListaDespesa();

            DespesaViewModel despesaAtualizar = listaDespesa.First(x => x.Id == Id);

            var result = new ViagemViewModel() { Despesa = despesaAtualizar };

            return PartialView("_AdicionarDespesa", result);
        }

        //[ClaimsAuthorize("viagem", "Adicionar")]
        //[Route("nova-viagem-02")]
        public IActionResult ObterDespesas()
        {
            var lista = TratarCacheListaDespesa();

            return PartialView("_ListaDespesa", lista);
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
                if (viagem != null)
                {
                    var serialize = JsonSerializer.Serialize(viagem, null);

                    TempData["ViagemCache"] = serialize;
                    viagem = viagemCache;
                }
                else if (TempData["DespesasViagem"] != null)
                {
                    viagem = JsonSerializer.Deserialize<ViagemViewModel>(TempData["ViagemCache"].ToString(), null);

                    var serialize = JsonSerializer.Serialize(viagem, null);
                    TempData["DespesasViagem"] = serialize;
                }
            }
            catch (Exception ex) { }

            return viagem;
        }

    }
}