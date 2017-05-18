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
            //string s = Archivos.obtenerFileName("c:\\users\\jesus miramontes\\documents\\visual studio 2017\\Projects\\ProcesarFacturasXml\\ProcesarFacturasXml\\Resources\\file.xml");
            Archivos a = new Archivos("C:\\Users\\Jesus Miramontes\\Desktop\\fact");
            a.ejecutar();
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

        /// <summary>
        /// Actualiza el origen del datagridview
        /// </summary>
        /// <param name="dgv">Datagridview al que se le asignará el origen</param>
        /// <param name="lista">Lista con las facturas</param>
        static void establecerOrigenDgv(DataGridView dgv, IList<Factura> lista) {
            // Establece el origen a null
            dgv.DataSource = null;

            //Establece la lista facturas como el origen del dgv
            dgv.DataSource = facturas;
        }

        static void copiarAPortapapeles(string s) {
            Clipboard.SetText(s);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                agregarArchivoALista(openFileDialog1.FileName);
                establecerOrigenDgv(dgvFacturas, facturas);
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.xml"))
                {
                    agregarArchivoALista(file);
                }
                establecerOrigenDgv(dgvFacturas, facturas);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Exporta la lista de factuas a un archivo de texto en la ruta establecida con el save file dialog
                Factura.exportarAArchivo(Factura.listaACsv(facturas), saveFileDialog1.FileName);
            }
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            // Copia las facturas en csv al portapapeles
            copiarAPortapapeles(Factura.listaACsv(facturas));
        }
    }
}
