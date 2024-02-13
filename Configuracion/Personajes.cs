using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion
{
    public class CC_Personaje
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Rol { get; set; }
    }
    public class CC_Personajes : CC_Personaje
    {
        public static List<CC_Personaje> Agentes = new List<CC_Personaje>();
    }
    public class CC_Roles
    {
        private static List<string> roles = new List<string> { "Duelista", "Iniciador", "Controlador", "Centinela", "Sanador" };

        public static List<string> Roles()
        {
            return roles;
        }
    }
}
