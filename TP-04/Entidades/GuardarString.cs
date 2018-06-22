using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public static class GuardarString
    {
        #region Metodos
        /// <summary>
        /// Guarda el texto en archivo , en caso de exitir solo lo agrega
        /// </summary>
        /// <param name="texto">texto a agregar dentro del archivo </param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static bool Guardar(this string texto, string archivo)
        {
            try
            {               
                string descktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);                
                string filePath = descktopFolder + Path.DirectorySeparatorChar + archivo;
                StreamWriter sw = new StreamWriter(filePath, true);                
                sw.WriteLine(texto);                
                sw.Close();                              
            }            
            catch (Exception ex)
            {
                throw ex;
            }           
            return true;
        }
        #endregion
    }
}
