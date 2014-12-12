using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
    class ScopeControl
    {
        public Stack<int> ScopeStack = new Stack<int>();
        public int Scope { get; set; }
        public ScopeControl()
        {

        }

        public void CreateScope()
        {
            Scope++;
            ScopeStack.Push(Scope);
        }
        public void DestroyScope()
        {
            ScopeStack.Pop();

        }
    }
}
