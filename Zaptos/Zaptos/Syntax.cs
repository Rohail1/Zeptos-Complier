using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
    class Syntax
    {

        MyListDT mylist = new MyListDT();
        int i = 0;
        public Syntax()
        {
            // TODO: Complete member initialization
        }
        public void tokkensetExcater(List<string> tkset)
        {
            List<string> Classlist = new List<string>();
            List<string> ValueList = new List<string>();
            List<string> lineNumber = new List<string>();
            foreach (string tks in tkset)
            {
                string temperary = "";
                bool comma_Checker = false;
                bool class_value_checker = false;
                for (int i = 0; i < tks.Length; i++)
                {
                    char temp = tks[i];
                    if (temp == '(' || temp == ')')
                    {
                        if (i == 1 || i == 3)
                        {
                            if (i == 1)
                            {
                                Classlist.Add(temp.ToString());
                            }
                            else
                            {
                                ValueList.Add(temp.ToString());
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                    else if (temp == ',')
                    {
                        if (i == 1 || i == 3)
                        {
                            if (i == 1)
                            {
                                Classlist.Add(temp.ToString());
                                comma_Checker = true;
                            }
                            else
                            {
                                if (comma_Checker)
                                {
                                    ValueList.Add(temp.ToString());
                                }
                                else
                                {
                                    Classlist.Add(temperary);
                                    temperary = "";
                                    class_value_checker = true;
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            if (!class_value_checker)
                            {
                                if (!string.IsNullOrEmpty(temperary))
                                {
                                    Classlist.Add(temperary);
                                    temperary = "";
                                    class_value_checker = true;
                                }

                            }

                            else
                            {
                                if (!string.IsNullOrEmpty(temperary))
                                {
                                    ValueList.Add(temperary);
                                    temperary = "";
                                }

                            }
                        }
                    }
                    else
                    {
                        temperary = temperary + temp.ToString();
                    }
                }
                lineNumber.Add(temperary);
            }
            mylist.ClassList = Classlist;
            mylist.ValueList = ValueList;
            mylist.LineNumberList = lineNumber;
   
        }
        List<string> wordbreaker(string line)
        {
            List<string> wordlist = new List<string>();
            string temperary = "";
            for (int i = 0; i < line.Length; i++)
            {
                char temp = line[i];
                if (temp == ' ')
                {
                    wordlist.Add(temperary);
                    temperary = "";
                }
                else
                {
                    temperary += temperary;
                }
            }
            wordlist.Add(temperary);
            return wordlist;
        }
        public List<string> SytaxAnalyzer()
        {
            List<string> Error_List = new List<string>();

            if (global_Space())
            {
               // i++;
                if (mylist.ClassList.ElementAt(i) == "MainClass")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        i++;
                        if (Main_Func())
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                i++;
                                if (global_Space())
                                {
                                  //  i++;
                                    i = 9999;
                 
                                }
                                else
                                {
                                    Error_List.Add(i.ToString());
                                }
                            }
                            else
                            {
                                Error_List.Add(i.ToString());
                            }
                        }
                        else
                        {
                            Error_List.Add(i.ToString());
                        }
                    }
                    else
                    {
                        Error_List.Add(i.ToString());
                    }
                }
                else
                {
                    Error_List.Add(i.ToString());
                }

            }
            else
            {
                Error_List.Add(mylist.LineNumberList.ElementAt(i).ToString());
            }


            return Error_List;
        }
        bool Main_Func()
        {
            if (mylist.ClassList.ElementAt(i) == "func")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "Main")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        i++;
                        if (MST())
                        {

                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool global_Space()
        {

            if (mylist.ClassList.ElementAt(i) == "Class")
            {
                if (Class())
                {
                    i++;
                    if (global_Space())
                    {
                        return true;     
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Struct")
            {
                if (Struct())
                {
                    i++;
                    if (global_Space())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else if (mylist.ClassList.ElementAt(i) == "MainClass")
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "End_Marker")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        bool Class()
        {
            if (mylist.ClassList.ElementAt(i) == "Class")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        i++;
                        if (class_body())
                        {

                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool class_body()
        {


            if (declearation())
            {
                i++;
                if (class_body())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (func_dec())
            {
                i++;
                if (class_body())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "}")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        bool declearation()
        {
            if (mylist.ClassList.ElementAt(i) == "var")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (dec_var_arr())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool dec_var_arr()
        {
            if (Init())
            {

                if (List())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (mylist.ClassList.ElementAt(i) == "[")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "]")
                    {
                        i++;
                        if (Init3())
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == ";")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (mylist.ClassList.ElementAt(i) == "Int_Const")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "]")
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == ";")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }
        bool Init()
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (Init2())
                {
                    i++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ";" || mylist.ClassList.ElementAt(i) == ",")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        bool Init2()
        {
            if (Const())
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Init())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (OR_OP())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool List()
        {
            if (mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (Init())
                    {
                        i++;
                        if (List())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        bool Init3()
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "[")
                {
                    i++;
                    if (ID_Const())
                    {
                       // i++;
                        if (More_Elements())
                        {

                            if (mylist.ClassList.ElementAt(i) == "]")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool More_Elements()
        {
            if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (ID_Const())
                {
                  //  i++;
                    if (More_Elements())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "]")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool ID_Const()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                return true;
            }
            else if (Const())
            {
                i++;
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Const()
        {
            if (mylist.ClassList.ElementAt(i) == "Int_Const" || mylist.ClassList.ElementAt(i) == "str_Const" || mylist.ClassList.ElementAt(i) == "Flt_Const")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Object_dec()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (Object_dec2())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Object_dec2()
        {
            if (mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "new")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "ID")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "(")
                        {
                            i++;
                            if (Para_Less())
                            {
                                if (mylist.ClassList.ElementAt(i) == ")")
                                {
                                    i++;
                                    if (mylist.ClassList.ElementAt(i) == ";")
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Struct()
        {
            if (mylist.ClassList.ElementAt(i) == "Struct")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        i++;
                        if (Struct_Body())
                        {

                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                i++;
                                if (mylist.ClassList.ElementAt(i) == ";")
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Struct_Body()
        {
            if (declearation())
            {
                i++;
                if (Struct_Body())
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else if (func_dec())
            {
                i++;
                if (Struct_Body())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "}")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        bool Body()
        {
            if (mylist.ClassList.ElementAt(i) == "{")
            {
                i++;
                if (MST())
                {
                    if (mylist.ClassList.ElementAt(i) == "}")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool MST()
        {
            if (SST())
            {
                if (mylist.ClassList.ElementAt(i) == "return")
                {
                    return true;
                }
                else
                {
                    i++;
                    if (MST())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }        
                }
                
            }
            else if (mylist.ClassList.ElementAt(i) == "}" || mylist.ClassList.ElementAt(i) == "break" || mylist.ClassList.ElementAt(i) == "return")
            {
                return true;
            }
            else
            {
                return false;

            }

        }
        bool If()
        {
            if (mylist.ClassList.ElementAt(i) == "if")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (Cond())
                    {

                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            if (Body())
                            {
                                i++;
                                if (O_Else())
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool O_Else()
        {
            if (mylist.ClassList.ElementAt(i) == "else")
            {
                i++;
                if (Body())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (MST())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool While()
        {
            if (mylist.ClassList.ElementAt(i) == "while")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (Cond())
                    {

                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            if (Body())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool For()
        {
            if (mylist.ClassList.ElementAt(i) == "for")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (Assign_Dec())
                    {
                        i++;
                        if (for_Cond())
                        {

                            if (mylist.ClassList.ElementAt(i) == ";")
                            {
                                i++;
                                if (Assign_INC_Dec())
                                {

                                    if (mylist.ClassList.ElementAt(i) == ")")
                                    {
                                        i++;
                                        if (Body())
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Assign_Dec()
        {
            if (mylist.ClassList.ElementAt(i) == "var")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "Int_Const" || mylist.ClassList.ElementAt(i) == "Flt_Const")
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == ";")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (Assignment())
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool for_Cond()
        {
            if (Cond())
            {
                //i++;
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Assign_INC_Dec()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Assign_INC_Dec2())
                {
                    i++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (var_arr())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Assign_INC_Dec2()
        {
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (var_arr())
                    {

                        if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                        {
                            i++;
                            if (OR_OP())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (var_arr())
            {

                if (Assign_INC_Dec3())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Assign_INC_Dec3()
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (OR_OP())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Do_While()
        {
            if (mylist.ClassList.ElementAt(i) == "do")
            {
                i++;
                if (Body())
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "while")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "(")
                        {
                            i++;
                            if (Cond())
                            {

                                if (mylist.ClassList.ElementAt(i) == ")")
                                {
                                    i++;
                                    if (mylist.ClassList.ElementAt(i) == ";")
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Switch()
        {
            if (mylist.ClassList.ElementAt(i) == "switch")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (ID_Const())
                    {
                       // i++;
                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == "{")
                            {
                                i++;
                                if (Case())
                                {

                                    if (Default())
                                    {
                                        i++;
                                        if (mylist.ClassList.ElementAt(i) == "}")
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Case()
        {
            if (mylist.ClassList.ElementAt(i) == "case")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == ":")
                    {
                        i++;
                        if (Case_Body())
                        {
                            i++;
                            if (Case())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "default")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Default()
        {
            if (mylist.ClassList.ElementAt(i) == "default")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == ":")
                {
                    i++;
                        if (MST())
                        {
                          //  i++;
                            if (mylist.ClassList.ElementAt(i) == "break")
                            {
                                i++;
                                if (mylist.ClassList.ElementAt(i) == ";")
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }
        bool Case_Body()
        {
            if (MST())
            {
                //i++;
                if (mylist.ClassList.ElementAt(i) == "break")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == ";")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Cond()
        {
            if (ID_Const())
            {
              //  i++;
                if (Cond2())
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Cond2()
        {
            if (mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                i++;
                if (ID_Const())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool func_dec()
        {
            if (mylist.ClassList.ElementAt(i) == "func")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "(")
                    {
                        i++;
                        if (Para_Less())
                        {
                           // i++;
                            if (mylist.ClassList.ElementAt(i) == ")")
                            {
                                i++;
                                if (mylist.ClassList.ElementAt(i) == "{")
                                {
                                    i++;
                                    if (Func_Body())
                                    {
                                        i++;
                                        if (mylist.ClassList.ElementAt(i) == "}")
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {

                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Func_Body()
        {
            if (MST())
            {
             //   i++;
                if (mylist.ClassList.ElementAt(i) == "return")
                {
                    i++;
                    if (OR_OP())
                    {
                      //  i++;
                        if (mylist.ClassList.ElementAt(i) == ";")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Para_Less()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Para_Less2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Const())
            {
                i++;
                if (Para_Less2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        bool Para_Less2()
        {
            if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (Para_Less())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Func_method_Calling()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Func_method_Calling2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Func_method_Calling2()
        {
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "(")
                    {
                        i++;
                        if (Para_Less())
                        {
                          //  i++;
                            if (mylist.ClassList.ElementAt(i) == ")")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less())
                {
                   // i++;
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool Assignment()
        {
            if (var_Objvar())
            {

                if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                {
                    i++;
                    if (OR_OP())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool var_Objvar()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (var_Objvar2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool var_Objvar2()
        {
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (var_arr())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (var_arr())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool var_arr()
        {
            if (mylist.ClassList.ElementAt(i) == "[")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "Int_Const")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "]")
                    {
                        i++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == "Assig_Op" || mylist.ClassList.ElementAt(i) == "Inc_dec" || mylist.ClassList.ElementAt(i) == "Relat_Op" || mylist.ClassList.ElementAt(i) == ";" || mylist.ClassList.ElementAt(i) == "M_D_M"  || mylist.ClassList.ElementAt(i) == "Arith_Op")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool OR_OP()
        {
            if (And_OP())
            {
               // i++;
                if (OR_OP1())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool OR_OP1()
        {
            if (mylist.ClassList.ElementAt(i) == "||")
            {
                i++;
                if (And_OP())
                {
                  //  i++;
                    if (OR_OP1())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool And_OP()
        {
            if (RE())
            {
                //i++;
                if (And_OP1())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool And_OP1()
        {
            if (mylist.ClassList.ElementAt(i) == "&&")
            {
                i++;
                if (RE())
                {
                  //  i++;
                    if (And_OP1())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "||" || mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool RE()
        {
            if (AE())
            {
                //i++;
                if (RE1())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool RE1()
        {
            if (mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                i++;
                if (AE())
                {
                  //  i++;
                    if (RE1())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "&&" || mylist.ClassList.ElementAt(i) == "||" || mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool AE()
        {
            if (T())
            {
                //i++;
                if (AE1())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool AE1()
        {
            if (mylist.ClassList.ElementAt(i) == "Arith_Op")
            {
                i++;
                if (T())
                {
               //     i++;
                    if (AE1())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "&&" || mylist.ClassList.ElementAt(i) == "||" || mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";" || mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool T()
        {
            if (F())
            {
             //   i++;
                if (T1())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool T1()
        {
            if (mylist.ClassList.ElementAt(i) == "M_D_M")
            {
                i++;
                if (F())
                {
                    //i++;
                    if (T1())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Arith_Op" || mylist.ClassList.ElementAt(i) == "&&" || mylist.ClassList.ElementAt(i) == "||" || mylist.ClassList.ElementAt(i) == ")" || mylist.ClassList.ElementAt(i) == ";" || mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool F()
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (F1())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (Const())
            {
                i++;
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (OR_OP())
                {
                    return true;
                }
                else
	            {
                    return false;
	            }
            }
            else if (F2())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool F1()
        {
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (F3())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else if (var_arr())
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less())
                {
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool F2()
        {
            if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (var_Objvar())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool F3()
        {
            if (var_arr())
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less())
                {
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        bool SST_INC_DEC()
        {
            if (var_Objvar())
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "Inc_dec")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == ";")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (var_Objvar())
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == ";")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool SST()
        {
            if (declearation() || While() || For() || If() || Do_While() || Switch())
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (var_Objvar())
                {
                    if (mylist.ClassList.ElementAt(i) == ";")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "ID")
            {
                if (SST2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //else if (mylist.ClassList.ElementAt(i) == "Break")
            //{
            //    i++;
            //    if (mylist.ClassList.ElementAt(i) == ";")
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
                
            //}
            //else if (mylist.ClassList.ElementAt(i) == "continue")
            //{
            //    i++;
            //    if (mylist.ClassList.ElementAt(i) == ";")
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            else if (mylist.ClassList.ElementAt(i) == "return")
            {  
                return true;
            }
            else
            {
                return false;
            }
        }
        bool SST2()
        {
            i++;
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Object_dec2())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (SST3())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less())
                {
                    //i++;
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == ";")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (var_arr())
            {
               // i++;
                if (SST4())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        bool SST3()
        {
            if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less())
                {
                  //  i++;
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == ";")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (var_arr())
            {
                i++;
                if (SST4())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool SST4()
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (OR_OP())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == ";")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
   
    }
}

class MyListDT
{
   
    public List<string> ClassList { get; set; }
    public List<string> ValueList { get; set; }
    public List<string> LineNumberList { get; set; }
}
