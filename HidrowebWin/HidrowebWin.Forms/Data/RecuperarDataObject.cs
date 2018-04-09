using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using HidrowebWin.Forms.Data.HIDRODataSetTableAdapters;

namespace HidrowebWin.Forms.Data
{
    public class BuscaDadosHelper
    {


        public static async Task<string[]> BuscarNomeEstacao(int codEstacao)
        {
            EstacaoTableAdapter estacaoAdapter = new EstacaoTableAdapter();
            IEnumerable<HIDRODataSet.EstacaoRow> estacoes = new List<HIDRODataSet.EstacaoRow>();
            await Task.Run(() => {  estacoes = estacaoAdapter.GetData().Where(c => c.Codigo == codEstacao); });
            return await Task.FromResult(estacoes.Select(c => $"{c.Codigo} - {c.Nome}").ToArray());
        }
    }
}
