using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class Logger
    {
        public static Logger Instance { get; } = new Logger();

        private Logger() { }

        private const string filePath = "logs.txt";

        public void log(Exception exception)
        {
            try
            {
                using (StreamWriter fs = new StreamWriter(filePath, true))
                {
                    String entry = DateTime.Now.ToUniversalTime() + " " + exception.ToString();
                    fs.WriteLine(entry);
                    fs.WriteLine();
                }
            }
            catch (Exception e) { }
        }
    }
}
