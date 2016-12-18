using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class Semantic_Analyzer
    {
        List<Token> j = new List<Token>();
        int tn = 0;
        public bool CheckSemantics(List<Token> tokenSet)
        {
            this.j = tokenSet;
           // return Start();
            return true;
        }
    }
}
