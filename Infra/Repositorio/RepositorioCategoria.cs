using Domain.Interfaces.ICategoria;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioCategoria : RepositoryGenerics<Categoria>, InterfaceCategoria
    {
        public Task<IList<Categoria>> ListarTodasCategoriasUsuario(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
