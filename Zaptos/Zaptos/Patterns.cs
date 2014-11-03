using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Zaptos
{
    class Patterns
    {

        public List<string> Pattern_Matching(string[] lines)
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
                    bool condition_flag = false;
                    string word = WordInLines.ElementAt(i);
                    if (!condition_flag)
                    {
                        flag = keyword_checker(word);
                        if(flag)
                        {
                            tokenset.Add("("+word+","+ word + "," + line_number + ")");
                            continue;
                        }
                        
                    }
                    if (!condition_flag)
                    {
                        flag = identifier_pattern(word);
                        if (flag)
                        {
                            tokenset.Add("(ID," + word + "," + line_number + ")");
                            continue;
                        }
                        
                        
                    }
                    if (!condition_flag)
                    {
                        flag = Constant_Pattern(word);
                        if (flag)
                        {
                            string conts_type = const_checker(word);
                            tokenset.Add("(" + conts_type + "," + word + "," + line_number + ")");
                            continue;
                        }
                        
                    }
                    if (!condition_flag)
                    {
                        flag = Puntuator_Pattern(word);
                        if(flag)
                        {
                            string punc_type = Punch_check(word);
                            tokenset.Add("("+punc_type+"," + word + "," + line_number + ")");
                            continue;
                        }
                        
                    }
                    if (!condition_flag)
                    {
                        flag = Operator_Pattern(word);
                        if (flag)
                        {
                            string Operator_Type = Operator_check(word);
                            tokenset.Add("(" + Operator_Type + "," + word + "," + line_number + ")");
                            continue;
                        }
                        
                    }
                    if(true)
                    {
                        tokenset.Add("(Error,"+word+"," + line_number + ")");
                    }
                    
                    
                }
            }

            return tokenset;
        }
        
        public List<string> Word_Breaker(string line)
        {
            List<string> word_list = new List<string>();
            string temperary = "";
            bool dot_checker = false;
            bool double_quote_checker = false;
            bool single_qoute_checker = false;
            int index_checker;
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
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("=");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '=')
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
                    
                }
                else if (temp == '+')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("+");
                        break;
                    }
                    else
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
                        else if (line[i + 1] == '=')
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
                    

                }
                else if (temp == '-')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("-");
                        break;
                    }
                    else
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
                    

                }
                else if (temp == '*')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("*");
                        break;
                    }
                    else
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
                    
                    
                }
                else if (temp == '/')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("/");
                        break;
                    }
                    else
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
                        else if (line[i + 1] == '/')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                            }

                            while (line[i] != '\n')
                            {
                                temp = line[i];
                                temperary = temperary + temp;

                                if (i < line.Length - 1)
                                {
                                    i++;
                                }
                                else
                                {
                                    temperary = "";
                                    break;
                                }
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
                   
                }
                else if (temp == '.')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add(".");
                        break;
                    }
                    else
                    {
                        if (char.IsDigit(line[i + 1]))
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
                                    temperary = "" + temp;
                                    dot_checker = false;
                                }

                                else
                                {
                                    temperary = "" + temp;
                                    dot_checker = false;
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
                    
                }
                else if (temp == '!')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add(".");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '=')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("!=");
                                i++;
                            }
                            else
                            {
                                word_list.Add("!=");
                                i++;
                            }
                        }
                        else
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("!");
                            }
                            else
                            {
                                word_list.Add("!");
                            }
                        }
                    }
                }
                else if (temp == '<')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("<");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '=')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("<=");
                                i++;
                            }
                            else
                            {
                                word_list.Add("<=");
                                i++;
                            }
                        }
                        else
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("<");
                            }
                            else
                            {
                                word_list.Add("<");
                            }
                        }
                    }
                    
                }
                else if (temp == '>')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add(">");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '=')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add(">=");
                                i++;
                            }
                            else
                            {
                                word_list.Add(">=");
                                i++;
                            }
                        }
                        else
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add(">");
                            }
                            else
                            {
                                word_list.Add(">");
                            }
                        }

                    }
                    
                }
                else if (temp == '%')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("%");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '=')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("%=");
                                i++;
                            }
                            else
                            {
                                word_list.Add("%=");
                                i++;
                            }
                        }
                        else
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("%");

                            }
                            else
                            {
                                word_list.Add("%");
                            }
                        }


                    }
                                   }
                else if (temp == '|')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("|");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '|')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("||");
                                i++;
                            }
                            else
                            {
                                word_list.Add("||");
                                i++;
                            }
                        }
                        else
                        {
                            temperary = temperary + temp;
                        }
                    }
                    
                }
                else if (temp == '&')
                {
                    index_checker = i + 1;
                    if (index_checker >= line.Length)
                    {
                        word_list.Add("&");
                        break;
                    }
                    else
                    {
                        if (line[i + 1] == '&')
                        {
                            if (!(temperary == string.Empty))
                            {
                                word_list.Add(temperary);
                                temperary = "";
                                word_list.Add("&&");
                                i++;
                            }
                            else
                            {
                                word_list.Add("&&");
                                i++;
                            }
                        }
                        else
                        {
                            temperary = temperary + temp;
                        }
                    }

                }
                else if (temp == '\"')
                {
                    if (!double_quote_checker)
                    {
                        double_quote_checker = true;
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "" + temp;

                        }
                        else
                        {
                            temperary = "" + temp;
                        }
                        i++;
                        while (line[i] != '\"')
                        {
                            temp = line[i];
                            temperary = temperary + temp;

                            if (i < line.Length -1)
                            {
                                i++;    
                            }
                            else
                            {
                                break;
                            }
                            
                        }    
                    }
                    temperary = temperary + line[i];
                    double_quote_checker = false;
                    
                }
                else if (temp == '\'')
                {
                    if (!single_qoute_checker)
                    {
                        single_qoute_checker = true;
                        if (!(temperary == string.Empty))
                        {
                            word_list.Add(temperary);
                            temperary = "" + temp;

                        }
                        else
                        {
                            temperary = "" + temp;
                        }
                        i++;
                        while (line[i] != '\'')
                        {
                            temp = line[i];
                            temperary = temperary + temp;

                            if (i < line.Length - 1)
                            {
                                i++;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    temperary = temperary + line[i];
                    single_qoute_checker = false;

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
        public static bool Puntuator_Pattern(string word)
        {
            string[] punc = { "[", "]", "{", "}", "(", ")", ".", ";", "\"", "\'","," };
            List<string> punctuators = new List<string>(punc);
            bool flag = false;
            if (punctuators.Contains(word))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        public static string Punch_check(string word)
        {
            string[] punc = { "[", "]", "{", "}", "(", ")", ".", ";", "\"", "\'","," };
            List<string> punctuators = new List<string>(punc);
            string punc_type;
            if (punctuators.Contains(word))
            {
                punc_type = word;
            }
            else
            {
                punc_type = "Not Found";
            }
            return punc_type;
        }
        public static bool keyword_checker(string word)
        {
            string[] keyword = { "break", "do", "Main", "if", "default", "return", "else", "case", "switch","new", "for", "const", "var", "while", "func", "null","MainClass","Class","Struct" };
            List<string> reservedKeywods = new List<string>(keyword);
            bool flag = false;
            if (reservedKeywods.Contains(word))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static bool Operator_Pattern(string word)
        {
            string[] Operators = { "++", "--", "!", "*", "/", "+", "-", "%", "&&", "||", "=", "+=", "*=", "-=", "/=", "%=", "<", ">", "<=", ">=", "!=", "==" };
            List<string> OperatorsList = new List<string>(Operators);
            bool flag = false;
            if (OperatorsList.Contains(word))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        public static string Operator_check(string word)
        {
            string[] Operators = { "++", "--", "!", "*", "/", "+", "-", "%", "&&", "||", "=", "+=", "*=", "-=", "/=", "%=", "<", ">", "<=", ">=", "!=", "==" };
            string classOfOperator = "";
            for (int i = 0; i < Operators.Length; i++)
            {

                if (word == "++" || word == "--")
                {
                    classOfOperator = "Inc_dec";

                }
                else if (word == "!")
                {
                    classOfOperator = "Not_Op";
                }
                else if (word == "+" || word == "*" || word == "-" || word == "/" || word == "%")
                {
                    classOfOperator = "Arith_Op";
                }
                else if (word == "&&" || word == "||")
                {
                    classOfOperator = "Cond_Op";
                }
                else if (word == "=" || word == "+=" || word == "*=" || word == "-=" || word == "/=" || word == "%=")
                {
                    classOfOperator = "Assig_Op";
                }
                else if (word == "<" || word == ">" || word == "<=" || word == ">=" || word == "!=" || word == "==")
                {
                    classOfOperator = "Relat_Op";
                }
            }
            return classOfOperator;
        }

        bool identifier_pattern(string word)
        {
            bool flag = false;
            string pattern = "^_[a-zA-Z0-9]+$";
            flag = Regex.IsMatch(word, pattern);
            return flag;
        }
        bool Constant_Pattern(string word)
        {
            bool flag = false;
            string integer_pattern = @"^[+-]?[0-9]+$";
            string string_pattern = "\"((\\[^\n\r])|[^\\\\\"\n\r])*\"";
            string float_pattern =@"^[+-]?([0-9]+\.?[0-9]+)$";
            if (Regex.IsMatch(word, integer_pattern) || Regex.IsMatch(word,float_pattern) || Regex.IsMatch(word, string_pattern))
	        {
                    flag = true;
                  return flag;		 
	        }
            return flag;
        }
        string const_checker(string word)
        {
            string integer_pattern = @"^[+-]?[0-9]+$";
            string string_pattern = "\"((\\[^\n\r])|[^\\\\\"\n\r])*\"";
            string float_pattern = @"^[+-]?([0-9]+\.?[0-9]+)$";
            string value="";
            if (Regex.IsMatch(word, integer_pattern))
            {
                value = "Int_Const";
                return value;
            }
            else if (Regex.IsMatch(word, float_pattern))
            {
                value = "Flt_Const";
                return value;
            }

            else if (Regex.IsMatch(word, string_pattern))
            {
                value = "str_Const";
                return value;
            }
            else
            {
                return value;
            }
        }
    }
}
