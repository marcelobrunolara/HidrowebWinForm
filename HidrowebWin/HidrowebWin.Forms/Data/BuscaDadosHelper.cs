
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;
using HidrowebWin.Forms.Data.Models;
using System;

namespace HidrowebWin.Forms.Data
{
    public class BuscaDadosHelper
    {
        private static string _queryDadosEstacaoPluviometrica = @"
                        SELECT 
                        Estacao.Codigo as Codigo,
                        Estacao.Nome as Nome, 
                        Estacao.Latitude as Latitude, 
                        Estacao.Longitude as Longitude,
                        Estacao.Altitude as Altitude, 
                        Estacao.CodigoAdicional as CodigoAdicional, 
                        Bacia.Nome as NomeBacia, 
                        SubBacia.Nome as NomeSubBacia, 
                        Rio.Nome as NomeRio, 
                        Estado.Sigla as Estado, 
                        Municipio.Nome as Municipio, 
                        Estacao.AreaDrenagem as AreaDrenagem, 
                        Estacao.PeriodoPluviometroInicio as Inicio, 
                        Estacao.PeriodoPluviometroFim as Fim, 
                        Entidade.Sigla as Responsavel, 
                        EntidadeOP.Sigla as Operadora
                        FROM ((((((Estacao  LEFT JOIN Bacia ON Estacao.BaciaCodigo = Bacia.Codigo) 
                            LEFT JOIN Rio ON Estacao.RioCodigo = Rio.Codigo) 
                            LEFT JOIN SubBacia ON Estacao.SubBaciaCodigo = SubBacia.Codigo) 
                            LEFT JOIN Estado ON Estacao.EstadoCodigo = Estado.Codigo) 
                            LEFT JOIN Municipio ON Estacao.MunicipioCodigo = Municipio.Codigo) 
                            LEFT JOIN Entidade ON Estacao.ResponsavelCodigo = Entidade.Codigo) 
                            LEFT JOIN Entidade AS EntidadeOP ON Estacao.OperadoraCodigo = EntidadeOP.Codigo
                        WHERE Estacao.Codigo={0}  AND Estacao.TipoEstacao=2";

        private static string _queryDadosEstacaoFluviometrica = @"
                        SELECT 
                        Estacao.Codigo as Codigo,
                        Estacao.Nome as Nome, 
                        Estacao.Latitude as Latitude, 
                        Estacao.Longitude as Longitude,
                        Estacao.Altitude as Altitude, 
                        Estacao.CodigoAdicional as CodigoAdicional, 
                        Bacia.Nome as NomeBacia, 
                        SubBacia.Nome as NomeSubBacia, 
                        Rio.Nome as NomeRio, 
                        Estado.Sigla as Estado, 
                        Municipio.Nome as Municipio, 
                        Estacao.AreaDrenagem as AreaDrenagem, 
                        Entidade.Sigla as Responsavel, 
                        EntidadeOP.Sigla as Operadora
                        FROM ((((((Estacao  LEFT JOIN Bacia ON Estacao.BaciaCodigo = Bacia.Codigo) 
                            LEFT JOIN Rio ON Estacao.RioCodigo = Rio.Codigo) 
                            LEFT JOIN SubBacia ON Estacao.SubBaciaCodigo = SubBacia.Codigo) 
                            LEFT JOIN Estado ON Estacao.EstadoCodigo = Estado.Codigo) 
                            LEFT JOIN Municipio ON Estacao.MunicipioCodigo = Municipio.Codigo) 
                            LEFT JOIN Entidade ON Estacao.ResponsavelCodigo = Entidade.Codigo) 
                            LEFT JOIN Entidade AS EntidadeOP ON Estacao.OperadoraCodigo = EntidadeOP.Codigo
                        WHERE Estacao.Codigo={0}  AND Estacao.TipoEstacao=1";

        private static async Task<DataTable> ExecuteAsyncQuery(string query)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.HIDROConnectionString))
            {
                await connection.OpenAsync();

                OleDbCommand command = new OleDbCommand(query, connection);
                DbDataReader reader = await command.ExecuteReaderAsync();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                reader.Close();
                connection.Close();

                return dataTable;
            }
        }

        public static async Task<EstacaoData> BuscarEstacaoPluviometrica(int codEstacao)
        {
            DataTable dataTable = await ExecuteAsyncQuery(string.Format(_queryDadosEstacaoPluviometrica, codEstacao));

            EstacaoData estacaoData = new EstacaoData();

            if (dataTable.Rows.Count > 0) { 
            estacaoData.Codigo = dataTable.Rows[0].Field<int>("Codigo");
            estacaoData.Nome = dataTable.Rows[0].Field<string>("Nome");
            estacaoData.NomeBacia = dataTable.Rows[0].Field<string>("NomeBacia");
            estacaoData.NomeSubBacia = dataTable.Rows[0].Field<string>("NomeSubBacia");
            estacaoData.NomeRio = dataTable.Rows[0].Field<string>("NomeRio");
            estacaoData.Operadora = dataTable.Rows[0].Field<string>("Operadora");
            estacaoData.Municipio = dataTable.Rows[0].Field<string>("Municipio");
            estacaoData.Estado = dataTable.Rows[0].Field<string>("Estado");
            estacaoData.Responsavel = dataTable.Rows[0].Field<string>("Responsavel");
            estacaoData.Latitude = dataTable.Rows[0].Field<double>("Latitude");
            estacaoData.Longitude = dataTable.Rows[0].Field<double>("Longitude");
            estacaoData.Altitude = dataTable.Rows[0].Field<double>("Altitude");
            estacaoData.AreaDrenagem = dataTable.Rows[0].Field<string>("AreaDrenagem").ToString();
            estacaoData.Inicio = dataTable.Rows[0].Field<DateTime?>("Inicio");
            estacaoData.Fim = dataTable.Rows[0].Field<DateTime?>("Fim");
            }

            return await Task.FromResult(estacaoData);
        }

        public static async Task<EstacaoData> BuscarEstacaoFluviometrica(int codEstacao)
        {
            DataTable dataTable = await ExecuteAsyncQuery(string.Format(_queryDadosEstacaoFluviometrica, codEstacao));

            EstacaoData estacaoData = new EstacaoData();

            if (dataTable.Rows.Count > 0)
            {
                estacaoData.Codigo = dataTable.Rows[0].Field<int>("Codigo");
                estacaoData.Nome = dataTable.Rows[0].Field<string>("Nome");
                estacaoData.NomeBacia = dataTable.Rows[0].Field<string>("NomeBacia");
                estacaoData.NomeSubBacia = dataTable.Rows[0].Field<string>("NomeSubBacia");
                estacaoData.NomeRio = dataTable.Rows[0].Field<string>("NomeRio");
                estacaoData.Operadora = dataTable.Rows[0].Field<string>("Operadora");
                estacaoData.Municipio = dataTable.Rows[0].Field<string>("Municipio");
                estacaoData.Estado = dataTable.Rows[0].Field<string>("Estado");
                estacaoData.Responsavel = dataTable.Rows[0].Field<string>("Responsavel");
                estacaoData.Latitude = dataTable.Rows[0].Field<double>("Latitude");
                estacaoData.Longitude = dataTable.Rows[0].Field<double>("Longitude");
                estacaoData.Altitude = dataTable.Rows[0].Field<double>("Altitude");
                estacaoData.AreaDrenagem = dataTable.Rows[0].Field<double>("AreaDrenagem").ToString();
            }

            return await Task.FromResult(estacaoData);
        }
    }
}
