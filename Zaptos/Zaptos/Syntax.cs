﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaptos;

namespace Zaptos
{
    class Syntax
    {

        MyListDT mylist = new MyListDT();
        ScopeControl scope = new ScopeControl();
        IntermediateCodeGenerator ICG = new IntermediateCodeGenerator();
        string tempVar="";
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
        public MyListDT SytaxAnalyzer()
        {
             mylist.SemanticErrorList = new List<string>();
             mylist.SyntaxErrorLineNumber = new List<string>();
             mylist.symbolTable = new List<SymbolTable>();
             mylist.IntermediateCode = new List<string>();
            if (global_Space())
            {
               // i++;
                if (mylist.ClassList.ElementAt(i) == "MainClass")
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        scope.CreateScope();
                        i++;
                        if (Main_Func())
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                scope.DestroyScope();
                                i++;
                                if (global_Space())
                                {
                                    mylist.SyntaxErrorLineNumber.Add("No Syntax Error. Code Successfully Parsed");
                 
                                }
                                else
                                {
                                    mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
                                }
                            }
                            else
                            {
                                mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
                            }
                        }
                        else
                        {
                            mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
                        }
                    }
                    else
                    {
                        mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
                    }
                }
                else
                {
                    mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
                }

            }
            else
            {
                mylist.SyntaxErrorLineNumber.Add(mylist.LineNumberList.ElementAt(i).ToString());
            }

            if (mylist.SemanticErrorList.Count == 0)
            {
                mylist.SemanticErrorList.Add("Code Successfully Parsed no Semantic Errors found");
            }
            if (ICG.IntermediateCode == null)
            {
                
            }
            else
            {
                mylist.IntermediateCode.Add(ICG.IntermediateCode);
            }
            
            return mylist;
        }
        bool Main_Func()
        {
            string classname;
            if (mylist.ClassList.ElementAt(i) == "func")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "Main")
                {
                    classname = mylist.ClassList.ElementAt(i);
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        scope.CreateScope();
                        i++;
                        if (MST(classname,scope.Scope))
                        {

                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                scope.DestroyScope();
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
            if (i == mylist.ClassList.Count)
            {
                return false;
            }

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
            string ClassName = "";
            if (mylist.ClassList.ElementAt(i) == "Class")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    if (LookUp(mylist.ValueList.ElementAt(i),mylist.ValueList.ElementAt(i),scope.Scope) == null)
                    {
                        insert(mylist.ValueList.ElementAt(i), mylist.ValueList.ElementAt(i),"Class", scope.Scope);
                        ClassName = mylist.ValueList.ElementAt(i);
                    }
                    else
                    {
                        mylist.SemanticErrorList.Add("RE-Declearation Error on Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }

                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        scope.CreateScope();
                        i++;
                        if (class_body(ClassName))
                        {

                            if (mylist.ClassList.ElementAt(i) == "}")
                            {
                                scope.DestroyScope();
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
        bool class_body(string classname)
        {
            int s = scope.Scope;

            if (declearation(classname,s))
            {
                i++;
                if (class_body(classname))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (func_dec(classname,s))
            {
                i++;
                if (class_body(classname))
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
        bool declearation(string classname, int s)
        {
            string n;
            if (mylist.ClassList.ElementAt(i) == "var")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n,classname,s) == null)
                    {
                        insert(n, classname, "-", s);
                    }
                    else
                    {
                        mylist.SemanticErrorList.Add("Redeclearation Error on Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }
                    i++;
                    if (dec_var_arr(n,classname,s))
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
        bool dec_var_arr(string n,string classname, int s)
        {
            if (Init(n,classname,s))
            {

                if (List(classname,s))
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
                        if (Init3(classname,s))
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
        bool Init(string name,string classname, int s)
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (Init2(name,classname, s))
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
        bool Init2(string name,string classname, int s)
        {
            string t="", n;
            if (Const(ref t))
            {
                mylist.symbolTable.Find(x => (x.name == name) && (x.scope == s)).type = t;
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "ID")
            {
                n = mylist.ValueList.ElementAt(i);
                t = LookUp(n,classname,s);
                if (t != null)
                {
                    mylist.symbolTable.Find(x => (x.name == name) && (x.scope == s)).type = t;
                }
                else
                {
                    mylist.SemanticErrorList.Add("Undecleared Identifier Used !! Line Number # " + mylist.LineNumberList.ElementAt(i).ToString());
                }

                i++;
                if (Init(name,classname,s))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (OR_OP(classname,s,ref t))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool List(string classname,int s)
        {
            string n;
            if (mylist.ClassList.ElementAt(i) == ";")
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n,classname,s) == null)
                    {
                        insert(n, classname, "-", s);
                    }
                    else
                    {
                        mylist.SemanticErrorList.Add("Redeclearation Error on Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }

                    i++;
                    if (Init(n,classname,s))
                    {
                        i++;
                        if (List(classname,s))
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
        bool Init3(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "[")
                {
                    i++;
                    if (ID_Const(classname,s))
                    {
                       // i++;
                        if (More_Elements(classname,s))
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
        bool More_Elements(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (ID_Const(classname,s))
                {
                  //  i++;
                    if (More_Elements(classname,s))
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
        bool ID_Const(string classnames,int s)
        {
            string t = "",n;

            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                n = mylist.ValueList.ElementAt(i);
                t = LookUp(n, classnames, s);
                if (t == null)
                {
                    mylist.SemanticErrorList.Add("Error : Undecleared Variable Used in line number #" + mylist.LineNumberList.ElementAt(i).ToString());
                }
                i++;
                return true;
            }
            else if (Const(ref t))
            {
                i++;
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Const(ref string t)
        {
            if (mylist.ClassList.ElementAt(i) == "Int_Const" || mylist.ClassList.ElementAt(i) == "str_Const" || mylist.ClassList.ElementAt(i) == "Flt_Const")
            {
                t = mylist.ClassList.ElementAt(i);
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
                    if (true)
                    {
                        return true;
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
        bool Object_dec2(string name,string objname, string classname ,int s)
        {
            string n;
            string perlist = "";
            if (mylist.ClassList.ElementAt(i) == ";")
            {
                insert(objname, classname, name, s);
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
                        n = mylist.ValueList.ElementAt(i);
                        if (LookUp(n,n,"Class") == null)
                        {
                            mylist.SemanticErrorList.Add("Identifier not decleared !! Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                        }
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "(")
                        {
                            i++;
                            if (Para_Less(classname,s,ref perlist))
                            {
                                if (mylist.ClassList.ElementAt(i) == ")")
                                {
                                    i++;
                                    if (mylist.ClassList.ElementAt(i) == ";")
                                    {
                                        insert(objname, classname, name, s);
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
            string ClassName = "";
            if (mylist.ClassList.ElementAt(i) == "Struct")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    if (LookUp(mylist.ValueList.ElementAt(i),mylist.ValueList.ElementAt(i),scope.Scope) == null)
                    {
                        insert(mylist.ValueList.ElementAt(i), mylist.ValueList.ElementAt(i),"struct", scope.Scope);
                        ClassName = mylist.ValueList.ElementAt(i);
                    }
                    else
                    {
                        mylist.SemanticErrorList.Add("RE-Declearation Error on Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "{")
                    {
                        scope.CreateScope();
                        i++;
                        if (Struct_Body(ClassName))
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
        bool Struct_Body(string classname)
        {
            int s = scope.Scope;
            if (declearation(classname,s))
            {
                i++;
                if (Struct_Body(classname))
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else if (func_dec(classname,s))
            {
                i++;
                if (Struct_Body(classname))
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
        bool Body(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "{")
            {
                scope.CreateScope();
                s = scope.Scope;
                i++;
                if (MST(classname,s))
                {
                    if (mylist.ClassList.ElementAt(i) == "}")
                    {
                        scope.DestroyScope();
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
        bool MST(string classname,int s)
        {
            if (SST(classname,s))
            {
                if (mylist.ClassList.ElementAt(i) == "return")
                {
                    return true;
                }
                else
                {
                    i++;
                    if (MST(classname,s))
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
        bool If(string classname,int s)
        {
            string label1;
            if (mylist.ClassList.ElementAt(i) == "if")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (Cond(classname,s))
                    {

                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            label1 = ICG.generatelabel();
                            tempVar = ICG.generateTemperaryVariable();
                            ICG.IntermediateCode += "if("+tempVar+"==false) jump " + label1;
                            ICG.IntermediateCode += Environment.NewLine;
                            if (Body(classname,s))
                            {
                                i++;
                                if (O_Else(classname,s,label1))
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
        bool O_Else(string classname,int s,string label1)
        {
            string label2;
            if (mylist.ClassList.ElementAt(i) == "else")
            {
                i++;
                label2 = ICG.generatelabel();
                ICG.IntermediateCode += "jump " + label2;
                ICG.IntermediateCode += Environment.NewLine;
                ICG.IntermediateCode += label1 + ":";
                ICG.IntermediateCode += Environment.NewLine;
                if (Body(classname,s))
                {
                    ICG.IntermediateCode += label2 + ":";
                    ICG.IntermediateCode += Environment.NewLine;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (MST(classname,s))
            {
                ICG.IntermediateCode += label1 + ":";
                ICG.IntermediateCode += Environment.NewLine; 
                return true;
            }
            else
            {
                return false;
            }
        }
        bool While(string classname,int s)
        {
            string label1, label2;
            if (mylist.ClassList.ElementAt(i) == "while")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (Cond(classname,s))
                    {

                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            label1 = ICG.generatelabel();
                            label2 = ICG.generatelabel();
                            tempVar = ICG.generateTemperaryVariable();
                            ICG.IntermediateCode += label1 +":";
                            ICG.IntermediateCode += Environment.NewLine;
                            ICG.IntermediateCode += "if(" + tempVar + "==false) jump " + label2;
                            ICG.IntermediateCode += Environment.NewLine;
                            if (Body(classname,s))
                            {
                                ICG.IntermediateCode += " jump " + label1;
                                ICG.IntermediateCode += Environment.NewLine;
                                ICG.IntermediateCode += label2 + " :";
                                ICG.IntermediateCode += Environment.NewLine;
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
        bool For(string classname,int s)
        {
            string label1, label2;
            if (mylist.ClassList.ElementAt(i) == "for")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    scope.CreateScope();
                    s = scope.Scope;
                    i++;
                    if (Assign_Dec(classname,s))
                    {
                        i++;
                        if (for_Cond(classname,s))
                        {

                            if (mylist.ClassList.ElementAt(i) == ";")
                            {
                                i++;
                                if (Assign_INC_Dec(classname,s))
                                {

                                    if (mylist.ClassList.ElementAt(i) == ")")
                                    {
                                        i++;
                                        label1= ICG.generatelabel();
                                        tempVar = ICG.generateTemperaryVariable();
                                        ICG.IntermediateCode += label1 + ":";
                                        ICG.IntermediateCode += Environment.NewLine;
                                        label2 = ICG.generatelabel();
                                        ICG.IntermediateCode += "if("+tempVar+"==false) jump  " + label2;
                                        ICG.IntermediateCode += Environment.NewLine;
                                        if (For_Body(classname,s))
                                        {
                                            ICG.IntermediateCode += "jump " + label1;
                                            ICG.IntermediateCode += Environment.NewLine;
                                            ICG.IntermediateCode += label2 + ":";
                                            ICG.IntermediateCode += Environment.NewLine;

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
        bool For_Body(string classname, int s)
        {
            if (mylist.ClassList.ElementAt(i) == "{")
            {

                i++;
                if (MST(classname, s))
                {
                    if (mylist.ClassList.ElementAt(i) == "}")
                    {
                        scope.DestroyScope();
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
        bool Assign_Dec(string classname,int s)
        {
            string n;
            if (mylist.ClassList.ElementAt(i) == "var")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n,classname,s) == null)
                    {
                        insert(n, classname, "-", s);
                    }
                    else
                    {
                        mylist.SemanticErrorList.Add("Redelecaration Error ! Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "Int_Const" || mylist.ClassList.ElementAt(i) == "Flt_Const")
                        {
                            mylist.symbolTable.Find(x => ((x.className == classname) && (x.name == n) && (x.scope == s))).type = mylist.ClassList.ElementAt(i);
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
            else if (Assignment(classname,s))
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
        bool for_Cond(string classname,int s)
        {
            if (Cond(classname,s))
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
        bool Assign_INC_Dec(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                string n = mylist.ValueList.ElementAt(i);
                if (LookUp(n,classname,s) == null)
                {
                    mylist.SemanticErrorList.Add("Error : Identifier Not decleared. Line Number# " + mylist.LineNumberList.ElementAt(i).ToString());
                }
                i++;
                if (Assign_INC_Dec2(classname,s))
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
                    string n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n, classname, s) == null)
                    {
                        mylist.SemanticErrorList.Add("Error : Identifier Not decleared. Line Number# " + mylist.LineNumberList.ElementAt(i).ToString());
                    }
                    i++;
                    if (var_arr(classname,s))
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
        bool Assign_INC_Dec2(string classname,int s)
        {
            string t = "";
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    i++;
                    if (var_arr(classname,s))
                    {

                        if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                        {
                            i++;
                            if (OR_OP(classname,s,ref t))
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
            else if (var_arr(classname,s))
            {

                if (Assign_INC_Dec3(classname,s,ref t))
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
        bool Assign_INC_Dec3(string classname,int s,ref string type)
        {
           
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (OR_OP(classname,s,ref type))
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
        bool Do_While(string classname,int s)
        {
            string Label1;
            if (mylist.ClassList.ElementAt(i) == "do")
            {
                Label1 = ICG.generatelabel();
                tempVar = ICG.generateTemperaryVariable();
                ICG.IntermediateCode += Label1 + ":";
                ICG.IntermediateCode += Environment.NewLine;

                i++;
                if (Body(classname,s))
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "while")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == "(")
                        {
                            i++;
                            if (Cond(classname,s))
                            {

                                if (mylist.ClassList.ElementAt(i) == ")")
                                {
                                    i++;
                                    if (mylist.ClassList.ElementAt(i) == ";")
                                    {
                                        ICG.IntermediateCode += "if(" + tempVar + "==true) jump " + Label1;
                                        ICG.IntermediateCode += Environment.NewLine;
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
        bool Switch(string classname,int s)
        {

            if (mylist.ClassList.ElementAt(i) == "switch")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "(")
                {
                    i++;
                    if (ID_Const(classname,s))
                    {
                       // i++;
                        if (mylist.ClassList.ElementAt(i) == ")")
                        {
                            i++;
                            if (mylist.ClassList.ElementAt(i) == "{")
                            {
                                scope.CreateScope();
                                s = scope.Scope;
                                i++;
                                if (Case(classname,s))
                                {

                                    if (Default(classname,s))
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
        bool Case(string classname,int s)
        {
            string t = "";
            if (mylist.ClassList.ElementAt(i) == "case")
            {
                i++;
                if (Const(ref t))
                {
                    i++;
                    if (mylist.ClassList.ElementAt(i) == ":")
                    {
                        i++;
                        if (Case_Body(classname,s))
                        {
                            i++;
                            if (Case(classname,s))
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
        bool Default(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "default")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == ":")
                {
                    i++;
                        if (MST(classname,s))
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
        bool Case_Body(string classname,int s)
        {
            if (MST(classname,s))
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
        bool Cond(string classname,int s)
        {
            if (ID_Const(classname,s))
            {
              //  i++;
                if (Cond2(classname,s))
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
        bool Cond2(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                i++;
                if (ID_Const(classname,s))
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
        bool func_dec(string classname,int s)
        {
            string n, perematerList = "";
            if (mylist.ClassList.ElementAt(i) == "func")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "(")
                    {
                        scope.CreateScope();
                        s = scope.Scope;
                        i++;
                        if (Para_Less(classname,s, ref perematerList))
                        {
                           // i++;
                            if (mylist.ClassList.ElementAt(i) == ")")
                            {
                                if (LookUp(n,classname,perematerList,s) == null)
                                {
                                    insert(n, classname, perematerList, s);
                                }
                                else
                                {
                                    mylist.SemanticErrorList.Add("Function with These perameters already decleared. Line Number # " + mylist.LineNumberList.ElementAt(i).ToString());
                                }
                                i++;
                                if (mylist.ClassList.ElementAt(i) == "{")
                                {
                                    i++;
                                    if (Func_Body(classname,s))
                                    {
                                        i++;
                                        if (mylist.ClassList.ElementAt(i) == "}")
                                        {
                                            scope.DestroyScope();
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
        bool Func_Body(string classname,int s)
        {
            string t = "";
            if (MST(classname,s))
            {
             //   i++;
                if (mylist.ClassList.ElementAt(i) == "return")
                {
                    i++;
                    if (OR_OP(classname,s,ref t))
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
        bool Para_Less(string classname,int s, ref string perList)
        {
            string t = "";
            string n;
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                n = mylist.ValueList.ElementAt(i);
                if (LookUp(n,classname,s) == null)
                {
                    insert(n, classname, "-", s);
                    perList += n;
                }
                i++;
                if (Para_Less2(classname,s,ref perList))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Const(ref t))
            {
                perList += t; 
                i++;
                if (Para_Less2(classname,s,ref perList))
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
        bool Para_Less2(string classname,int s,ref string perlist)
        {
            if (mylist.ClassList.ElementAt(i) == ",")
            {
                i++;
                if (Para_Less(classname,s,ref perlist))
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
        bool Func_method_Calling(string classname,int s)
        {
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                i++;
                if (Func_method_Calling2(classname,s))
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
        bool Func_method_Calling2(string classname,int s)
        {
            string perlist = "";
            string n;
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    i++;
                    if (mylist.ClassList.ElementAt(i) == "(")
                    {
                        i++;
                        if (Para_Less(classname,s,ref perlist))
                        {
                          //  i++;
                            if (mylist.ClassList.ElementAt(i) == ")")
                            {
                                if (LookUp(n,classname,perlist,s) == null)
                                {
                                    mylist.SemanticErrorList.Add("Error : Function not decleared. Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                                }

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
                if (Para_Less(classname,s,ref perlist))
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
        bool Assignment(string classname,int s)
        {
            string t = "";
            if (var_Objvar(classname,s))
            {

                if (mylist.ClassList.ElementAt(i) == "Assig_Op")
                {
                    i++;
                    if (OR_OP(classname,s,ref t))
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
        bool var_Objvar(string classname,int s)
        {
            string n;
            string t = "";
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                n = mylist.ValueList.ElementAt(i);
               string classnametype = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;
                t = LookUp(n, classname, s);
                if (t == null)
                {
                    mylist.SemanticErrorList.Add("Identifier not Decleared Line Number#" + mylist.LineNumberList.ElementAt(i).ToString());
                }
                i++;
                if (var_Objvar2(n,classname,classnametype,s))
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
        bool var_Objvar2(string name,string classname,string type,int s)
        {
            string n;
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n,type,s) == null)
                    {
                        mylist.SemanticErrorList.Add("Error Identifer not decleared Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                    i++;
                    if (var_arr(classname,s))
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
            else if (var_arr(classname,s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool var_arr(string classname,int s)
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
        bool OR_OP(string classname,int s,ref string type)
        {
            string t1 = null;
            if (And_OP(classname,s,ref type))
            {
               // i++;
                if (OR_OP1(classname, s, ref t1,type))
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
        bool OR_OP1(string classname, int s, ref string type,string type2)
        {
            string op =null,T1=null,rt=null;
            if (mylist.ClassList.ElementAt(i) == "||")
            {
                op = mylist.ClassList.ElementAt(i);
                i++;
                if (And_OP(classname, s, ref rt))
                {
                    T1 = Compatibility(type, rt, op);
                    if (T1 == "Error")
                    {
                        mylist.SemanticErrorList.Add("Operator or Operand Type Mismatch. Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                  //  i++;
                    if (OR_OP1(classname, s, ref type,T1))
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
        bool And_OP(string classname, int s, ref string type)
        {
            string T1 = null;
            if (RE(classname, s, ref type))
            {
                //i++;
                if (And_OP1(classname, s, ref type,T1))
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
        bool And_OP1(string classname, int s, ref string type,string type2)
        {
            string op = null, rt = null, T1 = null;
            if (mylist.ClassList.ElementAt(i) == "&&")
            {
                op = mylist.ClassList.ElementAt(i);
                i++;
                if (RE(classname, s, ref rt))
                {
                    T1 = Compatibility(type, rt, op);
                    if (T1 == "Error")
                    {
                        mylist.SemanticErrorList.Add("Operator or Operand Type Mismatch. Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                  //  i++;
                    if (And_OP1(classname, s, ref type,T1))
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
        bool RE(string classname, int s, ref string type)
        {
            string T1 = null;
            if (AE(classname, s, ref type))
            {
                //i++;
                if (RE1(classname, s, ref type,T1))
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
        bool RE1(string classname, int s, ref string type,string type2)
        {
            string op = null, T1 = null, rt = null;
            if (mylist.ClassList.ElementAt(i) == "Relat_Op")
            {
                op = mylist.ClassList.ElementAt(i);
                i++;
                if (AE(classname, s, ref rt))
                {
                    T1 = Compatibility(type, rt, op);
                    if (T1 == "Error")
                    {
                        mylist.SemanticErrorList.Add("Operator or Operand Type Mismatch. Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                  //  i++;
                    if (RE1(classname, s, ref type,T1))
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
        bool AE(string classname, int s, ref string type)
        {
            string t1 = null;
            if (T(classname, s, ref type))
            {
                //i++;
                if (AE1(classname, s, ref type, t1))
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
        bool AE1(string classname, int s, ref string type,string type2)
        {
            string op = null, rt = null, T1 = null;
            if (mylist.ClassList.ElementAt(i) == "Arith_Op")
            {
                op = mylist.ValueList.ElementAt(i);
                i++;
                if (T(classname, s, ref rt))
                {
                    T1 = Compatibility(type, rt, op);
                    if (T1 == "Error")
                    {
                        mylist.SemanticErrorList.Add("Operator or Operand Type Mismatch. Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
               //     i++;
                    if (AE1(classname, s, ref type,T1))
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
        bool T(string classname, int s, ref string type)
        {
            string t1 = null;
            if (F(classname, s, ref type))
            {
             //   i++;
                if (T1(classname, s, ref type,t1))
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
        bool T1(string classname, int s, ref string type,string type2)
        {
            string op = null, rt = null, t1 = null;
            if (mylist.ClassList.ElementAt(i) == "M_D_M")
            {
                op = mylist.ClassList.ElementAt(i);
                i++;
                if (F(classname, s, ref rt))
                {
                    t1 = Compatibility(type, rt, op);
                    if (t1 == "Error")
                    {
                        mylist.SemanticErrorList.Add("Operator or Operand Type Mismatch. Line Number #" + mylist.LineNumberList.ElementAt(i));
                    }
                    //i++;
                    if (T1(classname, s, ref type,t1))
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
        bool F(string classname, int s, ref string type)
        {
            string types = "";
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                string n = mylist.ValueList.ElementAt(i);
                if (LookUp(n,n,"Class") != null)
                {
                    types = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;    
                }
                else if (LookUp(n,classname,s) != null)
                {
                    types = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;
                }
                else
                {
                    mylist.SemanticErrorList.Add("ID not decleared Line Number#" + mylist.LineNumberList.ElementAt(i));
                }
                
                i++;
                if (F1(n,classname,types, s, ref type))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (Const(ref type))
            {
                i++;
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (OR_OP(classname, s, ref type))
                {
                    return true;
                }
                else
	            {
                    return false;
	            }
            }
            else if (F2(classname, s, ref type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool F1(string idname,string classname,string classtype, int s, ref string type)
        {
            string perList = "";
            string n = idname;
            if (mylist.ClassList.ElementAt(i) == ".")
            {
                i++;
                if (mylist.ClassList.ElementAt(i) == "ID")
                {
                    n = mylist.ClassList.ElementAt(i);
                    i++;
                    if (F3(n,classname,classtype,s,ref type))
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
            else if (var_arr(classname,s))
            {
                if (LookUp(n, n, "Class") != null)
                {
                    type = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;
                }
                else if (LookUp(n, classname, s) != null)
                {
                    type = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;
                }
                else
                {
                    mylist.SemanticErrorList.Add("ID not decleared Line Number#" + mylist.LineNumberList.ElementAt(i));
                }
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less(classname,s,ref perList))
                {
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        if (LookUp(n,classtype,perList) == null)
                        {
                            mylist.SemanticErrorList.Add("Method not defined Line Number# " + mylist.LineNumberList.ElementAt(i).ToString());
                        }
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
        bool F2(string classname, int s, ref string type)
        {
            if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (var_Objvar(classname,s))
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
        bool F3(string name,string classname,string classtype, int s, ref string type)
        {
            string perList = "";
            if (var_arr(classname,s))
            {
                type = mylist.symbolTable.Find(x => x.name == name && x.className == classtype).type;
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less(classname,s, ref perList))
                {
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        if ( LookUp(name, classtype, perList) == null)
                        {
                            mylist.SemanticErrorList.Add("Method not defined Line Number# " + mylist.LineNumberList.ElementAt(i).ToString());
                        }

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
            else
            {
                return false;
            }
        }
        //bool SST_INC_DEC()
        //{
        //    if (true)
        //    {
        //        i++;
        //        if (mylist.ClassList.ElementAt(i) == "Inc_dec")
        //        {
        //            i++;
        //            if (mylist.ClassList.ElementAt(i) == ";")
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
        //    {
        //        i++;
        //        if (true)
        //        {
        //            i++;
        //            if (mylist.ClassList.ElementAt(i) == ";")
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        bool SST(string classname,int s)
        {
            string classtype="";
            string n;
            if (declearation(classname,s) || While(classname,s) || For(classname,s) || If(classname,s) || Do_While(classname,s) || Switch(classname,s))
            {
                return true;
            }
            else if (mylist.ClassList.ElementAt(i) == "Inc_dec")
            {
                i++;
                if (var_Objvar(classname,s))
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
                classtype = "";
                n = mylist.ValueList.ElementAt(i);
                if (LookUp(n,classname,s) ==  null)
                {
                    if (LookUp(n,n,"Class") == null)
                    {
                        mylist.SemanticErrorList.Add("Identifer Not decleared !! Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }
                    else
                    {
                        classtype = mylist.symbolTable.Find(x => x.name == n && x.className == x.name).className;
                    }
                }
                else if(LookUp(n,n,"Class") == null)
                {
                    if (LookUp(n, classname, s) == null)
                    {
                        mylist.SemanticErrorList.Add("Identifer Not decleared !! Line Number #" + mylist.LineNumberList.ElementAt(i).ToString());
                    }
                    else
                    {
                        classtype = mylist.symbolTable.Find(x => x.name == n && x.className == classname).type;
                    }
                    
                }
                
                if (SST2(n,classname,classtype,s))
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
        bool SST2(string name,string classname,string classtype,int s)
        {
            string perList = "";
            string n;
            i++;
            if (mylist.ClassList.ElementAt(i) == "ID")
            {
                n = mylist.ValueList.ElementAt(i);
                if (LookUp(n,classname,s) !=  null)
                {
                    mylist.SemanticErrorList.Add("Redeclearation Error !! Line Number" + mylist.LineNumberList.ElementAt(i).ToString());
                }
                else
                {
                    insert(n, classname,name, s);
                }
                i++;
                if (Object_dec2(name,n,classname,s))
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
                    n = mylist.ValueList.ElementAt(i);
                    if (LookUp(n,classtype,s) == null)
                    {
                        
                    }
                    i++;
                    if (SST3(n,classname,classtype,s))
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
                if (Para_Less(classname,s,ref perList))
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
            else if (var_arr(classname,s))
            {
               // i++;
                if (SST4(name,classname,s))
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
        bool SST3(string name,string classname,string classtype,int s)
        {
            string perlist = "";
            if (mylist.ClassList.ElementAt(i) == "(")
            {
                i++;
                if (Para_Less(classname,s,ref perlist))
                {
                  //  i++;
                    if (mylist.ClassList.ElementAt(i) == ")")
                    {
                        i++;
                        if (mylist.ClassList.ElementAt(i) == ";")
                        {
                            if (LookUp(name,classtype,perlist) == null)
                            {
                                mylist.SemanticErrorList.Add("method not found Line Number#" + mylist.LineNumberList.ElementAt(i).ToString());
                            }
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
            else if (var_arr(classname,s))
            {
                if (LookUp(name, classtype) == null)
                {
                    mylist.SemanticErrorList.Add("Error: Undecleared Variable Used. Line Number#" + mylist.LineNumberList.ElementAt(i).ToString());
                }
                i++;
                if (SST4(name,classname,s))
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
        bool SST4(string name,string classname,int s)
        {

            string t = "";
            if (mylist.ClassList.ElementAt(i) == "Assig_Op")
            {
                i++;
                if (OR_OP(classname,s,ref t))
                {
                    mylist.symbolTable.Find(x => x.name == name && x.scope == s && x.className == classname).type = t;
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
        string LookUp(string name,string classname,int s)
        {
            SymbolTable temp = new SymbolTable();
            temp.type = "";
            temp = mylist.symbolTable.Find(x => ((x.name == name) && (x.className == classname) && (x.scope == s)));

            if (temp == null)
            {
                return null;
            }
            else
            {
                return temp.type;
            }
            
        }
        string LookUp(string name, string classname)
        {
            SymbolTable temp = new SymbolTable();
            temp.type = "";
            temp = mylist.symbolTable.Find(x => ((x.name == name) && (x.className == classname)));

            if (temp == null)
            {
                return null;
            }
            else
            {
                return temp.type;
            }

        }
        string LookUp(string name, string classname, string type)
        {
            SymbolTable temp = new SymbolTable();
            temp.type = null;
            temp = mylist.symbolTable.Find(x => ((x.name == name) && (x.className == classname) && (x.type == type)));
            if (temp == null)
            {
                return null;
            }
            else
            {
                return temp.type;
            }
        }
        string LookUp(string name, string classname,string type, int s)
        {
            SymbolTable temp = new SymbolTable();
            temp.type = null;
            temp = mylist.symbolTable.Find(x => ((x.name == name) && (x.className == classname) && (x.scope == s) && (x.type == type)));
            if (temp == null)
            {
                return null;
            }
            else
            {
                return temp.type;
            }
        }
        void insert(string name, string classname,string type, int s)
        {
            mylist.symbolTable.Add(new SymbolTable(name, type, classname, s));
        }
        string Compatibility(string lt, string rt,string op)
        {
            string type = null;
            if (op == "+" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Int_Const";
            }
            else if (op == "+" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "+" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "+" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "+" && lt == "str_Const" && rt == "Int_Const" )
            {
                return type = "str_Const";
            }
            else if (op == "+" && rt == "str_Const" && lt == "Int_Const")
            {
                return type = "str_Const";
            }
            else if (op == "+" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "str_Const";
            }
            else if (op == "-" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Int_Const";
            }
            else if (op == "-" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "-" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "-" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "*" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Int_Const";
            }
            else if (op == "*" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "*" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "*" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "/" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Int_Const";
            }
            else if (op == "/" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == "/" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Int_Const";
            }
            else if (op == "/" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Flt_Const";
            }
            else if (op == ">" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == ">" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == ">" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == ">" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == ">=" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "<" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "<" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "<=" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "!=" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && lt == "Int_Const" && rt == "Int_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && lt == "Int_Const" && rt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && rt == "Int_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && rt == "Flt_Const" && lt == "Flt_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && lt == "Int_Const" && rt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "==" && rt == "Int_Const" && lt == "str_const")
            {
                return type = "Bool";
            }
            else if (op == "==" && rt == "Flt_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && lt == "Flt_Const" && rt == "str_Const")
            {
                return type = "Bool";
            }
            else if (op == "==" && rt == "str_Const" && lt == "str_Const")
            {
                return type = "Bool";
            }
            else
            {
                return type = "Error";
            }

        }
   
    }
}

class MyListDT
{
    public List<string> IntermediateCode { get; set; }
    public List<string> ClassList { get; set; }
    public List<string> ValueList { get; set; }
    public List<string> LineNumberList { get; set; }
    public List<string> SyntaxErrorLineNumber { get; set; }
    public List<string> SemanticErrorList { get; set; }
    public List<SymbolTable> symbolTable { get; set; }
}

