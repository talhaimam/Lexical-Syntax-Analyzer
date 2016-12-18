using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class myDigit
    {
        int[,] TT = { { 2, 2, 1 }, { 3, 3, 1 }, { 3, 3, 1 }, { 3, 3, 3 } };
        int state = 0;
        string input;
        bool jani = false;

        public void inp(string word)
        {
            this.input = word;
        }

        public bool Finite()
        {
            int i = 0;
            while (i < input.Length)
            {
                state = transition(state, input[i]);
                i++;
            }
            if (state == 1)
            {
                return true;
            }
            else
                return false;
        }
        public int transition(int st, char ch)
        {
            if (jani)
            {
                return -1;
            }
            else if (!jani)
            {
                if (ch == '+')
                {
                    state = TT[st, 0];
                    return state;
                }
                if (ch == '-')
                {
                    state = TT[st, 1];
                    return state;
                }
                else if (ch >= '0' && ch <= '9')
                {
                    state = TT[st, 2];
                    return state;
                }

                else
                {
                    jani = true;
                    return -1;
                }
            }
            return -1;
        }
    }
}
