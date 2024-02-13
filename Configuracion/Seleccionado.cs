using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{
    public class CC_Seleccionado
    {
        public string Agente { get; set; }
        public string Jugador { get; set; }
        public string Imagen { get; set; }
    }
    public static class CC_Seleccionados
    {
        public static List<CC_Seleccionado> Seleccionados = new List<CC_Seleccionado>();
    }
}
