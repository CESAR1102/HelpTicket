using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class DatosCompartidos
    {
        public string GetEstado(char estado)
        {
            string cadena;
            switch (estado)
            {
                case 'N':
                    cadena = "Inactivo";
                    break;
                case 'S':
                    cadena = "Activo";
                    break;
                case 'I':
                    cadena = "Ingresado";
                    break;
                case 'A':
                    cadena = "Aprobado";
                    break;
                case 'P':
                    cadena = "En proceso";
                    break;
                case 'F':
                    cadena = "Finalizado";
                    break;
                default:
                    cadena = "No definido";
                    break;
            }
            return cadena;
        }

        public string GetImportancia(int importancia)
        {
            string cadena;
            switch (importancia)
            {
                case 1:
                    cadena = "Alta";
                    break;
                case 2:
                    cadena = "Normal";
                    break;
                case 3:
                    cadena = "Baja";
                    break;
                default:
                    cadena = "No definido";
                    break;
            }
            return cadena;
        }
    }
}