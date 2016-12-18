using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lexical_Analyzer2 
{
    class myLanguage
    {
        //public static char wordBreaker = ' ';
        public static string[] Keywords = { "Alpha", "Digit", "Word","Float", "till", "untill", "Case", "then", "ProgramStart", "End", "Goback", "Class", "create","main", "Void", "For", "call" };
        public static string[] Punctuators = { "[", "]", "(", ")", ",", ":","." };
        public static string[] Operators = { "+", "-", "/", "%", "*" , "!", "++", "--", "<", ">", "<=", ">=","&","|", "!=", "==", "&&", "||", "+=", "-=", "/=", "*=", "=", "%=",";" };
        public static string[] Pointers = { "~", "?" };
        public static string EndMarker = "$";

        public static bool isEndMarker(string word)
        {
            if (word == EndMarker)
                return true;
            else
                return false;
        }
        public static bool IsPointer(string word)
        {
            foreach (var item in Pointers)
            {
                if (word == item)
                    return true;
            }
            return false;

        }
        public static bool IsKeyword(string word)
        {
            foreach (var item in Keywords)
            {
                if (word == item)
                    return true;
            }
            return false;

        }
        
        public static bool IsPunctuator(string word)
        {
            foreach (var item in Punctuators)
            {
                if (word == item.ToString())
                    return true;
            }
            return false;

        }
        public static bool IsOperator(string word)
        {
            foreach (var item in Operators)
            {
                if (word == item)
                    return true;
            }
            return false;

        }
        public static bool IsIdentifier(string word)
        {
            return Regex.Match(word, @"^[a-zA-Z]+\d?$").Success ? true : false;
        }

        public static bool IsFloat(string word)
        {
            return Regex.Match(word, "^(\\-|\\+|)(\\d*)\\.(\\d)+$").Success ? true : false;
        }
        
        public static bool IsAlphaConstant(string word)
        {

            string checkword = word;
            myAlpha ma = new myAlpha();
            //ma.inp(checkword);
            try
            {
                bool a = ma.check(checkword);
                if (a == true)
                {
                    return true;
                }
            }
            catch (Exception e) 
            {
                word = e.Message;
            }
            return false;

            //return Regex.Match(word, "^'.'$").Success?true:false;
            //return Regex.Match(word, "^\'(\\w|\\d|\\D)\'$").Success ? true : false;
        }
        public static bool IsDigitConstant(string word)
        {
            string checkWord = word;
            myDigit md = new myDigit();
            md.inp(checkWord);
            bool a = md.Finite();
            if (a == true)
            {
                return true;
            }
            return false;

            //return Regex.Match(word, "^\'[+-]?\\d+\'$").Success ? true : false;
            //return Regex.Match(word, "^(\\-|\\+|)([0-9]+)$").Success ? true : false;
        }
        public static bool IsWordConstant(string word)
        {
            //string checkWord = word;
            //myWord mw = new myWord();
            //mw.inp(checkWord);
            //bool a = mw.Finite();
            //if (a == true)
            //{  
            //    return true;
            //}
            //return false;
            return Regex.Match(word, "^\"(\\D|\\d|\\w)*\"$").Success ? true : false;

        }
        
    }
}
