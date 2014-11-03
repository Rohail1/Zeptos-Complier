using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
    class Syntax
    {
        public List<string> tokkensets = new List<string>();
        private List<string> tokenset;

        public Syntax(List<string> tokenset)
        {
            // TODO: Complete member initialization
            tokkensets = tokkensetExcater(tokenset);
        }

       List<string> tokkensetExcater(List<string> tkset)
       {
           List<string> temp_list = new List<string>();
           
           foreach (string tks in tkset)
	        {
               string temperary = "";
               bool comma_Checker = false;
               for (int i = 0; i < tks.Length; i++)
			    {
			        char temp = tks[i];
                    if (temp == '(' || temp == ')' )
	                {
                        if (i == 1 || i == 3)
                        {
                            if (i == 1)
                            {
                                temperary = temp.ToString();
                                temperary += " ";
                            }
                            else
                            {
                                temperary += temp.ToString();
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
                                temperary = temp.ToString();
                                temperary += " ";
                                comma_Checker = true;
                            }
                            else
                            {
                                if (comma_Checker)
                                {
                                    temperary += temp.ToString();
                                }
                                else
                                {
                                    temperary += " ";
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            temperary += " ";
                            continue;
                        }
                    }
                    else
	                {
                       temperary = temperary + temp.ToString();
	                }
			    }
               temp_list.Add(temperary);
	        }

           return temp_list;
       }

    }
}
