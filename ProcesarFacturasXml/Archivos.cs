using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace ProcesarFacturasXml
{
    public class Archivos
    {
        // Lista con los pdf encontrados
        public IList<string> pdfs;

        // Lista con los xmls encontrados
        private IList<string> xmls;

        // Lista con los archivos que existen en ambas listas
        private IList<string> coinciden;

        // Ruta de la carpeta sobre la que se buscarán archivos y analizarán
        public string folder_path;

        // Extensiones a buscar
        private string[] extensiones = { ".xml", ".pdf" };

        // Almacena las facturas con sus propiedades, lista que funciona para mostrar los datos en gdv
        public IList<Factura> facturas_xmls;

        /// <summary>
        /// Constructor que inicializa en nil
        /// </summary>
        public Archivos() {
            pdfs = new List<string>();
            xmls = new List<string>();
            coinciden = new List<string>();
            folder_path = "nil";
            facturas_xmls = new List<Factura>();
        }

        /// <summary>
        /// Constructor que recibe la ruta del folder
        /// </summary>
        /// <param name="path">Carpeta a analizar</param>
        public Archivos(string path) {
            pdfs = new List<string>();
            xmls = new List<string>();
            coinciden = new List<string>();
            folder_path = path;
            facturas_xmls = new List<Factura>();
        }

        /// <summary>
        /// Busca en la carpeta todos los archivos .pdf y los agrega a la lista
        /// </summary>
        private void cargarArchivos() {

            // Repite la operación con los archivos .pdf y .xml
            for (int i = 0; i < extensiones.Length; i++)
            {
                // Recorre todos los archivos de la carpeta con la extensión correspondiente
                foreach (string file in Directory.EnumerateFiles(folder_path, "*" + extensiones[i]))
                {
                    // Si el archivo tiene la extensión buscada lo agrega a la lista correspondiente
                    if (extensiones[i] == ".pdf")
                    {
                        pdfs.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                    else if (extensiones[i] == ".xml")
                    {
                        xmls.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                        facturas_xmls.Add(Factura.readFromXmlFile(file));
                    }
                } 
            }
        }
        
        /// <summary>
        /// Compara ambas lista en busca de archivos que existan en ambos formatos
        /// </summary>
        private void compararListas() {

            // Toma como límite el tamaño de la lista mas grande
            //int limite = (pdfs.Count >= xmls.Count) ? pdfs.Count : xmls.Count;

            // Recorre la lista mas grande y la compara con la mas chica, si coincide en ambas listas la agrega a la correspondiente
                // Copia las listas ya que serán modificadas
            IList<string> pdfs_copy = new List<string>(pdfs);
            IList<string> xmls_copy = new List<string>(xmls);

                if (pdfs.Count > xmls.Count)
                {
                    foreach (var item in pdfs_copy)
                    {
                        if (xmls.Contains(item))
                        {
                            coinciden.Add(item);
                            pdfs.RemoveAt(pdfs.IndexOf(item));
                        }
                    }
                }
                else
                {
                    foreach (var item in xmls_copy)
                    {
                        if (xmls.Contains(item))
                        {
                            coinciden.Add(item);
                            xmls.RemoveAt(pdfs.IndexOf(item));
                    }
                }
                }   
            
        }

        /// <summary>
        /// Mueve los archivos pdf y xml a sus respectivas subcarpetas
        /// </summary>
        private void moverCoinciden() {

            foreach (var file in coinciden)
            {
                // Ruta a donde se moverá el archivo
                string target_path = System.IO.Path.Combine(folder_path, file);
                string[] extensiones = { ".xml", ".pdf" };

                //Crea las carpetas con los nombres de los archivos que coinciden en caso de no existir ya
                if (!System.IO.Directory.Exists(target_path))
                {
                    System.IO.Directory.CreateDirectory(target_path);
                }

            }
            
            foreach (var file in coinciden)
            {
                // Almacena la ubicación del archivo con el que se está trabajando
                string from_location = "";

                // Realiza la operacíon con cada extensión
                for (int i = 0; i < extensiones.Length; i++)
                {
                    // Concatena la ruta del la carpeta, el nombre del archivo y la extensión
                    from_location = string.Format("{0}\\{1}{2}", folder_path, file, extensiones[i]);
            
                    // Concatena la ruta de la carpeta, el nombre de la subcarpeta, el nombre del archivo, y la extensión
                    string to_location = string.Format("{0}\\{1}\\{2}{3}", folder_path, file, file, extensiones[i]);

                    //System.Threading.Thread.Sleep(1000);
                    try
                    {
                        // Mueve el archivo de la ruta padre a la subcarpeta
                        System.IO.File.Move(from_location, to_location);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);//throw ex;
                    }
            
                }

                // Comprimir en ZIP la carpeta creada que ya contiene los archivos
                string current_working_folder = folder_path + "\\" + System.IO.Path.GetFileNameWithoutExtension(from_location);
                ZipFile.CreateFromDirectory(current_working_folder, current_working_folder+".zip");

                // Elimina la carpeta despues de comprimirla
                Directory.Delete(current_working_folder, true);
            }
        }

        /// <summary>
        /// Analiza los archivos, crea las carpetas, y mueve los .xml y.pdf
        /// </summary>
        public void ejecutar() {
            cargarArchivos();
            compararListas();
            moverCoinciden();
        }

        /// <summary>
        /// Getter facturas_xml
        /// </summary>
        /// <returns> Lista de facturas para mostrarse en dgv</returns>
        public IList<Factura> getXmlsAsfacturas() {
            return facturas_xmls;
        }

        /// <summary>
        /// Actualiza el origen del datagridview
        /// </summary>
        /// <param name="dgv">Datagridview al que se le asignará el origen</param>
        /// <param name="lista">Lista con las facturas</param>
        public static void establecerOrigenDgv(DataGridView dgv, IList<Factura> lista)
        {
            // Establece el origen a null
            dgv.DataSource = null;

            //Establece la lista facturas como el origen del dgv
            dgv.DataSource = lista;
        }

        /// <summary>
        /// Actualiza el origen del datagridview
        /// </summary>
        /// <param name="dgv">Datagridview al que se le asignará el origen</param>
        /// <param name="lista">Lista con las facturas</param>
        public static void establecerOrigenDgv(DataGridView dgv, IList<string> lista)
        {
            foreach (var i in lista)
            {
                dgv.Rows.Add(i);
            }
        }

    }
}
