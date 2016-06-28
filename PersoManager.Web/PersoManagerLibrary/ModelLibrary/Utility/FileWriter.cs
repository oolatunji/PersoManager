using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoManagerLibrary
{
    public class FileWriter
    {
        public static void Write(string data)
        {
            string directoryPath = System.Configuration.ConfigurationManager.AppSettings.Get("PersoFilePath");

            var today = System.DateTime.Now;

            string filename = string.Format("{0}_CMSData", String.Format("{0:dMyyyyHHmmss}", today));

            string filepath = directoryPath + "\\" + filename + ".txt";


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            StreamWriter file = new StreamWriter(fs);
            file.Write(data);
            file.Close();
            fs.Close();
        }
    }
}
