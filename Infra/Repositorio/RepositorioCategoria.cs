using Domain.Interfaces.ICategoria;
using Entities.Entidades;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioCategoria : RepositoryGenerics<Categoria>, InterfaceCategoria
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositorioCategoria()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<IList<Categoria>> ListarCategoriasUsuario(string emailUsuario)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await
                                  (from s in banco.SistemaFinanceiro
                                   join c in banco.Categoria on s.Id equals c.IdSistema
                                   join us in banco.UsuarioSistemaFinanceiro on s.Id equals us.IdSistema
                                   where us.EmailUsuario.Equals(emailUsuario) && us.SistemaAtual
                                   select c).AsNoTracking().ToListAsync();
            }
        }
    }
}
