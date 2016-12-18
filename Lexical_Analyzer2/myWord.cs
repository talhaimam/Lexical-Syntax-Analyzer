using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class myWord
    {
        bool lastBackSlash = false;
        char[] escapeSequence = new char[] { '\\', '\'', '\"', '\0', 'a', 'f', 'n', 'r', 't', 'v', 'b' };
        char[] specialChars = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', ';', ':', '/', '>', '?', '<', '.', ',', '|', '~', '`', '[', ']', '}', '{', ' ' };
        int[,] TT = { { 3, 1, 6, 6, 6 }, { 6, 2, 1, 5, 6 }, { 6, 6, 6, 6, 6 }, { 4, 6, 3, 7, 6 }, { 6, 6, 6, 6, 6 }, { 6, 6, 6, 6, 1 }, { 6, 6, 6, 6, 3 } };
        int state = 0;
        string input;

        //public sTRING(string value)
        //{
        //    input = value;
        //}


        public bool checkEscape(char ch)
        {
            for (int i = 0; i < escapeSequence.Length; i++)
            {
                if (ch == escapeSequence[i])
                    return true;
            }
            return false;
        }

        public bool checkSpcialChars(char ch)
        {
            for (int i = 0; i < specialChars.Length; i++)
            {
                if (ch == specialChars[i])
                    return true;
            }
            return false;
        }
        public bool Finite(string value)
        {
            input = value;
            int i = 0;
            while (i < input.Length)
            {
                state = transition(state, input[i]);
                i++;
            }
            if (state == 2)
            {
                return true;
            }
            else
                return false;
        }
        public int transition(int st, char ch)
        {
            //    if (ch == '*')
            //    {
            //        state = TT[st, 0];
            //        return state;
            //    }

            //    else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || ch == '\\' || ch == '\''  || ch == '\"' || ch == '\0' || ch == '\a' || ch == '\f' || ch == '\n' || ch == '\r' || ch == '\t' || ch == '\v' || ch == '\b' || ch == '!' || ch == '@' || ch == '#' || ch == '$' || ch == '%' || ch == '&' || ch == '(' || ch == ')' || ch == '-' || ch == '_' || ch == '+' || ch == '=' || ch == ';' || ch == ':' || ch == '/' || ch == '>' || ch == '?' || ch == '<' || ch == '.' || ch == ',' || ch == '|' || ch == '~' || ch == '`' || ch == '[' || ch == ']' || ch == '}' || ch == '{' || ch == '^' || ch == ' ')
            //    {
            //        state = TT[st, 1];
            //        return state;
            //    }
            //    else
            //    {

            //        return 3;
            //    }

            if (ch == '\'')
            {
                if (!lastBackSlash)
                    state = TT[st, 0];
                else
                {
                    state = TT[st, 4];
                    lastBackSlash = false;
                }
            }
            else if (ch == '\"')
            {
                if (!lastBackSlash)
                    state = TT[st, 1];
                else
                {
                    state = TT[st, 4];
                    lastBackSlash = false;
                }
            }
            //else if(checkEscape(c))
            //    IS = TT[st, 4];
            else if (((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || checkSpcialChars(ch)))
            {
                if (lastBackSlash)
                {
                    if (checkEscape(ch))
                    {
                        state = TT[st, 4];
                        lastBackSlash = false;
                    }
                }
                else
                    state = TT[st, 2];
            }
            else if (ch == '\\')
            {
                state = TT[st, 3];
                lastBackSlash = true;
            }
            else
            {
                state = TT[st, 4];
            }
            return state;
        }
    }
}
