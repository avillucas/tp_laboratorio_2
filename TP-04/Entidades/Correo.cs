﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{
    public class Correo : IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> paquetes;

        public List<Paquete> Paquetes {
            get {
                return paquetes;
            }
            set {
                paquetes = value;
            }        
        }

        #region Constructores
        public Correo()
        {
            paquetes = new List<Paquete>();
            mockPaquetes = new List<Thread>();
        }
        #endregion
        #region Metodos
        public void FinEntregas()
        {
            foreach (Thread hilo in mockPaquetes )
            {
                hilo.Abort();
            }
        }

        string IMostrar<List<Paquete>>.MostrarDatos(IMostrar<List<Paquete>>elementos)
        {
            string datos = "" ;
            //TODO PREGUNTAR POR QUE elementos es correo y no un listado de paquetes 
            Correo correo = (Correo)elementos;            
            foreach (Paquete p in correo.Paquetes)
            {
               datos +=  string.Format("{0} para {1} ({2})", p.TrackingID, p.DireccionEntrega, p.Estado.ToString());
            }
            return datos;
        }

        public static Correo operator +(Correo c, Paquete p)
        {
            foreach (Paquete paquete in c.paquetes)
            {
                if (paquete == p)
                {
                    throw new TrackingIdRepetidoException(string.Format("El paquete con trackingID {0} ya se encuentra en la lista de paquetes",p.TrackingID));
                }
            }
            //
            c.paquetes.Add(p);
            //
            Thread hilo = new Thread(p.MockCicloDeVida);
            c.mockPaquetes.Add(hilo);
            hilo.Start();
            //
            return c;
        }
        #endregion
    }
}