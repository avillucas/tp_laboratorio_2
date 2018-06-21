using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{   
   
    public class Paquete : IMostrar<Paquete>
    {
        private string direccionEntrega;
        private EEstado estado;
        private string trackingID;

        public enum EEstado
        {
            Ingresado,
            EnViaje,
            Entregado
        }


        public delegate void DelegadoEstado(object sender, EventArgs e);
        public delegate void DelegadoEstadoException(Exception e);        

        #region Propiedades
        public string DireccionEntrega {
            get
            {
                return direccionEntrega;
            }
            set
            {
                direccionEntrega = value;
            }
        }

        public EEstado Estado {
            get
            {
                return estado;
            }
            set
            {
                estado = value;
            }
        }
        public string TrackingID
        {
            get
            {
                return trackingID;
            }
            set
            {
                trackingID = value;
            }
        }
        #endregion

        #region Construccion
        public Paquete(string direccionDeEntrega, string trackingID)
        {
            this.direccionEntrega = direccionDeEntrega;
            this.trackingID = trackingID;
        }
        #endregion

        #region Metodos

        public void MockCicloDeVida()
        {
            bool finish = false;
            while (!finish)
            {
                Thread.Sleep(10000);
                if (estado == EEstado.Ingresado)
                {
                    estado = EEstado.EnViaje;
                }
                else if (estado == EEstado.EnViaje)
                {
                    estado = EEstado.Entregado;                 
                }
                else if (estado == EEstado.Entregado)
                {                    
                    try
                    {
                        PaqueteDAO.Insertar(this);
                    }
                    catch (Exception ex)
                    {
                        InformaException(ex);
                    }
                    finally
                    {
                        finish = true;
                    }                    
                }
                InformaEstado(this, null);
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            string st = "";
            if (elemento is Paquete)
            {
                Paquete paquete = (Paquete)elemento;
                st =  string.Format("{0} para {1}", paquete.TrackingID, paquete.DireccionEntrega);
            }
            return st;
        }

        public static bool operator ==(Paquete p1, Paquete p2)
        {
            return p1.trackingID == p2.trackingID;
        }

        public static bool operator !=(Paquete p1, Paquete p2)
        {
            return !(p1==p2);
        }
        public override string ToString()
        {
            return string.Format("{0} para {1}", TrackingID, DireccionEntrega);
        }
        #endregion

        #region Eventos
        public event DelegadoEstado InformaEstado;
        public event DelegadoEstadoException InformaException;
        #endregion
    }
}
