using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
    public static class CN_Jugadores
    {
        private static string filePath = "JugadoresCache.memfile";
        public static void GuardarJugadores(string[] jugadores)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string jugador in jugadores)
                {
                    writer.WriteLine(jugador);
                }
            }
        }
        public static void CargarJugadores(Form frm)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    for (int i = 1; i < 6; i++)
                    {
                        TextBox txtNombre = (TextBox)frm.Controls.Find($"txtNombre{i}", true).FirstOrDefault();
                        txtNombre.Text = reader.ReadLine();
                    }
                }
            }
        }
    }
}
