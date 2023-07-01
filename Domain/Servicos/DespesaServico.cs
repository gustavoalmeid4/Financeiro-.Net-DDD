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
    }
}
