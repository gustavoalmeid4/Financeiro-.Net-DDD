using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
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
    public class RepositorioUsuarioSistemaFinanceiro : RepositoryGenerics<UsuarioSistemaFinanceiro>, InterfaceUsuarioSistemaFinanceiro
    {
        private readonly DbContextOptions _optionsBuilder;

        public RepositorioUsuarioSistemaFinanceiro()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistemaFinanceiro(int idSistema)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.UsuarioSistemaFinanceiro
                    .Where(s => s.IdSistema == idSistema)
                    .AsNoTracking()
                    .ToListAsync();
            }

        }

        public async Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.UsuarioSistemaFinanceiro
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EmailUsuario.Equals(emailUsuario));
            }
        }

        public async Task RemoverUsuarioSistemaFinanceiro(List<UsuarioSistemaFinanceiro> usuarios)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                 banco.UsuarioSistemaFinanceiro.RemoveRange(usuarios);
                await banco.SaveChangesAsync();
            }
        }
    }
}
