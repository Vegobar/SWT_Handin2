using System;
using System.IO;

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