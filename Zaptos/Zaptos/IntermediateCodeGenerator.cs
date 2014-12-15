using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
    class IntermediateCodeGenerator
    {
        public int label { get; set; }
        public int temperaryVariable { get; set; }
        public string IntermediateCode { get; set; }
        public IntermediateCodeGenerator()
        {

        }
        public string generatelabel()
        {
            string L = "L";
            L += label;
            label++;
            return L;
        }
        public string generateTemperaryVariable()
        {
            string t = "T";
            t += temperaryVariable;
            temperaryVariable++;
            return t;
        }
    }
}
