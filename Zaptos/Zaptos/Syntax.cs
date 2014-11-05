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

       public MyListDT tokkensetExcater(List<string> tkset)
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
                    if (temp == '(' || temp == ')' )
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
                    else if(temp == ',')
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
           return mylist;
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
               i++;
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
                                   i++;
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
               Error_List.Add(i.ToString());
           }


           return Error_List;
       }
       bool global_Space()
       {

           if (mylist.ClassList.ElementAt(i) == "Class")
           {
               if (Class())
               {
                   return true;
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
                   return true;
               }
               else
               {
                   return false;
               }

           }
           else if (mylist.ClassList.ElementAt(i+1) == "MainClass")
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
                            i++;
                            if (mylist.ClassList.ElementAt(i)=="}")
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
          bool checker = false; 
            
              if (declearation())
              {
                  checker = true;
                  class_body();
              }
              else if(func_dec())
              {
                  checker = true;
                  class_body();
              }
              else if(mylist.ClassList.ElementAt(i+1) == "}")
              {
                  checker = true;
              }
              else
              {
                  checker = false;
              }
              return checker;
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
                           return true;
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
                           if (mylist.ClassList.ElementAt(i)== ";")
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
           if (mylist.ClassList.ElementAt(i) == "=")
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
           else if(mylist.ClassList.ElementAt(i) == "ID")
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
           if (mylist.ClassList.ElementAt(i) == "=")
           {
               i++;
               if (mylist.ClassList.ElementAt(i) == "[")
               {
                   i++;
                   if (ID_Const())
                   {
                       i++;
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
                   i++;
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
               return true;
           }
           else if(Const())
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       bool Const()
       {
           if (mylist.ClassList.ElementAt(i) == "Int_Const" || mylist.ClassList.ElementAt(i) == "str_Const" ||mylist.ClassList.ElementAt(i) == "Flt_Const" )
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
           if (mylist.ClassList.ElementAt(i) == ";" )
           {
               i++;
               if (mylist.ClassList.ElementAt(i) == "=")
               {
                   i++;
                   if (mylist.ClassList.ElementAt(i) == "new")
                   {
                       i++;
                       if (mylist.ClassList.ElementAt(i) == "(")
                       {
                           i++;
                           if (Para_Less())
                           {
                               i++;
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















    }
}

class MyListDT
{
   
    public List<string> ClassList { get; set; }
    public List<string> ValueList { get; set; }
    public List<string> LineNumberList { get; set; }
}
