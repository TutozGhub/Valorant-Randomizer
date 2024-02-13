using Vista.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;
using Configuracion;
using System.Threading;

namespace Vista
{
    public partial class frmMenu : Form
    {
        CN_Logica logica = new CN_Logica();
        int mapa = 0;
        public frmMenu()
        {
            InitializeComponent();
            logica.GetVersion(lblVersion);
            logica.GetPersonajes();
            logica.GetMapas(mapa, pctMapa, lblMapa, lblCoordenadas, lblSites);
            tmrMapas.Start();
            CN_Jugadores.CargarJugadores(this);
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            clear(this);
            List<string> nombres = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtNombre1.Text))
            {
                nombres.Add(txtNombre1.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtNombre2.Text))
            {
                nombres.Add(txtNombre2.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtNombre3.Text))
            {
                nombres.Add(txtNombre3.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtNombre4.Text))
            {
                nombres.Add(txtNombre4.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtNombre5.Text))
            {
                nombres.Add(txtNombre5.Text);
            }
            logica.Jugadores = nombres.ToArray();
            CN_Jugadores.GuardarJugadores(nombres.ToArray());
            logica.Random();
            int i = 1; // Variable para mantener un seguimiento de qué número de lblOutput estás usando

            foreach (CC_Seleccionado item in CC_Seleccionados.Seleccionados)
            {
                Label lblOutput = (Label)this.Controls.Find($"lblOutput{i}", true).FirstOrDefault();
                PictureBox pctImagen = (PictureBox)this.Controls.Find($"pctImagen{i}", true).FirstOrDefault();
                pctImagen.SizeMode = PictureBoxSizeMode.Zoom;

                if (lblOutput != null)
                {
                    lblOutput.Text = $"{item.Jugador} va a jugar {item.Agente}";
                    pctImagen.Load(item.Imagen);
                }

                i++;
            }
        }
        private void clear(Control frm)
        {
            foreach (Control c in frm.Controls)
            {
                if (c is Label lbl && c.AccessibleDescription == "agentes")
                {
                    lbl.Text = string.Empty;
                    lbl.Visible = true;
                }
                if (c is PictureBox pct && c.AccessibleDescription == "agentes")
                {
                    pct.Image = null;
                    pct.Visible = true;
                }
                if (c is GroupBox)
                {
                    clear(c);
                }
            }
        }

        private void tmrMapas_Tick(object sender, EventArgs e) 
        {
            logica.cargarMapaPct(mapa, pctMapa, lblMapa, lblCoordenadas, lblSites);
            if (mapa + 1 == CC_Mapas.Mapas.Count)
            {
                mapa = 0;
            }
            else
            {
                mapa++;
            }
        }
    }
}
