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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Factura f = Factura.readFromXmlFile(openFileDialog1.FileName);
                dgvFacturas.Rows.Add(f.fecha, f.emisor, f.total);
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.xml"))
                {
                    Factura f = Factura.readFromXmlFile(file);
                    dgvFacturas.Rows.Add(f.fecha, f.emisor, f.total);
                }
            }
        }
    }
}
