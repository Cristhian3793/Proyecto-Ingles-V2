using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.Modelos;
namespace Logica.ModelosDAO
{
    interface LicenciaCambridgeDAO
    {
        IList<ClLicenciaCambridge> getLicenciasCambridge();
        void InsertarLicenciaCambridge(ClLicenciaCambridge licencia);
        string eliminarLicenciaCambridge(long idLicencia);
        bool actualizarLicenciaCambridge(ClLicenciaCambridge licencia, long idLicencia);
        IList<ClLicenciaCambridge> getLicenciasCambridgexLibro(long idLicencia);
    }
}
