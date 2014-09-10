using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaptos
{
    class Patterns
    {

    /*    public List<string> Pattern_Matching(string[] lines)
        {
            List<string> tokenset = new List<string>();
            int line_number = 0;
            foreach (string line in lines)
            {
                line_number++;
                List<string> WordInLines = new List<string>();
                WordInLines = Word_Breaker(line);
                for (int i = 0; i < WordInLines.Count; i++)
                {
                    bool flag = false;
                    string word = WordInLines.ElementAt(i);
                    if (!flag)
                    {
                        flag = Keyword_checker(word);
                        if(flag)
                        {
                            tokenset.Add("(Keyword," + word + "," + line_number + ")");
                        }
                    }
                    else if (!flag)
                    {
                        flag = Constant_Pattern(word);
                        if(flag)
                        {
                            string conts_type = const_Checker(word);
                            tokenset.Add("("+conts_type+"," + word + "," + line_number + ")");
                        }
                    }
                    else if (!flag)
                    {
                        flag = identifier_Pattern(word);
                        if(flag)
                        {
                            tokenset.Add("(ID," + word + "," + line_number + ")");
                        }
                    }
                    else if (!flag)
                    {
                        flag = Puntuator_Pattern(word);
                        if(flag)
                        {
                            string punc_type = Punch_check(word);
                            tokenset.Add("("+punc_type+"," + word + "," + line_number + ")");
                        }
                        
                    }
                    else if (!flag)
                    {
                        flag = Operator_Pattern(word);
                        if (flag)
                        {
                            string Operator_Type = Operator_check(word);
                            tokenset.Add("(" + Operator_Type + "," + word + "," + line_number + ")");
                        }

                    }
                    else
                    {
                        tokenset.Add("(Error,Invalid," + line_number + ")");
                    }

                }
            }

            return tokenset;
        }
        */
        public List<string> Word_Breaker(string line)
        {
            List<string> word_list = new List<string>();
            string temperary = "";
            bool dot_checker = false;
            for (int i = 0; i < line.Length; i++)
            {
              
                char temp = line[i];
                if (temp == ' ')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";

                    }
                }
                else if (temp == '(')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add(""+temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ')')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == '[')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ']')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == '}')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == '{')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ',')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ';')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ',')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == ';')
                {
                    if (!(temperary == string.Empty))
                    {
                        word_list.Add(temperary);
                        temperary = "";
                        word_list.Add("" + temp);
                    }
                    else
                    {
                        word_list.Add("" + temp);
                    }
                }
                else if (temp == '=')
                {
                    if (line[i+1] == '=')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("==");
                            i++;
                        }
                        else
                        {
                            word_list.Add("==");
                            i++;
                        }     
                    }
                    else
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("" + temp);
                        }
                        else
                        {
                            word_list.Add("" + temp);
                        }
                    }
                   
                }
                else if (temp == '+')
                {
                    if (line[i + 1] == '+')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("++");
                            i++;
                        }
                        else
                        {
                            word_list.Add("++");
                            i++;
                        }
                    }
                    else if (line[i+1] == '=')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("+=");
                        }
                        else
                        {
                            word_list.Add("+=");
                        }
                    }
                    else 
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("+");
                        }
                        else
                        {
                            word_list.Add("+");
                        }
                    }

                }
                else if (temp == '-')
                {
                    if (line[i + 1] == '-')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("--");
                            i++;
                        }
                        else
                        {
                            word_list.Add("--");
                            i++;
                        }
                    }
                    else if (line[i + 1] == '=')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("-=");
                        }
                        else
                        {
                            word_list.Add("-=");
                        } 
                    }
                    else
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("-");
                        }
                        else
                        {
                            word_list.Add("-");
                        }
                    }

                }
                else if (temp == '*')
                {
                    if (line[i + 1] == '=')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("*=");
                            i++;
                        }
                        else
                        {
                            word_list.Add("*=");
                            i++;
                        }
                    }
                    else
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("*");
                        }
                        else
                        {
                            word_list.Add("*");
                        }
                    }

                }
                else if (temp == '/')
                {
                    if (line[i + 1] == '=')
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("/=");
                            i++;
                        }
                        else
                        {
                            word_list.Add("/=");
                            i++;
                        }
                    }
                    else
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("/");
                        }
                        else
                        {
                            word_list.Add("/");
                        }
                    }

                }
                else if (temp == '.')
                {
                    if (char.IsDigit(line[i+1]))
                    {
                        if (!dot_checker)
                        {
                            temperary = temperary + temp;
                            dot_checker = true;
                        }
                        else
                        {
                            if (!(temperary == string.Empty))
                            {
                            word_list.Add(temperary);
                            temperary = ""+temp;
                            }    
                        
                            else
                            {
                               temperary = ""+temp;
                            }                 
                        }
                    }
                    else
                    {
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "";
                            word_list.Add("" + temp);
                        }

                        else
                        {
                            word_list.Add("" + temp);
                        }                 
                    }
                }
                else if (false)
                {
                    
                }
                else
                {
                    temperary = temperary + temp;
                }


            }
            if (!(temperary == string.Empty))
            {
                word_list.Add(temperary);
            }
            return word_list;
        }
    }
}
