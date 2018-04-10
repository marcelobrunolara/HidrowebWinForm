using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Data.Models
{
    public class EstacaoData
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public string CodigoAdicional { get; set; }
        public string NomeBacia { get; set; }
        public string NomeSubBacia { get; set; }
        public string NomeRio { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string AreaDrenagem { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Responsavel { get; set; }
        public string Operadora { get; set; }
    }
}
