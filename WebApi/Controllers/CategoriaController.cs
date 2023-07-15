using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {

        private readonly InterfaceCategoria _interfaceCategoria;

        private readonly ICategoriaService _categoriaService;
        public CategoriaController(InterfaceCategoria interfaceCategoria, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _interfaceCategoria = interfaceCategoria;
        }


        [HttpPost("Api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            await _categoriaService.AdicionarCategoria(categoria);
            return categoria;
        }


        [HttpPut("Api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Categoria categoria)
        {
            await _categoriaService.AtualizarCategoria(categoria);
            return Task.FromResult(categoria);
        }

        [HttpGet("Api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string email)
        {
            return await _interfaceCategoria.ListarCategoriasUsuario(email);
        }       
        
        [HttpGet("Api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _interfaceCategoria.GetEntityById(id);
        }



        [HttpDelete("Api/DeletarCategoria")]
        [Produces("application/json")]
        public async Task<object> DeletarCategoria(int id)
        {

            try
            {
                var categoria = await _interfaceCategoria.GetEntityById(id);

                await _interfaceCategoria.Delete(categoria);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
