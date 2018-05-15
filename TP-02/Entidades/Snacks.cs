using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_2017
{
    public class Snacks:Producto
    {

        /// <summary>
        /// Los snacks tienen 104 calorías
        /// </summary>
        protected override short CantidadCalorias
        {
            get
            {
                return 104;
            }
        }

        #region "Constructores"

        /// <summary>
        /// Crea un snack
        /// </summary>
        /// <param name="marca">Marca del snack</param>
        /// <param name="codigo">codigo de barras del snack</param>
        /// <param name="color">Color del empaque</param>
        public Snacks(Producto.EMarca marca, string codigo, ConsoleColor color)
            : base(codigo, marca, color)
        {
        }

        #endregion

        #region "Metodos"
        /// <summary>
        /// Muestra en detalle el producto snack 
        /// </summary>
        /// <returns></returns>
        public override sealed string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SNACKS");
            sb.AppendLine(base.Mostrar());
            sb.AppendFormat("CALORIAS : {0}", this.CantidadCalorias);
            sb.AppendLine("");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

        #endregion
    }
}
