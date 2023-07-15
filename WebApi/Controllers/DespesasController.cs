using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DespesasController : ControllerBase
    {

        private readonly IDespesaService _despesaService;
        private readonly InterfaceDespesa _interfaceDespesa;

        public DespesasController(IDespesaService despesaService, InterfaceDespesa interfaceDespesa)
        {
             _despesaService = despesaService;
            _interfaceDespesa = interfaceDespesa;
        }

        [HttpPost("api/AdicionarDespesas")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesas(Despesa despesa)
        {
            await _despesaService.AdicionarDespesa(despesa);
            return despesa;
        }

        [HttpGet("Api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string email)
        {
            return await _interfaceDespesa.ListarDespesasUsuario(email);
        }        
        
        [HttpGet("Api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _interfaceDespesa.GetEntityById(id);
        }

        [HttpPut("Api/AtualizarDespesas")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesas(Despesa despesa)
        {
            await _despesaService.AtualizarDespesa(despesa);
            return Task.FromResult(despesa);
        }

        [HttpDelete("Api/DeletarDespesa")]
        [Produces("application/json")]
        public async Task<object> DeletarDespesa(int id)
        {

            try
            {
                var despesa = await _interfaceDespesa.GetEntityById(id);

                await _interfaceDespesa.Delete(despesa);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpGet("Api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string email)
        {
            return await _despesaService.CarregaGraficos(email);
        }

    }
}
