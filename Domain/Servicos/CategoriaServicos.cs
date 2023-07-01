using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServices;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class CategoriaServicos : ICategoriaService
    {
        private readonly InterfaceCategoria _interfaceCategoria;
        public CategoriaServicos(InterfaceCategoria interfaceCategoria)
        {
            _interfaceCategoria = interfaceCategoria;
        }
        public async Task AdicionarCategoria(Categoria categoria)
        {
            bool validCategoria = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (validCategoria)
                await _interfaceCategoria.Add(categoria);
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            bool validCategoria = categoria.ValidarPropriedadeString(categoria.Nome, "Nome");
            if (validCategoria)
                await _interfaceCategoria.Update(categoria);
        }
    }
}
