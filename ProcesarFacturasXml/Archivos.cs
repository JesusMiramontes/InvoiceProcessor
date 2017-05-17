using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesarFacturasXml
{
    class Archivos
    {
        public static IList<string> pdfs = new List<string>();
        public static IList<string> xmls = new List<string>();
        public static IList<string> coinciden = new List<string>();

        public static void agregarPdfs(string folder_path) {
            foreach (string file in Directory.EnumerateFiles(folder_path, "*.pdf"))
            {
                pdfs.Add(file);
            }
        }

        public static void agregarXmls(string folder_path)
        {
            foreach (string file in Directory.EnumerateFiles(folder_path, "*.xml"))
            {
                xmls.Add(file);
            }
        }

        public static string obtenerFileName(string full_path) {
            return System.IO.Path.GetFileNameWithoutExtension(full_path);
        }

        public static void compararListas(IList<string> pdfs, IList<string> xmls) {
            int limite = (pdfs.Count >= xmls.Count) ? pdfs.Count : xmls.Count;

            for (int i = 0; i < limite; i++)
            {
                if (pdfs.Count > xmls.Count)
                {
                    foreach (var item in pdfs)
                    {
                        if (xmls.Contains(item.Replace(".pdf", ".xml")))
                        {
                            coinciden.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in xmls)
                    {
                        if (pdfs.Contains(obtenerFileName(item) + ".pdf"))
                        {
                            coinciden.Add(item);
                        }
                    }
                }   
            }
        }
    }
}
