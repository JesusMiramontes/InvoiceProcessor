using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProcesarFacturasXml
{
    class Factura
    {
        public string total { get; set; }
        public string emisor { get; set; }
        public string fecha { get; set; }

        /// <summary>
        /// Constructor de factura que inicializa los valores en nil
        /// </summary>
        public Factura() {
            total = emisor = fecha = "nil";
        }

        /// <summary>
        /// Constructor de factura que recibe las propiedades de una factura
        /// </summary>
        /// <param name="t">Total</param>
        /// <param name="e">Emisor</param>
        /// <param name="f">Fecha</param>
        public Factura(string t, string e, string f) {
            total = t;
            emisor = e;
            fecha = f;
        }

        /// <summary>
        /// Metodo que obtiene la información de un .xml y regresa un objeto Factura
        /// </summary>
        /// <param name="file_path">Ruta del archivo a analizar</param>
        public static Factura readFromXmlFile(string file_path)
        {
            //file_path = "c:\\users\\jesus miramontes\\documents\\visual studio 2017\\Projects\\ProcesarFacturasXml\\ProcesarFacturasXml\\Resources\\file.xml";
            //Crea un objeto factura al que se le asignarán las propiedades para despues devolverlo
            Factura f = new Factura();

            //Crea un objeto capaz de leer archivos xml
            XmlReader reader = XmlReader.Create(file_path);

            while (reader.Read())
            {
                //Busca los atributos total y fecha que se encuentan en cfdi:comprobante
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "cfdi:Comprobante")
                {
                    if (reader.HasAttributes) {
                        f.total = reader.GetAttribute("total");
                        f.fecha = reader.GetAttribute("fecha");
                    }
                    else
                        throw new Exception("No ha sido posible procesar la información.");
                }

                //Busca el atributo nombre que se encuentan en cfdi:Emisor
                else if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "cfdi:Emisor")
                {
                    if (reader.HasAttributes)
                    {
                        f.emisor = reader.GetAttribute("nombre");
                    }
                    else
                        throw new Exception("No ha sido posible procesar la información.");
                }

            }

            //Regresa el objeto factura con los atributos encontrados
            return f;
        }
    }
}
