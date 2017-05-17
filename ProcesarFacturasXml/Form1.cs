using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProcesarFacturasXml
{
    public partial class Form1 : Form
    {
        // Lista que almacena las facturas importadas
        static IList<Factura> facturas = new List<Factura>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Recibe el path de un archivo xml, lo procesa, convierte a factura, y lo agrega a la lista de facturas
        /// </summary>
        /// <param name="file_path">Ruta del archivo a procesar</param>
        static void agregarArchivoALista(string file_path) {
            //Crea una factura f con la factura que recibe del xml
            Factura f = Factura.readFromXmlFile(file_path);

            //Agrega la factura f a la lista "facturas"
            facturas.Add(f);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                agregarArchivoALista(openFileDialog1.FileName);
            }

            //Establece la lista facturas como el origen del dgv
            dgvFacturas.DataSource = facturas;
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.xml"))
                {
                    agregarArchivoALista(file);
                }
            }

            //Establece la lista facturas como el origen del dgv
            dgvFacturas.DataSource = facturas;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Exporta la lista de factuas a un archivo de texto en la ruta establecida con el save file dialog
                Factura.exportarAArchivo(Factura.listaACsv(facturas), saveFileDialog1.FileName);
            }
        }
    }
}
