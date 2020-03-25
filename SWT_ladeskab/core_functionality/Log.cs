using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace core_functionality
{
    public class Log : ILog
    {
        public void PrintToFile(string TextToPrint, int RFID_id)
        {
            using (var write = File.AppendText("log.txt"))
            {
                log(TextToPrint, RFID_id, write);
            }
        }

        private void log(string TextToPrint, int RFID_id, TextWriter w)
        {
            w.WriteLine($"{DateTime.Now.ToLongTimeString()}{DateTime.Now.ToLongDateString()}");
            w.WriteLine($":{TextToPrint}", RFID_id);
        }
    }
}