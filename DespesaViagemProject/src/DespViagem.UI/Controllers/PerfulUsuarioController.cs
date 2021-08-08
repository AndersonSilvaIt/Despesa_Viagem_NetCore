using AutoMapper;
using DespViagem.Business.Interfaces;
using DespViagem.Business.Interfaces.services;
using DespViagem.Business.Models.Gerencial;
using DespViagem.Data.Contexto;
using DespViagem.UI.Models;
using DespViagem.UI.ViewModels.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespViagem.UI.Controllers
{
    public class PerfilUsuarioController : BaseController
    {
        private readonly IPerfilUsuarioService _perfilUsuarioService;
        private readonly int pageSize = 4;
        private readonly IMapper _mapper;
        private readonly ViagemContext _context;
        public PerfilUsuarioController(IPerfilUsuarioService perfilUsuarioService, IMapper mapper,
            ViagemContext context, INotificador notificador): base(notificador)
        {
            _perfilUsuarioService = perfilUsuarioService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var lista = _mapper.Map<IEnumerable<PerfilUsuarioVM>>(await _perfilUsuarioService.Get());

            return View(PaginatedList<PerfilUsuarioVM>.Create(lista.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Export()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Description");
            var lista = _mapper.Map<IEnumerable<PerfilUsuarioVM>>(await _perfilUsuarioService.Get());

            //foreach (var item in lista)
            //{
            //    builder.AppendLine($"{item.Code};{item.Description}");
            //}

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "perfilUsuarioos.csv");
        }

        public async Task<IActionResult> Create()
        {
            var teste = await _context.Tela.AsNoTracking().ToListAsync();

            var perfilUsuario = new PerfilUsuarioVM();

            List<TelaFuncaoPerfilUsuarioVM> listScreen = new List<TelaFuncaoPerfilUsuarioVM>();

            foreach (var item in teste)
            {
                listScreen.Add(new TelaFuncaoPerfilUsuarioVM { Description = item.Description, DescriptionEN = item.Description, IdScreen = item.Id });
            }
            perfilUsuario.Telas = listScreen;

            return View(perfilUsuario);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PerfilUsuarioVM perfilUsuarioVM)
        {
            ModelState.Remove("Id");

            perfilUsuarioVM.Telas = DeserializarObjetoResponse<List<TelaFuncaoPerfilUsuarioVM>>(perfilUsuarioVM.JsonList);

            if (!ModelState.IsValid) return View(perfilUsuarioVM);

            foreach (var item in perfilUsuarioVM.Telas)
            {
                item.Description = item.DescriptionEN;
            }

            var perfilUsuario = _mapper.Map<PerfilUsuario>(perfilUsuarioVM);
            await _perfilUsuarioService.Insert(perfilUsuario);
            await _perfilUsuarioService.Commited();

            var url = Url.Action("Index", "PerfilUsuario");
            return Json(new { success = true, url });
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            var perfilUsuario = _mapper.Map<PerfilUsuarioVM>(await _perfilUsuarioService.GetByIdWithScreens(id));

            return View("Edit", perfilUsuario);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PerfilUsuarioVM perfilUsuarioVM)
        {
            perfilUsuarioVM.Telas = DeserializarObjetoResponse<List<TelaFuncaoPerfilUsuarioVM>>(perfilUsuarioVM.JsonList);

            if (!ModelState.IsValid) return View(perfilUsuarioVM);

            foreach (var item in perfilUsuarioVM.Telas)
            {
                item.Description = item.DescriptionEN;
            }

            await _perfilUsuarioService.Update(_mapper.Map<PerfilUsuario>(perfilUsuarioVM));

            await _perfilUsuarioService.Commited();
            var url = Url.Action("Index", "PerfilUsuario");
            return Json(new { success = true, url });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var measureUnit = _mapper.Map<PerfilUsuarioVM>(await _perfilUsuarioService.GetByIdWithScreens(id));

            return View("Delete", measureUnit);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PerfilUsuarioVM perfilUsuarioVM)
        {
            perfilUsuarioVM.Telas = DeserializarObjetoResponse<List<TelaFuncaoPerfilUsuarioVM>>(perfilUsuarioVM.JsonList);

            await _perfilUsuarioService.Delete(perfilUsuarioVM.Id);
            await _perfilUsuarioService.Commited();

            var url = Url.Action("Index", "PerfilUsuario");
            return Json(new { success = true, url });
        }

        [HttpGet()]
        public async Task<IActionResult> Details(int id)
        {
            var perfilUsuario = _mapper.Map<PerfilUsuarioVM>(await _perfilUsuarioService.GetByIdWithScreens(id));

            return View("Details", perfilUsuario);
        }

        public async Task<IActionResult> Import(IFormFile file)
        {
            var list = await ListImport(file);

            return RedirectToAction("Index");
        }

        private async Task<bool> ListImport(IFormFile file)
        {
            List<PerfilUsuario> listImportar = new List<PerfilUsuario>();

            if (file.FileName.EndsWith(".csv"))
            {
                PerfilUsuario perfilUsuario = new PerfilUsuario();
                string[] rows;

                using (var sreader = new StreamReader(file.OpenReadStream()))
                {
                    string[] headers = sreader.ReadLine().Split(';');     //Title
                    while (!sreader.EndOfStream)                          //get all the content in rows 
                    {
                        rows = sreader.ReadLine().Split(';');
                        //perfilUsuario.Code = rows[0];
                        perfilUsuario.Description = rows[1];

                        listImportar.Add(perfilUsuario);
                        perfilUsuario = new PerfilUsuario();
                    }
                }
            }

            //await _perfilUsuarioService.Import(listImportar);
            return true;
        }
    }
}