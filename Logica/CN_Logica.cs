using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuracion;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Logica
{
    public class CN_Logica
    {
        public string[] Jugadores { get; set; }

        public async void GetPersonajes()
        {
            await ValorantAPI.GetPersonajes();
        }
        public async void GetVersion(System.Windows.Forms.Label lbl)
        {
            string version = await ValorantAPI.GetVersion();
            if (version.Contains("release-"))
            {
                version = version.Remove(0, 8);
            }
            lbl.Text = lbl.Text + version;
        }
        public async void GetMapas(int mapa, PictureBox pctMapa, System.Windows.Forms.Label lblMapa, System.Windows.Forms.Label lblCoordenadas, System.Windows.Forms.Label lblSites)
        {
            await ValorantAPI.GetMapas();
            await cargarMapaPct(mapa, pctMapa, lblMapa, lblCoordenadas, lblSites);
        }
        public void Random()
        {
            List<CC_Personaje> personajes = new List<CC_Personaje>();
            List<string> roles = new List<string>();
            CC_Seleccionados.Seleccionados.Clear();

            for (int i = 0, len = Jugadores.Length; i < len; i++)
            {
                string rol = elegirRolRandom(roles);
                roles.Add(rol);
                personajes.Add(elegirPersonajeRandom(personajes, rol));
                CC_Seleccionados.Seleccionados.Add(new CC_Seleccionado
                {
                    Agente = personajes[i].Nombre,
                    Jugador = Jugadores[i],
                    Imagen = personajes[i].Imagen
                });
                Thread.Sleep(1);
            }
        }
        private CC_Personaje elegirPersonajeRandom(List<CC_Personaje> agentesActuales)
        {
            List<CC_Personaje> personajes = CC_Personajes.Agentes;
            personajes = personajes.Except(agentesActuales).ToList();
            Random rdm = new Random();
            int num = rdm.Next(personajes.Count);

            return personajes[num];
        }
        private CC_Personaje elegirPersonajeRandom(List<CC_Personaje> agentesActuales, string rol)
        {
            List<CC_Personaje> personajes = CC_Personajes.Agentes;
            personajes = personajes.Except(agentesActuales).ToList();
            if (rol != string.Empty)
            {
                personajes = personajes.Where(p => p.Rol == rol).ToList();
            }

            Random rdm = new Random();
            int num = rdm.Next(personajes.Count);

            return personajes[num];
        }

        private string elegirRolRandom(List<string> rolesActuales)
        {
            List<string> roles = CC_Roles.Roles();
            roles = roles.Except(rolesActuales).ToList();

            Random rdm = new Random();
            int num = rdm.Next(roles.Count);

            return roles[num];
        }
        public async Task cargarMapaPct(int mapa, PictureBox pctMapa, System.Windows.Forms.Label lblMapa, System.Windows.Forms.Label lblCoordenadas, System.Windows.Forms.Label lblSites)
        {
            pctMapa.Load(CC_Mapas.Mapas.ElementAt(mapa).Imagen);
            lblMapa.Text = CC_Mapas.Mapas.ElementAt(mapa).Nombre;
            lblCoordenadas.Text = CC_Mapas.Mapas.ElementAt(mapa).Coordenadas;
            lblSites.Text = CC_Mapas.Mapas.ElementAt(mapa).Sites;
        }
    }
}
