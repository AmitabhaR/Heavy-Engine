/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/

#include "compile_scanner.hpp"

Scanner::Scanner(string file)
{
    this->file_name = file;
}

bool Scanner::start( )
{
    ifstream stm(file_name.c_str( ));
    bool isString = false;
    bool isComment = false;

    if (stm.is_open( ))
    {
          string cur_line;
 		  string cur_token;

          while (getline(stm,cur_line))
          {	
                for(int cntr = 0;cntr < cur_line.length();cntr++)
                {
                    string cur_char = cur_line.substr(cntr,1);

                    if (!isString && !isComment)
                    {
                        if (cur_char == " ")
                        {
                            if (cur_token != "")
                            {
                                token_list.push_back(cur_token);
                                cur_token = "";
                            }
                        }
                        else if (cur_char == ";")
                        {
                            if (cur_token != "")
                            {
                                token_list.push_back(cur_token);
                            }

                            token_list.push_back(";");
                            cur_token = "";
                        }
                        else if (cur_char == ",")
                        {
                            if (cur_token != "")
                            {
                                token_list.push_back(cur_token);
                            }

                            token_list.push_back(",");
                            cur_token = "";
                        }
                        else if (cur_char == "\"")
                        {
                            if (cur_token != "")
                            {
                                token_list.push_back(cur_token);
                            }

                            cur_token = "@";       // String identifier.
                            isString = true;
                        }
                        else if (cur_char == "?")
                        {
                            if (cur_token != "")
                            {
                                token_list.push_back(cur_token);
                            }

                            cur_token = "";
                            isComment = true;
                        }
                        else
                        {
                            cur_token += cur_char;
                        }
                }
                else if (isComment)
                {
                    if (cur_char == "?")
                    {
                        isComment = false;
                    }
                }
                else
                {
                    if (cur_char == "\"")
                    {
                        token_list.push_back(cur_token);
                        isString = false;
                        cur_token = "";
                    }
                    else
                    {
                        cur_token += cur_char;
                    }
                }
            }

            if (!isString)
            {
                if (cur_token != "")
                {
                    token_list.push_back(cur_token);
                }

                cur_token = "";
            }
        }

        if (isString)
        {
            error_list.push_back("Non-terminating string literal.");
        }
        else if (isComment)
        {
            error_list.push_back("Non-terminating comment literal.");
        }

        if (cur_token != "")
        {
            token_list.push_back(cur_token);
        }
    }

    if (error_list.size( ) > 0)
    {
        return false;
    }

    return true;
}

list<string> Scanner::getTokens( )
{
    return token_list;
}

list<string> Scanner::getErrors( )
{
    return error_list;
}
