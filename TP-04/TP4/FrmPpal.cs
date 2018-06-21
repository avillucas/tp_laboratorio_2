using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace MainCorreo
{
    public partial class FrmPpal : Form
    {
        private Correo correo;

        public FrmPpal()
        {
            InitializeComponent();
        }

        private void FrmPpal_Load(object sender, EventArgs e)
        {
            lstEstadoEntregado.ContextMenuStrip = cmsLista;
            correo = new Correo();
        }

        /// <summary>
        /// El método ActualizarEstados limpiará los 3 ListBox y luego recorrerá la lista de paquetes agregando
        /// cada uno de ellos en el listado que corresponda.
        /// </summary>
        private void ActualizarEstados()
        {
            lstEstadoIngresado.Items.Clear();
            lstEstadoEnViaje.Items.Clear();
            lstEstadoEntregado.Items.Clear();
            //
            foreach (Paquete paquete in correo.Paquetes)
            {
                if(paquete.Estado == Paquete.EEstado.Ingresado)
                {
                    lstEstadoIngresado.Items.Add(paquete);
                }
                else if (paquete.Estado == Paquete.EEstado.EnViaje)
                {
                    lstEstadoEnViaje.Items.Add(paquete);
                }
                else if (paquete.Estado == Paquete.EEstado.Entregado)
                {
                    lstEstadoEntregado.Items.Add(paquete);
                }
            }
            //
        }

        
        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            if (!Object.ReferenceEquals(elemento, null))
            {
                IMostrar<T> imostrarElemento = (IMostrar<T>) elemento;
                string texto = imostrarElemento.MostrarDatos(elemento);
                rtbMostrar.Text = texto;
                try
                {
                    texto.Guardar("salida.txt");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ha Ocurrido un error intentar salvar en un archivo  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void paq_InformarException(Exception exception) 
        {
            MessageBox.Show(exception.Message, "Ha Ocurrido un error intentar salvar el paquete ", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                    ActualizarEstados();        
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {            
            Paquete paquete = new Paquete(txtDireccion.Text, mtxtTrackingID.Text);
            paquete.InformaEstado += paq_InformaEstado;
            paquete.InformaException += paq_InformarException;
            try
            {
                correo += paquete;
            }
            catch (TrackingIdRepetidoException tex)
            {
                MessageBox.Show(tex.Message,"Ha Ocurrido un error al agrega el paquete",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ha Ocurrido un error desconocido al agrega el paquete", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            ActualizarEstados();

        }

        private void mostrarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            correo.FinEntregas();
        }
    }
}
