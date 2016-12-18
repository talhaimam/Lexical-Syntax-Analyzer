using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class myAlpha
    {
        public int[,] array = { { 1, 5, 5, 5, 5, 5, 5 }, { 5, 2, 2, 2, 4, 5, 5 }, { 3, 5, 5, 5, 5, 3, 5 }, { 5, 5, 5, 5, 5, 5, 5 }, { 5, 5, 5, 5, 5, 5, 2 }, { 5, 5, 5, 5, 5, 5, 5 } };
        public static String input;
        public int state = 0;
        static int s = 0, m = 0;
        bool flag = false;

        public bool check(String ip)
        {

            input = ip;

            int j = ip.Length;
            while (j > 0)
            {
                if (state == -100)
                {
                    break;
                }
                state = transition(state, ip[s]);
                s++;

                j--;
            }
            if (state == 3)
            {
                s = 0;
                flag = false;
                return true;

            }
            else
            {
                s = 0;
                flag = false;
                return false;
            }

        }

        public int transition(int st, char ch)
        {
            if (s == 0)
            {
                if (ch == '\'')
                {
                    return array[st, 0];

                }
            }

            if (ch == '@' || ch == '!' || ch == '#' || ch == '$' || ch == '%' || ch == '^' || ch == '&' || ch == '*' || ch == '(' || ch == ')' || ch == '_' || ch == '+' || ch == '-' || ch == '=' || ch == '?' || ch == '<' || ch == '>'
                   || ch == '/' || ch == ':' || ch == '.' || ch == ',' || ch == ';' || ch == ']' || ch == '[' || ch == '{' || ch == '}' || ch == '|' || ch == '~' || ch == '`' || ch == ' ')
            {
                return array[st, 3];
            }

            if (ch == '\'' && flag == false)
            {
                return array[st, 5];
            }

            if (input[s - 1] == '\\' && flag == true)
            {
                if (ch == 'n' || ch == 'r' || ch == 't' || ch == '"' || ch == '\'' || ch == '\\' || ch == 'b' || ch == 'f')
                {
                    flag = false;
                    return array[st, 6];
                }
            }

            if (ch == '\\')
            {
                flag = true;
                return array[st, 4];
            }

            if ((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122))
            {
                return array[st, 1];
            }
            if ((ch >= 48 && ch <= 57))
            {
                return array[st, 2];
            }
            else
            {

                return -100;

            }
        }
    }
}
