using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface ICategoriaService
    {
        Task AdicionarCategoria(Categoria categoria);

        Task AtualizarCategoria(Categoria categoria);

    }
}
