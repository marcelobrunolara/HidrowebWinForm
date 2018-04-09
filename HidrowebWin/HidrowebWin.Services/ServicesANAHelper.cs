using HidrowebWin.Services.ServiceANA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace HidrowebWin.Services
{
    public  class ServicesANAHelper
    {

       public static void trytostart()
        {
            ServiceANASoapClient a = new ServiceANASoapClient();
        }

    }
}
