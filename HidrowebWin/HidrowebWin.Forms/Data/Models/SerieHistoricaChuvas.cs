﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Data.Models
{
    public class SerieHistoricaChuvas
    {

        public string EstacaoCodigo { get; set; }
        public string NivelConsistencia { get; set; }
        public string DataHora { get; set; }
        public string TipoMedicaoChuvas { get; set; }
        public string Maxima { get; set; }
        public string Total { get; set; }
        public string DiaMaxima { get; set; }
        public string NumDiasDeChuva { get; set; }
        public string MaximaStatus { get; set; }
        public string TotalStatus { get; set; }
        public string NumDiasDeChuvaStatus { get; set; }
        public string TotalAnual { get; set; }
        public string TotalAnualStatus { get; set; }
        public string Chuva01 { get; set; }
        public string Chuva02 { get; set; }
        public string Chuva03 { get; set; }
        public string Chuva04 { get; set; }
        public string Chuva05 { get; set; }
        public string Chuva06 { get; set; }
        public string Chuva07 { get; set; }
        public string Chuva08 { get; set; }
        public string Chuva09 { get; set; }
        public string Chuva10 { get; set; }
        public string Chuva11 { get; set; }
        public string Chuva12 { get; set; }
        public string Chuva13 { get; set; }
        public string Chuva14 { get; set; }
        public string Chuva15 { get; set; }
        public string Chuva16 { get; set; }
        public string Chuva17 { get; set; }
        public string Chuva18 { get; set; }
        public string Chuva19 { get; set; }
        public string Chuva20 { get; set; }
        public string Chuva21 { get; set; }
        public string Chuva22 { get; set; }
        public string Chuva23 { get; set; }
        public string Chuva24 { get; set; }
        public string Chuva25 { get; set; }
        public string Chuva26 { get; set; }
        public string Chuva27 { get; set; }
        public string Chuva28 { get; set; }
        public string Chuva29 { get; set; }
        public string Chuva30 { get; set; }
        public string Chuva31 { get; set; }
        public string Chuva01Status { get; set; }
        public string Chuva02Status { get; set; }
        public string Chuva03Status { get; set; }
        public string Chuva04Status { get; set; }
        public string Chuva05Status { get; set; }
        public string Chuva06Status { get; set; }
        public string Chuva07Status { get; set; }
        public string Chuva08Status { get; set; }
        public string Chuva09Status { get; set; }
        public string Chuva10Status { get; set; }
        public string Chuva11Status { get; set; }
        public string Chuva12Status { get; set; }
        public string Chuva13Status { get; set; }
        public string Chuva14Status { get; set; }
        public string Chuva15Status { get; set; }
        public string Chuva16Status { get; set; }
        public string Chuva17Status { get; set; }
        public string Chuva18Status { get; set; }
        public string Chuva19Status { get; set; }
        public string Chuva20Status { get; set; }
        public string Chuva21Status { get; set; }
        public string Chuva22Status { get; set; }
        public string Chuva23Status { get; set; }
        public string Chuva24Status { get; set; }
        public string Chuva25Status { get; set; }
        public string Chuva26Status { get; set; }
        public string Chuva27Status { get; set; }
        public string Chuva28Status { get; set; }
        public string Chuva29Status { get; set; }
        public string Chuva30Status { get; set; }
        public string Chuva31Status { get; set; }
        public string DataIns { get; set; }

        #region Readonly

        public DateTime Data
        {
            get
            {
                return Convert.ToDateTime(DataHora);
            }
        }

        #region ChuvasArray
        private string[] _arrayString = null;
        public string[] ChuvasArray
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
                        Chuva01,
                        Chuva02,
                        Chuva03,
                        Chuva04,
                        Chuva05,
                        Chuva06,
                        Chuva07,
                        Chuva08,
                        Chuva09,
                        Chuva10,
                        Chuva11,
                        Chuva12,
                        Chuva13,
                        Chuva14,
                        Chuva15,
                        Chuva16,
                        Chuva17,
                        Chuva18,
                        Chuva19,
                        Chuva20,
                        Chuva21,
                        Chuva22,
                        Chuva23,
                        Chuva24,
                        Chuva25,
                        Chuva26,
                        Chuva27,
                        Chuva28,
                        Chuva29,
                        Chuva30,
                        Chuva31,
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
                        Chuva01Status,
                        Chuva02Status,
                        Chuva03Status,
                        Chuva04Status,
                        Chuva05Status,
                        Chuva06Status,
                        Chuva07Status,
                        Chuva08Status,
                        Chuva09Status,
                        Chuva10Status,
                        Chuva11Status,
                        Chuva12Status,
                        Chuva13Status,
                        Chuva14Status,
                        Chuva15Status,
                        Chuva16Status,
                        Chuva17Status,
                        Chuva18Status,
                        Chuva19Status,
                        Chuva20Status,
                        Chuva21Status,
                        Chuva22Status,
                        Chuva23Status,
                        Chuva24Status,
                        Chuva25Status,
                        Chuva26Status,
                        Chuva27Status,
                        Chuva28Status,
                        Chuva29Status,
                        Chuva30Status,
                        Chuva31Status,
            };
        }
        #endregion


        #endregion
    }
}
