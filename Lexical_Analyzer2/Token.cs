using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class Token
    {
        public Token(int ln,string vp) 
        {
            this.lineNumber = ln;
            this.valuePart = vp;
        }

        string valuePart { get; set; }
        public string classPart { get; set; }
        int lineNumber { get; set; }

        public override string ToString()
        {
            return "( " + this.valuePart + " , " + this.classPart + " , " + lineNumber + " )";
        }
     
    }
    public enum ClassPart
    {
        DT,till, untill, Case, then, ProgramStart, End, Goback, Class, create,PM,Alpha_Const,
        Digit_Const, Word_Const,Float_Const,ID,MDM,INC_DEC,Rel_OP,Asg_OP,Void,main,For,call,EndMarker
    }
}
