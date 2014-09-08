using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Zaptos
{
    class fileIO
    {
        public void write(string path,string[] lines)
        {
           File.WriteAllLines(path, lines);
        }
    }
}
