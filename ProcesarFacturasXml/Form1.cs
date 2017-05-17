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
        IList<Factura> facturas = new List<Factura>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Crea una factura f con la factura que recibe del xml
                Factura f = Factura.readFromXmlFile(openFileDialog1.FileName);

                //Agrega la factura f a la lista "facturas"
                facturas.Add(f);
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
                    //Crea una factura f con la factura que recibe del xml
                    Factura f = Factura.readFromXmlFile(file); //*****Código repetido, solucionar*****

                    //Agrega la factura f a la lista "facturas"
                    facturas.Add(f); //*****Código repetido, solucionar*****
                }
            }

            //Establece la lista facturas como el origen del dgv
            dgvFacturas.DataSource = facturas; //*****Código repetido, solucionar*****
        }
    }
}
