using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
   public class SymbolTable
    {
        public string name, type, className;
        public int scope;

       public SymbolTable()
        {

        }
        public SymbolTable(string name, string type, string classname, int s)
        {
            this.name = name;
            this.type = type;
            this.className = classname;
            this.scope = s;
        }
    }
}
