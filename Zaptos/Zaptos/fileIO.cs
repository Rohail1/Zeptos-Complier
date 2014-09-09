using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Threading;

namespace Zaptos
{
    class fileIO
    {
        public void WriteToFile(string path,string[] lines)
        {
           File.WriteAllLines(path, lines);
        }

        public string ExtractFromRichTextBox(RichTextBox rtb)
        {
            TextRange textrange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textrange.Text;
        }
        public string[] ReadFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
    }
}
