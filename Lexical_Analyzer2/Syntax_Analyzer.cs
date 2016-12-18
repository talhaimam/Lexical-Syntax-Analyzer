using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexical_Analyzer2
{
    class Syntax_Analyzer
    {
        List<Token> j = new List<Token>();
        int tn = 0;
        public bool ParseTokens(List<Token> tokenSet)
        {
            this.j = tokenSet;
            return Start();
        }
        public bool Start()//1
        {
            if (j[tn].classPart == ClassPart.ProgramStart.ToString()) //solution set
            {
                tn++;
                if (j[tn].classPart == "[")
                {
                    tn++;
                    if (defs())
                    {
                        tn++;
                        if (j[tn].classPart == "main")
                        {
                            tn++;
                            if (j[tn].classPart == "(")
                            {
                                tn++;
                                if (j[tn].classPart == ")")
                                {
                                    tn++;
                                    if (j[tn].classPart == "[")
                                    {
                                        tn++;
                                        if (m_st())
                                        {
                                            tn++;
                                            if (j[tn].classPart == "]")
                                            {
                                                tn++;
                                                if (Class())
                                                {
                                                    tn++;
                                                    if (j[tn].classPart == "]")
                                                    {
                                                        tn++;
                                                        if (j[tn].classPart == "End")
                                                        {
                                                            return true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool defs()//2
        {
            if(j[tn].classPart == ClassPart.DT.ToString())                     //solution set
            {
                tn++;
                if (def2())
                {
                    tn++;
                    if (defs())
                    {
                        return true;
                    }
                }
            }
            else if (j[tn].classPart == ClassPart.Void.ToString())
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (j[tn].classPart == ")")
                    {
                        tn++;
                        if (param())
                        {
                            tn++;
                            if (j[tn].classPart == ")")
                            {
                                tn++;
                                if (Body())
                                {
                                    tn++;
                                    if (defs())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //else if (j[tn].classPart == ClassPart.main.ToString())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chkE
        public bool def2()//3
        {
            if(isID())
            {
                tn++;
                if (def3())
                {
                    return true;
                }
            }
            else if(j[tn].classPart == "?")
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (INIT_2())
                    {
                        tn++;
                        if (LIST_2())
                        {
                            return true;
                        }
                    }
                }
            }
            //else if (j[tn].classPart == ClassPart.DT.ToString())
            //{
            //    tn++;
            //    if (def2())
            //    {
            //        tn++;
            //        if (defs())
            //        {
            //            return true;
            //        }
            //    }
            //}
            //else if (j[tn].classPart == ClassPart.Void.ToString())
            //{
            //    tn++;
            //    if (isID())
            //    {
            //        tn++;
            //        if (j[tn].classPart == ")")
            //        {
            //            tn++;
            //            if (param())
            //            {
            //                tn++;
            //                if (j[tn].classPart == ")")
            //                {
            //                    tn++;
            //                    if (Body())
            //                    {
            //                        tn++;
            //                        if (defs())
            //                        {
            //                            return true;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (j[tn].classPart == ClassPart.main.ToString())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool def3()//4
        {
            if(Dec_1_A())
            {
                return true;
            }
            else if (j[tn].classPart == "(")
            {
                tn++;
                if (param())
                {
                    tn++;
                    if (j[tn].classPart == ")")
                    {
                        tn++;
                        if (Body())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool param()//5
        {
            if(j[tn].classPart ==ClassPart.Void.ToString())
            {
                return true;
            }
            else if(isDT())
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (param2())
                    {
                        return true;
                    }
                }
            }
            //else if(j[tn].classPart ==")")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool param2()//6
        {
            if (j[tn].classPart == ",")
            {
                tn++;
                if (isDT())
                {
                    tn++;
                    if (isID())
                    {
                        tn++;
                        if (param2())
                        {
                            return true;
                        }
                    }
                }
            }
            //else if(j[tn].classPart ==")")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        }  //chk E
        public bool Body()//7
        {
            if(j[tn].classPart ==":")
            {
                return true;
            }
            else if(j[tn].classPart =="[")
            {
                tn++;
                if (m_st())
                {
                    tn++;
                    if (j[tn].classPart == "]")
                    {
                        return true;
                    }
                }
            }
            else if(s_st())
            {
                return true;
            }
            return false;
        }
        public bool m_st()//8
        {
            if (s_st())
            {
                tn++;
                if (m_st())
                {
                    return true;
                }
            }
            //else if(j[tn].classPart =="]")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool s_st()//9
        {
            if(INC_DEC())
            {
                tn++;
                if(isID())
                {
                    tn++;
                    if (j[tn].classPart == ":")
                    {
                        return true;
                    }
                }
            }
            else if (Case_then())
            {
                return true;
            }
            else if (till())
            {
                return true;
            }
            else if (untill())
            {
                return true;
            }
            else if (Obj_Crt())
            {
                return true;
            }
            else if (Dec())
            {
                return true;
            }
            else if (fn_call())
            {
                return true;
            }
            else if (isID())
            {
                tn++;
                if (s_st1())
                {
                    return true;
                }
            }
            return false;
        }
        public bool s_st1()//10
        {
            if(ORE())
            {
                tn++;
                if (j[tn].classPart == ":")
                {
                    return true;
                }
            }
            else if(j[tn].classPart == ClassPart.Asg_OP.ToString()
                ||j[tn].classPart == ClassPart.PM.ToString()
                || j[tn].classPart == ClassPart.MDM.ToString()
                || j[tn].classPart == ClassPart.Rel_OP.ToString())
            {
                tn++;
                if (ORE())
                {
                    tn++;
                    if (j[tn].classPart == ":")
                    {
                        return true;
                    }
                }
            }
            else if(LIST())
            {
                return true;
            }
            else if(j[tn].classPart ==".")
            {
                tn++;
                if (Dec2())
                {
                    return true;
                }
            }
            else if(INC_DEC())
            {
                return true;
            }
            return false;
        }
        public bool Dec()//12
        {
            if (isDT())                                              //solution set
            {
                tn++;
                if (Dec_1())
                {
                    return true;
                }
            }
            return false;
        }
        public bool Dec_1()//13
        {
            if(isID())
            {
                tn++;
                if (Dec_1_A())
                {
                    return true;
                }
            }
            else if(j[tn].classPart == "?")
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (INIT_2())
                    {
                        tn++;
                        if (LIST_2())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool Dec_1_A()//14
        {
            if(INIT())
            {
                tn++;
                if (LIST())
                {
                    return true;
                }
            }
            else if(j[tn].classPart ==".")
            {
                tn++;
                if (Dec2())
                {
                    return true;
                }
            }
            else if(INC_DEC())
            {
                return true;
            }
            return false;
        }
        public bool INIT()//15
        {
            if (isAOP()) //solution set
            {
                tn++;
                if (ORE())
                {
                    return true;
                }
            }
            //else if (LIST())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        }  //chk E
        public bool LIST()//16
        {
            if (j[tn].classPart == ":")
            {
                return true;
            }
            else if (j[tn].classPart == ",")
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (INIT())
                    {
                        tn++;
                        if (LIST())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool Dec2()//17
        {
            if (isID())
            {
                tn++;
                if (Dec_2_A())
                {
                    return true;
                }
            }
            return false;
        }
        public bool Dec_2_A()//18
        {
            if(j[tn].classPart ==":")
            {
                return true;
            }
            else if (j[tn].classPart == "(")
            {
                tn++;
                if (param())
                {
                    tn++;
                    if (j[tn].classPart == ")")
                    {
                        tn++;
                        if (j[tn].classPart == ":")
                        {
                            return true;
                        }
                    }
                }
            }
            else if (INC_DEC())
            {
                return true;
            }
                    
            return false;
        }
        public bool INC_DEC()//19
        {
            if (j[tn].classPart == ClassPart.INC_DEC.ToString())
            {
                tn++;
                if (j[tn].classPart == ":")
                {
                    return true;
                }
            }
            return false;
        }
        public bool INIT_2()//20
        {
            if (j[tn].classPart == ClassPart.Asg_OP.ToString()) //solution set
            {
                tn++;
                if (j[tn].classPart == "~")
                {
                    tn++;
                    if (isID())
                    {
                        return true;
                    }
                }

            }
            //else if (LIST_2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool LIST_2()//21
        {
            if (j[tn].classPart == ":")
            {
                return true;
            }
            else if (j[tn].classPart == ",")
            {
                tn++;
                if (j[tn].classPart == "?")
                {
                    tn++;
                    if (isID())
                    {
                        tn++;
                        if (INIT())
                        {
                            tn++;
                            if (LIST())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool Obj_Crt()//22
        {
            if (j[tn].classPart == ClassPart.create.ToString())
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (j[tn].classPart == ClassPart.For.ToString())
                    {
                        tn++;
                        if (isID())
                        {
                            tn++;
                            if (j[tn].classPart == ":")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool ORE()//23
        {
            if (ANDE())
            {
                tn++;
                if (ORE2())
                {
                    return true;
                }
            }
                return false;
        }
        public bool ORE2()//24
        {
            if (j[tn].classPart == "||") //SS
            {
                tn++;
                if (ANDE())
                {
                    tn++;
                    if (ORE2())
                    {
                        return true;
                    }
                }
            }
            //else if (j[tn].classPart == ":")
            //{
            //    return true;
            //}
            //else if (j[tn].classPart == ")")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool ANDE()//25
        {
            if (RE())
            {
                tn++;
                if (ANDE2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool ANDE2()//26
        {
            if (j[tn].classPart == "&&")
            {
                tn++;
                {
                    tn++;
                    if (RE())
                    {
                        tn++;
                        if (ANDE2())
                        {
                            return true;
                        }
                    }
                }
            }
            //else if (ORE2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } // chk E
        public bool RE()//27
        {
            if (E())
            {
                tn++;
                if (RE2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool RE2()//28
        {
            if (j[tn].classPart == ClassPart.Rel_OP.ToString())
            {
                tn++;
                if (E())
                {
                    tn++;
                    if (RE2())
                    {
                        return true;
                    }
                }
            }
            //else if (ANDE2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } // chk E
        public bool E()//29
        {
            if (T())
            {
                tn++;
                if (E2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool E2()//30
        {
            if (j[tn].classPart == ClassPart.PM.ToString())  //SS
            {
                tn++;
                if (T())
                {
                    tn++;
                    if (E2())
                    {
                        return true;
                    }
                }
            }
            //else if (RE2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } // chk E
        public bool T()//31
        {
            if (F())
            {
                tn++;
                if (T2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool T2()//32
        {
            if (j[tn].classPart == ClassPart.MDM.ToString())  //SS
            {
                tn++;
                if (F())
                {
                    tn++;
                    if (T2())
                    {
                        return true;
                    }
                }
            }
            //else if (E2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //che E
        public bool F()//33
        {
            if (isID())
            {
                tn++;
                if (F5())
                {
                    return true;
                }
            }
            else if (j[tn].classPart == "(")
            {
                tn++;
                if (ORE())
                {
                    tn++;
                    if (j[tn].classPart == ")")
                    {
                        return true;
                    }
                }
            }
            else if (j[tn].classPart == "!")
            {
                tn++;
                if (F())
                {
                    return true;
                }
            }
            else if (j[tn].classPart == ClassPart.INC_DEC.ToString())
            {
                tn++;
                if (isID())
                {
                    return true;
                }
            }
            else if (isConstant())
            {
                return true;
            }
            return false;
        }
        public bool F5()//34
        {

            if (j[tn].classPart == "(")
            {
                tn++;
                if (param())
                {
                    tn++;
                    if (j[tn].classPart == ")")
                    {
                        return true;
                    }
                }
            }
            else if (j[tn].classPart == ".")
            {
                tn++;
                if (F())
                {
                    return true;
                }
            }
            else if (j[tn].classPart == ClassPart.INC_DEC.ToString())
            {
                return true;
            }
            //else if (T2())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        }  // chk E
        public bool fn_call()//35
        {
            if (j[tn].classPart == ClassPart.call.ToString())
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (j[tn].classPart == "(")
                    {
                        tn++;
                        if (param3())
                        {
                            tn++;
                            if (j[tn].classPart == ")")
                            {
                                tn++;
                                if (j[tn].classPart == ":")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool param3()//36
        {
            if (isID())
            {
                tn++;
                if (param4())
                {
                    return true;
                }
            }
            else if (isConstant())
            {
                tn++;
                if (param4())
                {
                    return true;
                }
            }
            //else if (j[tn].classPart == ")")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }

            return false;
        } //chk E
        public bool param4()//46 
        {
            if (j[tn].classPart == ",")
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (param4())
                    {
                        return true;
                    }
                }
            }
            else if (j[tn].classPart == ",")
            {
                tn++;
                if (isConstant())
                {
                    tn++;
                    if (param4())
                    {
                        return true;
                    }
                }
            }
            //else if (j[tn].classPart == ")")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        }  //chk E
        public bool untill()//37
        {
            if (j[tn].classPart == ClassPart.untill.ToString())
            {
                tn++;
                if (j[tn].classPart == "(")
                {
                    tn++;
                    if (ORE())
                    {
                        tn++;
                        if (j[tn].classPart == ")")
                        {
                            tn++;
                            if (Body())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool till()//38
        {
            if (j[tn].classPart == ClassPart.till.ToString())
            {
                tn++;
                if (j[tn].classPart == "(")
                {
                    tn++;
                    if (F1())
                    {
                        tn++;
                        if (ORE())
                        {
                            tn++;
                            if (j[tn].classPart == ":")
                            {
                                tn++;
                                if (F3())
                                {
                                    tn++;
                                    if (j[tn].classPart == ")")
                                    {
                                        tn++;
                                        if (Body())
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool F1()//39
        {
            if(Dec())
            {
                return true;
            }
            else if(Dec_1())
            {
                return true;
            }
            else if (j[tn].classPart == ":")
            {
                return true;
            }
            return false;
        }
        //F2 replaced by ORE
        public bool F3()//41
        {
            if (j[tn].classPart == ClassPart.INC_DEC.ToString())
            {
                tn++;
                if (isID())
                {
                    return true;
                }
            }
            else if (isID())
            {
                tn++;
                if (F3_A())
                {
                    return true;
                }
            }
            return false;
        }
        public bool F3_A()//42
        {
            if (j[tn].classPart == ClassPart.INC_DEC.ToString())
            {
                return true;
            }
            else if (j[tn].classPart == ClassPart.Asg_OP.ToString())
            {
                tn++;
                if (ORE())
                {
                    return true;
                }
            }
            return false;
        }
        public bool Case_then()//43
        {
            if (j[tn].classPart == ClassPart.Case.ToString())
            {
                tn++;
                if (j[tn].classPart == "(")
                {
                    tn++;
                    if (ORE())
                    {
                        tn++;
                        if (j[tn].classPart == ")")
                        {
                            tn++;
                            if (Body())
                            {
                                tn++;
                                if (then())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool then()//44
        {
            if (j[tn].classPart == ClassPart.then.ToString())
            {
                tn++;
                if (Body())
                {
                    return true;
                }
            }
            //else if (s_st())
            //{
            //    return true;
            //}
            //else if (j[tn].classPart == ClassPart.main.ToString())
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
        public bool Class()//45
        {
            if (j[tn].classPart == ClassPart.Class.ToString())
            {
                tn++;
                if (isID())
                {
                    tn++;
                    if (j[tn].classPart == "[")
                    {
                        tn++;
                        if (m_st())
                        {
                            tn++;
                            if (j[tn].classPart == "]")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            //else if(j[tn].classPart=="]")
            //{
            //    return true;
            //}
            else
            {
                tn--;
                return true;
            }
            return false;
        } //chk E
       
// Utility Methods
        public bool isAOP()
        {
            if (j[tn].classPart == ClassPart.Asg_OP.ToString())
                return true;
            return false;
        }
        public bool isID()
        {
            if (j[tn].classPart == ClassPart.ID.ToString())
                return true;
            else
                return false;
        }
        public bool isDT()
        {
            if (j[tn].classPart == ClassPart.DT.ToString())
                return true;
            else
                return false;
        }
        public bool isConstant()
        {
            if (j[tn].classPart == ClassPart.Digit_Const.ToString() || j[tn].classPart == ClassPart.Alpha_Const.ToString() ||
                j[tn].classPart == ClassPart.Float_Const.ToString() || j[tn].classPart == ClassPart.Word_Const.ToString())
                return true;
            else
                return false;
        }
    }
}
