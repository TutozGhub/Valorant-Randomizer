using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{

    public class CC_Mapa
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Coordenadas { get; set; }
        public string Sites { get; set; }
    }
    public class CC_Mapas : CC_Mapa
    {
        public event Action<List<CC_Mapa>> ListaNoVacia;
        public static List<CC_Mapa> Mapas = new List<CC_Mapa>();
    }
}
