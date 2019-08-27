using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1
{

     public class  LogIt
    {
       
        private string FileName;

        public LogIt(string fileName)
        {

            FileName = fileName;

        }


        public void WriteNewLog(string text)
        {

            using (StreamWriter writer = File.CreateText(FileName))
            {

                writer.WriteLine(text);

            }

        }

        public void AppendLog(string text)
        {

            using (StreamWriter appendWriter =  File.AppendText(FileName))
            {

                appendWriter.WriteLine(text);

            }

        }

    }
}
