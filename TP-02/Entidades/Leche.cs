using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Entidades_2017
{
    public class Leche : Producto
    {
        public enum ETipo { Entera, Descremada }

        ETipo _tipo;

        /// <summary>
        /// Las leches tienen 20 calorías
        /// </summary>        
        protected override short CantidadCalorias
        {
            get
            {
                return 20;
            }
        }

        #region "Constructores"

        /// <summary>
        /// Crea una leche de tipo Entera
        /// </summary>
        /// <param name="marca">Narca de la leche</param>
        /// <param name="codigo">Codigo de barras de la leche</param>
        /// <param name="color">Color del empaque</param>
        public Leche(EMarca marca, string codigo, ConsoleColor color) : this(marca, codigo, color, ETipo.Entera)            
        {

        }

        /// <summary>
        /// Crea una leche
        /// </summary>
        /// <param name="marca">Marca de la leche</param>
        /// <param name="codigo">Codigo de barras</param>
        /// <param name="color">Color el empaque</param>
        /// <param name="tipo">Tipo de leche</param>
        public Leche(EMarca marca, string codigo, ConsoleColor color, ETipo tipo) : base(codigo, marca, color)
        {
            this._tipo = tipo;
        }

        #endregion

        #region "Metodos"

        /// <summary>
        /// Retorna un detalle de la leche
        /// </summary>
        /// <returns></returns>
        public override sealed string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("LECHE");
            sb.AppendLine(base.Mostrar());
            sb.AppendFormat("CALORIAS : {0}", this.CantidadCalorias);
            sb.AppendFormat("TIPO : " + this._tipo);
            sb.AppendLine("");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

        #endregion
    }
}
