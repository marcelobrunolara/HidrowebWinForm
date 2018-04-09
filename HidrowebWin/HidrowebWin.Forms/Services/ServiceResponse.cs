using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Services
{
    public class ServiceResponse
    {
        public DataTable Dados { get; set; }
        public bool EhValido { get; set; }
        public string Mensagem { get; set; }

        private ServiceResponse (DataTable dados, bool ehValido, string mensagem)
        {
            Dados = dados;
            EhValido = ehValido;
            Mensagem = mensagem;       
        }

        public static ServiceResponse Criar(DataTable dados, bool ehValido, string mensagem)
        {
            return new ServiceResponse(dados, ehValido, mensagem);
        }
    }
}
