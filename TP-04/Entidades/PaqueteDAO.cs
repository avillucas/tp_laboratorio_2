using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Entidades
{
    public static class PaqueteDAO
    {
        private static SqlConnection _conexion;
        private static SqlCommand _comando;

        #region Constructores
        static PaqueteDAO()        {
            _conexion = new SqlConnection(Properties.Settings.Default.Connection);
            _comando = new SqlCommand();
            _comando.CommandType = CommandType.Text;
            _comando.Connection = _conexion;
        }
        #endregion
        #region Metodos
        public static bool Insertar(Paquete p)
        {
            string sql = "INSERT INTO Paquetes (direccionEntrega,trackingID,alumno) VALUES('" + p.DireccionEntrega + "','" + p.TrackingID + "','" + Properties.Settings.Default.Alumno + "')";
            bool insertado = false;
            try
            {
                _comando.CommandText = sql;
                _conexion.Open();
                _comando.ExecuteNonQuery();
                insertado = true;
            }
            catch (Exception e)
            {
                insertado = false;
                throw e;
            }
            finally
            {
                if (insertado)
                    _conexion.Close();
            }
            return insertado;
        }
        #endregion
    }
}
