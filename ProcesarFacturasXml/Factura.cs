﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProcesarFacturasXml
{
    public class Factura
    {
        public string total { get; set; }
        public string fecha { get; set; }

        // Datos emisor
        public string emisor_razon_social { get; set; }
        public string emisor_rfc { get; set; }

        // Datos receptor
        public string receptor_razon_social { get; set; }
        public string receptor_rfc { get; set; }

        /// <summary>
        /// Constructor de factura que inicializa los valores en nil
        /// </summary>
        public Factura() {
            total = fecha = emisor_razon_social = emisor_rfc =
                receptor_razon_social = receptor_rfc = "nil";
        }

        /// <summary>
        /// Constructor de factura que recibe las propiedades de una factura
        /// </summary>
        /// <param name="t">Total</param>
        /// <param name="e">Emisor</param>
        /// <param name="f">Fecha</param>
        public Factura(string t, string e, string f) {
            total = t;
            emisor_razon_social = e;
            fecha = formatoFecha(f);
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

            bool valid_file = false;

            while (reader.Read())
            {
                //Busca los atributos total y fecha que se encuentan en cfdi:comprobante
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name == "cfdi:Comprobante")
                {
                    if (reader.HasAttributes)
                    {
                        f.total = reader.GetAttribute("total");
                        f.fecha = formatoFecha(reader.GetAttribute("fecha"));
                        valid_file = true;
                    }
                    else
                    {
                        throw new Exception("No ha sido posible procesar la información.");
                    }
                }

                //Busca el atributo nombre que se encuentan en cfdi:Emisor
                else if (valid_file && (reader.NodeType == XmlNodeType.Element) && reader.Name == "cfdi:Emisor")
                {
                    if (reader.HasAttributes)
                    {
                        f.emisor_razon_social = reader.GetAttribute("nombre");
                        f.emisor_rfc = reader.GetAttribute("rfc");
                    }
                    else
                        throw new Exception("No ha sido posible procesar la información.");
                }

                else if (valid_file && (reader.NodeType == XmlNodeType.Element) && reader.Name == "cfdi:Receptor")
                {
                    if (reader.HasAttributes)
                    {
                        f.receptor_razon_social = reader.GetAttribute("nombre");
                        f.receptor_rfc = reader.GetAttribute("rfc");
                    }
                    else
                        throw new Exception("No ha sido posible procesar la información.");
                }
                else
                {
                    //throw new Exception("No es un CDFI válido: " + file_path);
                }
            }

            reader.Close();

            //Regresa el objeto factura con los atributos encontrados
            return f;
        }

        /// <summary>
        /// Metodo que convierte los valores de una factura en csv
        /// </summary>
        /// <returns>string separado por comas</returns>
        public string aCsv() {
            //Cadena interpolada que separa los atributos de la factura con ','
            return $"{fecha};{emisor_razon_social};{total}";
        }


        /// <summary>
        /// Método que recibe una lista y regresa un string para guardarse en texto csv
        /// </summary>
        /// <param name="lista">Lista de facturas</param>
        /// <returns>string con propiedades de cada factura separada por comas, cada factura separada con \n</returns>
        public static string listaACsv(IList<Factura> lista) {
            // Cadena donde se almacenarán las facturas
            string output = "";

            // Recorre toda la lista
            foreach (var f in lista) {
                // Agrega al output la factura separada por comas que recibe del método aCsv()
                output += f.aCsv();

                // Agrega un salto de linea entre cada factura
                output += "\r\n";
            }
            return output;
        }

        /// <summary>
        /// Metodo que exporta la lista de facturas a un archivo de texto
        /// </summary>
        /// <param name="facturas">Lista de facturas importadas</param>
        /// <param name="file_path">Ruta donde se guardará el archivo</param>
        public static void exportarAArchivo(string facturas, string file_path) {
            // Crea un objeto StreamWriter y le asigna la ruta recibida
            System.IO.StreamWriter writer = new System.IO.StreamWriter(file_path);

            //Escribe en el writer la cadena de facturas recibida
            writer.WriteLine(facturas);

            // Cierra el flujo del writer
            writer.Close();
        }

        public static string formatoFecha(string f) {
            if (f == null)
                return null;
            DateTime date = DateTime.Parse(f);
            return String.Format("{0:dd/MM/yy}", date);
        }
    }
}
