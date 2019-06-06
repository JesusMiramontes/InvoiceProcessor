using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesarFacturasXml
{
    public partial class Refactorizar : Form
    {
        public Refactorizar()
        {
            InitializeComponent();
            Archivos.establecerOrigenDgv(dgvXmls,Form1.a.facturas_xmls);
            Archivos.establecerOrigenDgv(dgvPdfs,Form1.a.pdfs);
        }
    }
}
