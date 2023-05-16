using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
            notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string mensagem { get; set; }

        [NotMapped]
        public List<Notifica> notificacoes { get; set; }

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(nomePropriedade))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo obrigatorio",
                    NomePropriedade = nomePropriedade
                });
                return false;
            };

            return true;
        }

        public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrEmpty(nomePropriedade))
            {
                notificacoes.Add(new Notifica
                {
                    mensagem = "Campo obrigatorio",
                    NomePropriedade = nomePropriedade
                });
                return false;
            };

            return true;
        }
    }
}
