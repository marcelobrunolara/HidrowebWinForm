using HidrowebWin.Forms.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Data
{
    public static class ListaEstacoesCache
    {
        public static IList<EstacaoData> Estacoes { get; set; } = new List<EstacaoData>();
    }
}
