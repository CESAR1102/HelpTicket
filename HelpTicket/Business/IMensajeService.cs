using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IMensajeService:ICrudService<Mensaje>
    {
        bool EnviarMensaje(Mensaje msm);
        bool MensajeSolFinTicket(string de, string contenido, string cod_Atencion);
    }
}
