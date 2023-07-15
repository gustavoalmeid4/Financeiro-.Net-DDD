using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class DespesaServico : IDespesaService
    {
        private readonly InterfaceDespesa _interfaceDespesa;

        public DespesaServico(InterfaceDespesa interfaceDespesa)
        {
            _interfaceDespesa = interfaceDespesa;
        }

        public async Task AdicionarDespesa(Despesa despesa)
        {
            DateTime dataAtual = DateTime.Now;
            despesa.DataCadastro = dataAtual;
            despesa.Ano = dataAtual.Year;
            despesa.Mes = dataAtual.Month;

            bool validDespesa = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if (!validDespesa)
                await _interfaceDespesa.Add(despesa);

        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            DateTime dataAtual = DateTime.Now;
            despesa.DataAlteracao = dataAtual;

            if (despesa.Pago)
                despesa.DataPagamento = dataAtual;

            bool validDespesa = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if (!validDespesa)
                await _interfaceDespesa.Update(despesa);
        }

        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            IList<Despesa> depesasUsuario = await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
            IList<Despesa> depesasNaoPagas = await _interfaceDespesa.ListarDespesasUsuarioNaoPagasMesesAnteriores(emailUsuario);

            decimal depesasTotais = depesasNaoPagas.Any() ? depesasNaoPagas.ToList().Sum(x => x.Valor) : 0;

            decimal despesasPagas = depesasUsuario.Where(d => d.Pago && d.EnumTipoDespesa == Entities.Enums.EnumTipoDespesa.Contas).Sum(d => d.Valor); 

            decimal despesasPendentes = depesasUsuario.Where(d => !d.Pago && d.EnumTipoDespesa == Entities.Enums.EnumTipoDespesa.Contas).Sum(d => d.Valor);

            decimal investimentos = depesasUsuario.Where(d =>  d.EnumTipoDespesa == Entities.Enums.EnumTipoDespesa.Investimento).Sum(d => d.Valor);

            return new
            {
                sucesso = "Ok",
                despesasPagas = despesasPagas,
                depesasNaoPagas = depesasNaoPagas,
                despesasPendentes = despesasPendentes,
                investimentos = investimentos

            };

        }
    }
}
