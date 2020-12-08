using AutoMapper;
using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespViagem.UI.Controllers
{
    [Authorize]
    public class ViagemController : BaseController
    {
        private static ViagemViewModel _viagemViewModel;

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
        public async Task<IActionResult> Inicial()
        {
            var viagens = _mapper.Map<IEnumerable<ViagemViewModel>>(await _viagemRepository.ObterTodos());

            return View("Index", viagens);
        }

        //[AllowAnonymous]
        [Route("dados-da-viagem/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var viagemViewModel = await ObterViagemEndereco(id);

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

            if (!OperacaoValida()) return View(await ObterViagemEndereco(id));

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
            return PartialView("_AdicionarDespesa", new ViagemViewModel());
        }

        //[ClaimsAuthorize("Fornecedor", "Atualizar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarDespesa(ViagemViewModel viagemViewModel)
        {
            ////HttpContext.Request.Form.Files
            ModelState.Remove("Cliente");
            ModelState.Remove("Descricao");
            ModelState.Remove("Id");
            ModelState.Remove("ViagemId");
            ModelState.Remove("Despesa.Id");
            ModelState.Remove("Despesa.ViagemId");
            //Despesa.Id
            if (!ModelState.IsValid)
                return PartialView("_AdicionarDespesa", viagemViewModel);

            _viagemService.PreAdicionar(_mapper.Map<Despesa>(viagemViewModel.Despesa));

            if (!OperacaoValida())
                return PartialView("_AdicionarDespesa", viagemViewModel);

            DespesaViewModel newDespesa = viagemViewModel.Despesa;
            if (_viagemViewModel == null)
            {
                _viagemViewModel = new ViagemViewModel();
                _viagemViewModel.Despesas = new List<DespesaViewModel>();
            }
            _viagemViewModel.Despesas.Add(newDespesa);

            _viagemViewModel.Despesa = new DespesaViewModel();
            viagemViewModel.Despesa = new DespesaViewModel();

            var url = Url.Action("ObterDespesas", "Viagem");
            return Json(new { success = true, url });
        }

        //[ClaimsAuthorize("viagem", "Adicionar")]
        //[Route("nova-viagem-02")]
        public async Task<IActionResult> ObterDespesas()
        {
            var viagem = new ViagemViewModel();

            return PartialView("_ListaDespesa", _viagemViewModel.Despesas);
        }

        private async Task<ViagemViewModel> ObterViagemEndereco(Guid id)
        {
            return _mapper.Map<ViagemViewModel>(await _viagemRepository.ObterViagemEndereco(id));
        }

        private async Task<ViagemViewModel> ObterViagemDespesaEndereco(Guid id)
        {
            return _mapper.Map<ViagemViewModel>(await _viagemRepository.ObterViagemEnderecoDespesa(id));
        }

    }
}