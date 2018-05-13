using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Data.Models
{
    public class SerieHistoricaCotas
    {

            public string EstacaoCodigo { get; set; }
            public string NivelConsistencia { get; set; }
            public string DataHora { get; set; }
            public string MediaDiaria { get; set; }
            public string TipoMedicaoCotas { get; set; }
            public string Maxima { get; set; }
            public string Minima { get; set; }
            public string Media { get; set; }
            public string DiaMaxima { get; set; }
            public string DiaMinima { get; set; }
            public string MaximaStatus { get; set; }
            public string MinimaStatus { get; set; }
            public string MediaStatus { get; set; }
            public string MediaAnualStatus { get; set; }
            public string Cota01 { get; set; }
            public string Cota02 { get; set; }
            public string Cota03 { get; set; }
            public string Cota04 { get; set; }
            public string Cota05 { get; set; }
            public string Cota06 { get; set; }
            public string Cota07 { get; set; }
            public string Cota08 { get; set; }
            public string Cota09 { get; set; }
            public string Cota10 { get; set; }
            public string Cota11 { get; set; }
            public string Cota12 { get; set; }
            public string Cota13 { get; set; }
            public string Cota14 { get; set; }
            public string Cota15 { get; set; }
            public string Cota16 { get; set; }
            public string Cota17 { get; set; }
            public string Cota18 { get; set; }
            public string Cota19 { get; set; }
            public string Cota20 { get; set; }
            public string Cota21 { get; set; }
            public string Cota22 { get; set; }
            public string Cota23 { get; set; }
            public string Cota24 { get; set; }
            public string Cota25 { get; set; }
            public string Cota26 { get; set; }
            public string Cota27 { get; set; }
            public string Cota28 { get; set; }
            public string Cota29 { get; set; }
            public string Cota30 { get; set; }
            public string Cota31 { get; set; }
            public string Cota01Status { get; set; }
            public string Cota02Status { get; set; }
            public string Cota03Status { get; set; }
            public string Cota04Status { get; set; }
            public string Cota05Status { get; set; }
            public string Cota06Status { get; set; }
            public string Cota07Status { get; set; }
            public string Cota08Status { get; set; }
            public string Cota09Status { get; set; }
            public string Cota10Status { get; set; }
            public string Cota11Status { get; set; }
            public string Cota12Status { get; set; }
            public string Cota13Status { get; set; }
            public string Cota14Status { get; set; }
            public string Cota15Status { get; set; }
            public string Cota16Status { get; set; }
            public string Cota17Status { get; set; }
            public string Cota18Status { get; set; }
            public string Cota19Status { get; set; }
            public string Cota20Status { get; set; }
            public string Cota21Status { get; set; }
            public string Cota22Status { get; set; }
            public string Cota23Status { get; set; }
            public string Cota24Status { get; set; }
            public string Cota25Status { get; set; }
            public string Cota26Status { get; set; }
            public string Cota27Status { get; set; }
            public string Cota28Status { get; set; }
            public string Cota29Status { get; set; }
            public string Cota30Status { get; set; }
            public string Cota31Status { get; set; }
            public string DataIns { get; set; }

            #region Readonly

            public DateTime Data
            {
                get
                {
                    return Convert.ToDateTime(DataHora);
                }
            }

            #region Cotas Array
            private string[] _arrayString = null;
            public string[] CotasArray
            {
                get
                {
                    if (_arrayString == null)
                        return _inicializaArrayAuxiliar();

                    return _arrayString;
                }
            }

            private string[] _inicializaArrayAuxiliar()
            {
                return new[] {
                        "",
                        Cota01,
                        Cota02,
                        Cota03,
                        Cota04,
                        Cota05,
                        Cota06,
                        Cota07,
                        Cota08,
                        Cota09,
                        Cota10,
                        Cota11,
                        Cota12,
                        Cota13,
                        Cota14,
                        Cota15,
                        Cota16,
                        Cota17,
                        Cota18,
                        Cota19,
                        Cota20,
                        Cota21,
                        Cota22,
                        Cota23,
                        Cota24,
                        Cota25,
                        Cota26,
                        Cota27,
                        Cota28,
                        Cota29,
                        Cota30,
                        Cota31,
            };
            }
            #endregion

            #region StatusChuvasArray
            private string[] _arrayStatusString = null;
            public string[] StatusCotasArray
            {
                get
                {
                    if (_arrayStatusString == null)
                        return _inicializaArrayStatusAuxiliar();

                    return _arrayStatusString;
                }
            }

            private string[] _inicializaArrayStatusAuxiliar()
            {
                return new[] {
                        "",
                        Cota01Status,
                        Cota02Status,
                        Cota03Status,
                        Cota04Status,
                        Cota05Status,
                        Cota06Status,
                        Cota07Status,
                        Cota08Status,
                        Cota09Status,
                        Cota10Status,
                        Cota11Status,
                        Cota12Status,
                        Cota13Status,
                        Cota14Status,
                        Cota15Status,
                        Cota16Status,
                        Cota17Status,
                        Cota18Status,
                        Cota19Status,
                        Cota20Status,
                        Cota21Status,
                        Cota22Status,
                        Cota23Status,
                        Cota24Status,
                        Cota25Status,
                        Cota26Status,
                        Cota27Status,
                        Cota28Status,
                        Cota29Status,
                        Cota30Status,
                        Cota31Status,
            };
            }
            #endregion


            #endregion

        

    }
}

