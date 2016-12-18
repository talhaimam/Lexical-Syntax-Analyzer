using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Lexical_Analyzer2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int charsize=0;
        static int charflag = 0;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void File_Path_TextChanged(object sender, EventArgs e)
        {
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            string path = File_Path.Text;
            string resultPath = @"D:\TestingFiles\Tokens.txt";
            ReadFile(path);
            BreakWords();
            WriteToFile(resultPath);
            Process.Start(resultPath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Syntax_Analyzer analyzeSyntax = new Syntax_Analyzer();
            bool syntaxIsValid = analyzeSyntax.ParseTokens(tokenSet);
            string[] result = { "Parsed!", "Error" };
            if (syntaxIsValid)
            {
                showResult.Text += " "+result[0];
            }
            else
                showResult.Text += " "+result[1];
        }
        

        List<string> linesOfCode = new List<string>();
        List<Token> tokenSet = new List<Token>();

        private void generateEndToken(List<Token> tokenSet)
        {
        }

        #region ReadingWriting
        private void ReadFile(string path)
        {
            StreamReader s1 = new StreamReader(path);

            while (!s1.EndOfStream)
            {
                linesOfCode.Add(s1.ReadLine()+" ");
            }
            s1.Close();
        }
        private void WriteToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))
            {
                foreach (var item in tokenSet)
                {
                    writer.WriteLine(item);
                }
            }
        }
        #endregion
        

        

        private void BreakWords()
        {
            string temp = "";
            string tempChar = "";
            bool checkWord = true;
            bool checkChar = true;
            int sizeChar = 0;
            int lineNumber = 0;
            char backSlash = '\\';
            //int sp = 2;
            foreach (string lines in linesOfCode)
            {
                lineNumber++;
                
                foreach (char currentCharacter in lines)
                {
                    
                    //Comments ka scene banao jani
                    if(currentCharacter==';'&&checkWord)
                    {
                        if (temp!="")
                        {
                            IdentifyWords(lineNumber, temp);
                            temp = "";
                        }
                        checkWord = false;
                    }
                    else if (!checkWord)
                    {
                        if (currentCharacter == ';')
                        {
                            checkWord = true;
                            continue;
                        }
                        else
                            continue;
                    }
                        //temp me agar ' aagay tou ye check chalay jab tak checkChar true hai
                    else if (checkChar == false)
                    {
                        if (sizeChar < 3)
                        {
                            tempChar += currentCharacter;
                            sizeChar++;
                            continue;
                        }
                        try
                        {
                            if (sizeChar == 3 && tempChar[1] != '\\')
                            {
                                //string word = tempChar.Substring(1,1);
                                IdentifyWords(lineNumber, tempChar);
                                temp = tempChar = "";
                                checkChar = true;
                                sizeChar = 0;
                            }
                            else if (sizeChar == 4 && tempChar[1] == '\\')
                            {
                                //string word = tempChar.Substring(1,1);
                                IdentifyWords(lineNumber, tempChar);
                                temp = tempChar = "";
                                checkChar = true;
                                sizeChar = 0;
                            }
                            else if (sizeChar == 5)
                            {
                                IdentifyWords(lineNumber, tempChar);
                                if (currentCharacter == '\'')
                                {
                                    tempChar += currentCharacter;
                                    sizeChar = 1;
                                    continue;
                                }
                                else
                                {
                                    temp += currentCharacter;
                                    IdentifyWords(lineNumber, temp);
                                    checkChar = true;
                                }
                            }
                            else
                            {
                                tempChar += currentCharacter;
                                sizeChar++;
                                continue;
                            }
                        }
                        catch (Exception e)
                        {
                            string error = "invalid lexeme";
                            IdentifyWords(lineNumber, e.Message+error);

                        }

                    }

                    else if (checkWord&&checkChar)
                    {
                        if (currentCharacter == '#')
                        {
                            if (temp != "")
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = "";
                            }
                            break;
                        }
                        else if(currentCharacter!=' ' &&
                            myLanguage.Pointers.Contains(temp))
                        {
                            IdentifyWords(lineNumber, temp);
                            temp = currentCharacter.ToString();
                        }

                       
                            
                        //checks for a word
                        else if (currentCharacter != ' '
                            && !myLanguage.Operators.Contains(currentCharacter.ToString())
                            && !myLanguage.Punctuators.Contains(currentCharacter.ToString())
                            && !myLanguage.Operators.Contains(temp))
                        {
                            if ((currentCharacter != '\'' && currentCharacter != '\"')
                                &&myLanguage.Punctuators.Contains(temp))
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = currentCharacter.ToString();
                            }
                            else if (currentCharacter != '\'' && currentCharacter != '\"')
                            {
                                temp += currentCharacter;
                            }

                            else if (currentCharacter == '\"')
                            {
                                temp += currentCharacter;
                            }
                            else if (currentCharacter == '\'')
                            {
                                tempChar += currentCharacter;
                                sizeChar++;
                                //temp += currentCharacter;
                                checkChar = false;
                                continue;
                            }
                            else if (temp.Contains("\'".ToString()))
                            {
                                temp += currentCharacter;
                                if (temp.Contains(backSlash.ToString()) && temp.Length == 4)
                                {
                                    IdentifyWords(lineNumber, temp);
                                    temp = currentCharacter.ToString();
                                }
                                else
                                {
                                    IdentifyWords(lineNumber, temp);
                                    temp = currentCharacter.ToString();
                                }
                            }
                        }


                        //Checks for space
                        else if (currentCharacter == ' ')
                        {
                            if (temp != "")
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = "";
                            }
                        }
                        //Check operator
                        #region operators
                        else if (myLanguage.Operators.Contains(currentCharacter.ToString()) || myLanguage.Operators.Contains(temp))
                        {
                            if (temp != "" && !myLanguage.Operators.Contains(temp))
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = currentCharacter.ToString();
                            }
                            else if (temp == "")
                            {
                                temp += currentCharacter.ToString();
                            }
                            else if (temp != "")
                            {
                                if (temp == currentCharacter.ToString())
                                {
                                    temp += currentCharacter;
                                    IdentifyWords(lineNumber, temp);
                                    temp = "";
                                }
                                else if ((temp == "=" || temp == "+" || temp == "-" || temp == "!" || temp == "*" || temp == "/"
                                    || temp == "%" || temp == ">" || temp == "<" || temp == "!") && currentCharacter.ToString() == "=")
                                {
                                    temp += currentCharacter;
                                    IdentifyWords(lineNumber, temp);
                                    temp = "";
                                }
                                else if ((temp == "+" || temp == "-") && (currentCharacter >= 48 && currentCharacter <= 57))
                                {
                                    temp += currentCharacter;
                                }
                                else
                                {
                                    IdentifyWords(lineNumber, temp);
                                    temp = currentCharacter.ToString();
                                }
                            }
                            

                            else
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = currentCharacter.ToString();
                            }

                        }
                        #endregion


                        //Puctuators Check
                        #region puctuators
                        else if (myLanguage.Punctuators.Contains(currentCharacter.ToString()))
                        {
                            if (temp != "")
                            {
                                IdentifyWords(lineNumber, temp);
                                temp = currentCharacter.ToString();
                            }
                            else
                            {
                                IdentifyWords(lineNumber, currentCharacter.ToString());
                            }
                        }
                        else
                        {
                            IdentifyWords(lineNumber, temp);
                            temp = currentCharacter.ToString();

                        }
                        #endregion
                    }
                 
                }
            }
        }


        private void IdentifyWords(int lineNumber, string temp)
        {
            //char[] Numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string word = temp;
            Token token = new Token(lineNumber, word);

            if (myLanguage.IsPunctuator(word))
            {
                token.classPart = word;
            }
            else if (myLanguage.IsPointer(word))
            {
                token.classPart = word;
            }
            else if (myLanguage.IsKeyword(word))
            {
                if (word == "Alpha" || word == "Digit" || word == "Word" || word == "Float")
                {
                    token.classPart = ClassPart.DT.ToString();
                }
                else if (word == "till")
                {
                    token.classPart = ClassPart.till.ToString();
                }
                else if (word == "untill")
                {
                    token.classPart = ClassPart.untill.ToString();
                }
                else if (word == "Case")
                {
                    token.classPart = ClassPart.Case.ToString();
                }
                else if (word == "then")
                {
                    token.classPart = ClassPart.then.ToString();
                }
                else if (word == "ProgramStart")
                {
                    token.classPart = ClassPart.ProgramStart.ToString();
                }
                else if (word == "End")
                {
                    token.classPart = ClassPart.End.ToString();
                }
                else if (word == "Goback")
                {
                    token.classPart = ClassPart.Goback.ToString();

                }
                else if (word == "Class")
                {
                    token.classPart = ClassPart.Class.ToString();
                }
                else if (word == "create")
                {
                    token.classPart = ClassPart.create.ToString();
                }
                else if (word == "Void")
                {
                    token.classPart = ClassPart.Void.ToString();
                }
                else if (word == "main")
                {
                    token.classPart = ClassPart.main.ToString();
                }
                else if (word == "call")
                {
                    token.classPart = ClassPart.call.ToString();
                }
                else if (word == "For")
                {
                    token.classPart = ClassPart.For.ToString();
                }
            }
            else if (myLanguage.IsOperator(word))
            {
                if (word == "+" || word == "-")
                {
                    token.classPart = ClassPart.PM.ToString();
                }
                else if (word == "/" || word == "%"||word=="*")
                {
                    token.classPart = ClassPart.MDM.ToString();
                }
                else if (word == "&&" || word == "||" || word == "!")
                {
                    token.classPart = word;
                }
                else if (word == "<" || word == ">" || word == "<=" || word == ">="
                    || word == "==" || word == "!=" )
                {
                    token.classPart = ClassPart.Rel_OP.ToString();
                }
                else if (word == "+=" || word == "-=" || word == "%="
                    || word == "*=" || word == "/=" || word == "=")
                {
                    token.classPart = ClassPart.Asg_OP.ToString();
                }
                else if (word == "++" || word == "--")
                {
                    token.classPart = ClassPart.INC_DEC.ToString();
                }
            }
            else if (myLanguage.IsIdentifier(word))
            {
                token.classPart = ClassPart.ID.ToString();
            }
            else if (myLanguage.IsAlphaConstant(word))
            {
                token.classPart = ClassPart.Alpha_Const.ToString();
            }
            else if (myLanguage.IsFloat(word))
            {
                token.classPart = ClassPart.Float_Const.ToString();
            }
            else if (myLanguage.IsDigitConstant(word))
            {
                token.classPart = ClassPart.Digit_Const.ToString();
            }
            else if (myLanguage.IsWordConstant(word))
            {
                token.classPart = ClassPart.Word_Const.ToString();
            }
            
            else
            {
                token.classPart = "INVALID LEXEME";
            }

            tokenSet.Add(token);

        }

       

    }
}
