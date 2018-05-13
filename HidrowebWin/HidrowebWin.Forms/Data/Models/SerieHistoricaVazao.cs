using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Data.Models
{
    public class SerieHistoricaVazao
    {
        public string EstacaoCodigo { get; set; }
        public string NivelConsistencia { get; set; }
        public string DataHora { get; set; }
        public string MediaDiaria { get; set; }
        public string MetodoObtencaoVazoes { get; set; }
        public string Maxima { get; set; }
        public string Minima { get; set; }
        public string Media { get; set; }
        public string DiaMaxima { get; set; }
        public string DiaMinima { get; set; }
        public string MaximaStatus { get; set; }
       public string MinimaStatus { get; set; }
        public string MediaStatus { get; set; }
        public string MediaAnual { get; set; }
        public string MediaAnualStatus { get; set; }
        public string Vazao01 { get; set; }
        public string Vazao02 { get; set; }
        public string Vazao03 { get; set; }
        public string Vazao04 { get; set; }
        public string Vazao05 { get; set; }
        public string Vazao06 { get; set; }
        public string Vazao07 { get; set; }
        public string Vazao08 { get; set; }
        public string Vazao09 { get; set; }
        public string Vazao10 { get; set; }
        public string Vazao11 { get; set; }
        public string Vazao12 { get; set; }
        public string Vazao13 { get; set; }
        public string Vazao14 { get; set; }
        public string Vazao15 { get; set; }
        public string Vazao16 { get; set; }
        public string Vazao17 { get; set; }
        public string Vazao18 { get; set; }
        public string Vazao19 { get; set; }
        public string Vazao20 { get; set; }
        public string Vazao21 { get; set; }
        public string Vazao22 { get; set; }
        public string Vazao23 { get; set; }
        public string Vazao24 { get; set; }
        public string Vazao25 { get; set; }
        public string Vazao26 { get; set; }
        public string Vazao27 { get; set; }
        public string Vazao28 { get; set; }
        public string Vazao29 { get; set; }
        public string Vazao30 { get; set; }
        public string Vazao31 { get; set; }
        public string Vazao01Status { get; set; }
        public string Vazao02Status { get; set; }
        public string Vazao03Status { get; set; }
        public string Vazao04Status { get; set; }
        public string Vazao05Status { get; set; }
        public string Vazao06Status { get; set; }
        public string Vazao07Status { get; set; }
        public string Vazao08Status { get; set; }
        public string Vazao09Status { get; set; }
        public string Vazao10Status { get; set; }
        public string Vazao11Status { get; set; }
        public string Vazao12Status { get; set; }
        public string Vazao13Status { get; set; }
        public string Vazao14Status { get; set; }
        public string Vazao15Status { get; set; }
        public string Vazao16Status { get; set; }
        public string Vazao17Status { get; set; }
        public string Vazao18Status { get; set; }
        public string Vazao19Status { get; set; }
        public string Vazao20Status { get; set; }
        public string Vazao21Status { get; set; }
        public string Vazao22Status { get; set; }
        public string Vazao23Status { get; set; }
        public string Vazao24Status { get; set; }
        public string Vazao25Status { get; set; }
        public string Vazao26Status { get; set; }
        public string Vazao27Status { get; set; }
        public string Vazao28Status { get; set; }
        public string Vazao29Status { get; set; }
        public string Vazao30Status { get; set; }
        public string Vazao31Status { get; set; }
        public string DataIns { get; set; }

        #region Readonly

        public DateTime Data
        {
            get
            {
                return Convert.ToDateTime(DataHora);
            }
        }

        #region VazaoArray
        private string[] _arrayString = null;
        public string[] VazaoArray
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
                        Vazao01,
                        Vazao02,
                        Vazao03,
                        Vazao04,
                        Vazao05,
                        Vazao06,
                        Vazao07,
                        Vazao08,
                        Vazao09,
                        Vazao10,
                        Vazao11,
                        Vazao12,
                        Vazao13,
                        Vazao14,
                        Vazao15,
                        Vazao16,
                        Vazao17,
                        Vazao18,
                        Vazao19,
                        Vazao20,
                        Vazao21,
                        Vazao22,
                        Vazao23,
                        Vazao24,
                        Vazao25,
                        Vazao26,
                        Vazao27,
                        Vazao28,
                        Vazao29,
                        Vazao30,
                        Vazao31,
            };
        }
        #endregion

        #region StatusChuvasArray
        private string[] _arrayStatusString = null;
        public string[] StatusChuvasArray
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
                        Vazao01Status,
                        Vazao02Status,
                        Vazao03Status,
                        Vazao04Status,
                        Vazao05Status,
                        Vazao06Status,
                        Vazao07Status,
                        Vazao08Status,
                        Vazao09Status,
                        Vazao10Status,
                        Vazao11Status,
                        Vazao12Status,
                        Vazao13Status,
                        Vazao14Status,
                        Vazao15Status,
                        Vazao16Status,
                        Vazao17Status,
                        Vazao18Status,
                        Vazao19Status,
                        Vazao20Status,
                        Vazao21Status,
                        Vazao22Status,
                        Vazao23Status,
                        Vazao24Status,
                        Vazao25Status,
                        Vazao26Status,
                        Vazao27Status,
                        Vazao28Status,
                        Vazao29Status,
                        Vazao30Status,
                        Vazao31Status,
            };
        }
        #endregion


        #endregion
    }
}
