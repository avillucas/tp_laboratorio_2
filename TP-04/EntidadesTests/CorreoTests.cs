using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tests
{
    [TestClass()]
    public class CorreoTests
    {
        [TestMethod()]        
        public void CorreoExistsTest()
        {
            // arrange  
            Correo correo = new Correo();            
            Paquete paquete = new Paquete("Tesst", "11-12-1234");
            // act  
            try
            {
                correo.Paquetes.Add(paquete);
            }
            catch (NullReferenceException e)
            {
                Assert.Fail();
            }
            // assert 
        }

        [TestMethod()]
        [ExpectedException(typeof(TrackingIdRepetidoException))]
        public void CorreoDuplicatedAdd()
        {
            //arrange
            Correo correo = new Correo();
            Paquete paquete = new Paquete("Tesst", "11-12-1234");
            Paquete paquete1 = new Paquete("Tesster", "11-12-1234");
            //act            
            correo += paquete;
            correo += paquete1;
            //assert
        }
    }
}