

using HidrowebWin.Forms.ServiceANA;
using System.Net;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Services
{
    public class ServiceANAHelper
    {
        static ServiceANASoapClient _service = new ServiceANASoapClient();

        //Codigo 2 - chuva
        public static async Task<ServiceResponse> DadosPluviometricosEstacao(int codigoEstacao)
        {
            try
            {

                var dados = await _service.HidroSerieHistoricaAsync(codigoEstacao.ToString(), string.Empty, string.Empty, "2", string.Empty);
                return ServiceResponse.Criar(dados, true, string.Empty);
            }
            catch (System.Exception e)
            {
                return ServiceResponse.Criar(null, false, e.Message);
            }
        }
        //Codigo 1 - vazao
        public static async Task<ServiceResponse> DadosFluviometricosVazaoEstacao(int codigoEstacao)
        {
            try
            {

                var dados = await _service.HidroSerieHistoricaAsync(codigoEstacao.ToString(), string.Empty, string.Empty, "3", string.Empty);
                return ServiceResponse.Criar(dados, true, string.Empty);
            }
            catch (System.Exception e)
            {
                return ServiceResponse.Criar(null, false, e.Message);
            }
        }

        //Codigo 1 - cota
        public static async Task<ServiceResponse> DadosFluviometricosCotaEstacao(int codigoEstacao)
        {
            try
            {

                var dados = await _service.HidroSerieHistoricaAsync(codigoEstacao.ToString(), string.Empty, string.Empty, "1", string.Empty);
                return ServiceResponse.Criar(dados, true, string.Empty);
            }
            catch (System.Exception e)
            {
                return ServiceResponse.Criar(null, false, e.Message);
            }
        }
    }
}
