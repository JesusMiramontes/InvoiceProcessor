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
            bool valid_file = true;
            Factura f = new Factura();

            //Crea una factura f con la factura que recibe del xml
            try
            {
                f = Factura.readFromXmlFile(file_path);
            }
            catch (Exception)
            {
                valid_file = false;
            }

            if (valid_file)
            {
                //Agrega la factura f a la lista "facturas"
                facturas.Add(f);
            }
            
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
            dgv.DataSource = lista;
        }

        /// <summary>
        /// Copia el contenido que reciba al portapapeles
        /// </summary>
        /// <param name="s"> texto a almacenar en portapapeles</param>
        static void copiarAPortapapeles(string s) {
            Clipboard.SetText(s);
        }

        /// <summary>
        /// Metodo que se ejecuta al importar un solo archivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            // Muesta el dialogo y si se eligue un archivo lo agrega a la lista y actualiza el dgv
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                agregarArchivoALista(openFileDialog1.FileName);
                establecerOrigenDgv(dgvFacturas, facturas);
            }
        }

        /// <summary>
        /// Se ejecuta al presionar boton para analizar carpeta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            // Crea un nuevo objeto lista
            facturas = new List<Factura>();

            // Crea un objeto de archivos
            Archivos a = new Archivos();

            // Se muestra el dialogo y compara la selección del usuario
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Al objeto a le asigna la ruta de la carpeta seleccionada
                a = new Archivos(folderBrowserDialog1.SelectedPath);

                // Restablece la configuración del dialogo
                folderBrowserDialog1.Reset();
                folderBrowserDialog1.Dispose();

                // Importa, analiza, almacena los xmls; Crea subcarpetas y mueve los pdf y xml a su carpeta correspondiente
                a.ejecutar();

                // Actualiza la lista facturas según el objeto archivos
                facturas = a.getXmlsAsfacturas();

                // Actualiza los datos de dgv
                establecerOrigenDgv(dgvFacturas, a.getXmlsAsfacturas());
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
