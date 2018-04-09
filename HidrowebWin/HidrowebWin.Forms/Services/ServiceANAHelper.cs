

using HidrowebWin.Forms.ServiceANA;
using System.Threading.Tasks;

namespace HidrowebWin.Forms.Services
{
    public class ServiceANAHelper
    {
        static ServiceANASoapClient _service = new ServiceANASoapClient();
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
    }
}
