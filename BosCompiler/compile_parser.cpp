/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/

#include "compile_parser.hpp"
#include <fstream>

Parser::Parser(list<string> tokens , int gen_code)
{
    this->tokens = tokens;
    this->gen_code = gen_code;
}

bool Parser::parse(string class_name , string dir)
{
    string cur_action;
    string cur_code;
    string cur_comma_param;
    list<Variable> variable_list; // Static variable list.
    list<string> jump_points;
    list<string> header_files;
    Variable def_var;
	
    for(list<string>::iterator token = tokens.begin();token != tokens.end( );token++)
    {
        string cur_token = *token;

        if (cur_action == "")
        {
            if (cur_token == "static_var")
            {
                cur_action = "static_var";
            }
            else if (cur_token == "var")
            {
                cur_action = "var";
            }
            else if (cur_token == "set")
            {
                cur_action = "set";
            }
            else if (cur_token == "add")
            {
                cur_action = "add";
            }
            else if (cur_token == "sub")
            {
                cur_action = "sub";
            }
            else if (cur_token == "mul")
            {
                cur_action = "mul";
            }
            else if (cur_token == "div")
            {
                cur_action = "div";
            }
            else if (cur_token == "if")
            {
                cur_action = "if";
            }
            else if (cur_token == "goto")
            {
                cur_action = "goto";
            }
            else if (cur_token == "object_setX")
            {
                cur_action = "object_setX";
            }
            else if (cur_token == "object_setY")
            {
                cur_action = "object_setY";
            }
            else if (cur_token == "object_getX")
            {
                cur_action = "object_getX";
            }
            else if (cur_token == "object_getY")
            {
                cur_action = "object_getY";
            }
            else if (cur_token == "object_setTag")
            {
                cur_action = "object_setTag";
            }
            else if (cur_token == "object_getTag")
            {
                cur_action = "object_getTag";
            }
            else if (cur_token == "object_setStatic")
            {
                cur_action = "object_setStatic";
            }
            else if (cur_token == "object_getStatic")
            {
                cur_action = "object_getStatic";
            }
            else if (cur_token == "object_setRigid")
            {
                cur_action = "object_setRigid";
            }
            else if (cur_token == "object_getRigid")
            {
                cur_action = "object_getRigid";
            }
            else if (cur_token == "object_setCollider")
            {
                cur_action = "object_setCollider";
            }
            else if (cur_token == "object_getCollider")
            {
                cur_action = "object_getCollider";
            }
            else if (cur_token == "object_registerScript")
            {
                cur_action = "object_registerScript";
            }
            else if (cur_token == "object_setText")
            {
                cur_action = "object_setText";
            }
            else if (cur_token == "object_getText")
            {
                cur_action = "object_getText";
            }
            else if (cur_token == "scene_addGameObject")
            {
                cur_action = "scene_addGameObject";
            }
            else if (cur_token == "scene_destroyGameObject")
            {
                cur_action = "scene_destroyGameObject";
            }
            else if (cur_token == "scene_activate")
            {
                cur_action = "scene_activate";
            }
            else if (cur_token == "native")
            {
            	cur_action = "native";
			}
			else if (cur_token == "navigation_start")
			{
				cur_action = "navigation_start";
			}
			else if (cur_token == "animation_start")
			{
				cur_action = "animation_start";
			}
			else if (cur_token == "import_header")
			{
				if (gen_code == 0x4)
				{
					cur_action = "import_header";
				}
				else
				{
					errors.push_back("Error : import_header only supported in c++ builds.");
				}
			}
            else
            {
                if (cur_token.substr(cur_token.length() - 1,1) == ":")
                {
                    bool isSuccess = true;

                    for(list<string>::iterator jmp_pnt = jump_points.begin();jmp_pnt != jump_points.end();jmp_pnt++)
                    {
                        if (*jmp_pnt == cur_token)
                        {
                            isSuccess = false;
                            break;
                        }
                    }

                    if (isSuccess)
                    {
                       if (this->gen_code != 0x2 && this->gen_code != 0x3) this->generated_codes.push_back(cur_token);
                        jump_points.push_back(cur_token.substr(0,cur_token.length() - 1));
                    }
                    else
                    {
                        errors.push_back("Error : Point already defined (" + cur_token + ")!");
                    }
                }
                else
                {
                    errors.push_back("Error : Unknown Token (" + cur_token + ")!");
                }
            }
        }
        else
        {
            if (cur_action == "end_line")
            {
                if (cur_token != ";")
                {
                    errors.push_back("Error : End of line expected.");
                }

                cur_action = "";
            }
            else if (cur_action == "comma")
            {
                if (cur_token != ",")
                {
                    errors.push_back("Error : Expected comma in the line.");
                }

                cur_action = cur_comma_param;
            }
            else if (cur_action == "import_header")
            {
            	bool isSuccess = false;
            	
            	if (cur_token.substr(0,1) == "@")
            	{
            		isSuccess = true;
				}
				
				if (isSuccess)
				{
					header_files.push_back(cur_token.substr(1,cur_token.length() - 1));
				}
				else
				{
					this->errors.push_back("Error : Expected a string literal as the header file name!");
				}
				
				cur_action = "";
			}
            else if (cur_action == "static_var")
            {
                if (cur_token == "int" || cur_token == "string")
                {
                    def_var = Variable( );
                    def_var.isStatic = true;
                    def_var.var_type = cur_token;
                }
                else
                {
                    errors.push_back("Error : Invalid type for static variable . (expected int or string).");
                }

                cur_action = "static_var_name";
            }
            else if (cur_action == "static_var_name")
            {
                bool isSucces = true;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        isSucces = false;
                        break;
                    }
                }

                if (isSucces)
                {
                    def_var.var_name = cur_token;

                    variable_list.push_back(def_var);
                }
                else
                {
                    errors.push_back("Error : Variable name is already used.");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "var")
            {
                if (cur_token == "int" || cur_token == "string")
                {
                    if (gen_code == 1 || gen_code == 4)
                    {
                        cur_code = cur_token + " ";
                    }
                    else if (gen_code == 2 || gen_code == 3)
                    {
                        if (cur_token == "string")
                        {
                            cur_code = "String ";
                        }
                        else
                        {
                            cur_code = cur_token + " ";
                        }
                    }

                    def_var = Variable( );
                    def_var.var_type = cur_token;
                }
                else
                {
                    errors.push_back("Error : Invalid type for variable . (expected int or string).");
                }

                cur_action = "var_name";
            }
            else if (cur_action == "var_name")
            {
                bool isSucces = true;

                for(list<Variable>::iterator cur_var = variable_list.begin();cur_var != variable_list.end();cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        isSucces = false;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code += cur_token + " = ";

                    if (def_var.var_type == "string")
                    {
                        cur_code += "\"\";";
                    }
                    else
                    {
                        cur_code += "0;";
                    }

                    def_var.var_name = cur_token;

                    variable_list.push_back(def_var);
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Variable name is already used.");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "add")
            {
                bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " += ";
                }
                else
                {
                    errors.push_back("Error : Variable in add not defined.");
                }

                cur_comma_param = "add_var";
                cur_action = "comma";
            }
            else if (cur_action == "add_var")
            {
                if (def_var.var_type == "int")
                {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token)
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in add statement.");
                      }
                }
                else if (def_var.var_type == "string")
                {
                    bool isSucces = false;
                    bool isVar = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token)
                            {
                                isSucces = true;
                                isVar = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                         if (cur_token.substr(0,1) == "@")
                         {
                             isSucces = true;
                         }
                      }

                      if (isSucces)
                      {
                          if (!isVar) cur_code += "\"" + cur_token.substr(1,cur_token.length( ) - 1) + "\";"; else cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in add statement.");
                      }
                }

                cur_action = "end_line";
            }
            else if (cur_action == "set")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    errors.push_back("Error : Variable in set not defined.");
                }

                cur_comma_param = "set_var";
                cur_action = "comma";
            }
            else if (cur_action == "set_var")
            {
                if (def_var.var_type == "int")
                {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type == "int")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in set statement.");
                      }
                }
                else if (def_var.var_type == "string")
                {
                    bool isSucces = false;
                    bool isVar = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                            {
                                isSucces = true;
                                isVar = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                         if (cur_token.substr(0,1) == "@")
                         {
                             isSucces = true;
                         }
                      }

                      if (isSucces)
                      {
                          if (!isVar) cur_code += "\"" + cur_token.substr(1,cur_token.length( ) - 1) + "\";"; else cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in set statement.");
                      }
                }

                cur_action = "end_line";
            }
            else if (cur_action == "sub")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " -= ";
                }
                else
                {
                    errors.push_back("Error : Variable in sub not defined.");
                }

                cur_comma_param = "sub_var";
                cur_action = "comma";
            }
            else if (cur_action == "sub_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in sub statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "mul")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " *= ";
                }
                else
                {
                    errors.push_back("Error : Variable in mul not defined.");
                }

                cur_comma_param = "mul_var";
                cur_action = "comma";
            }
            else if (cur_action == "mul_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool  isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in mul statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "div")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " /= ";
                }
                else
                {
                    errors.push_back("Error : Variable in div not defined.");
                }

                cur_comma_param = "div_var";
                cur_action = "comma";
            }
            else if (cur_action == "div_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in div statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setX")
            {
                bool isSuccess = false;
                bool isVar = false;
                bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code = "gameObject.pos_x = ";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_x = ";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_x = ";
                    }
                    
                	}
                	else
                	{
                		 if (isThis)
                    	 {
                        	 cur_code = "pGameObject->pos_x = ";
                    	 }
                    	 else if (!isVar)
                    	 {
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_x = ";
                    	 }
                    	 else
                    	 {
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_x = ";
                     	 }
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setX_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setX_value")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code += cur_token + ";";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Value or variable expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setY")
            {
                bool isSuccess = false;
                bool isVar = false;
                bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code = "gameObject.pos_y = ";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_y = ";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_y = ";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code = "pGameObject->pos_y = ";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_y = ";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_y = ";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setY_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setY_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                        if (isNum)
                        {
                            isSuccess = true;
                        }
                }

                if (isSuccess)
                {
                    cur_code += cur_token + ";";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Value or variable expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getX")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getX_name";
                cur_action = "comma";
            }
            else if (cur_action == "object_getX_name")
            {
                  bool isSuccess = false;
                  bool isVar = false;
                  bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                		
                    if (isThis)
                    {
                        cur_code += "gameObject.pos_x;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_x;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_x;";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject->pos_x;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_x;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_x;";
                    	}
					}
					

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getY")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getY_name";
                cur_action = "comma";
            }
            else if (cur_action == "object_getY_name")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.pos_y;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_y;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_y;";
                    }
				
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->pos_y;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_y;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_y;";
                    	}		
					}
				
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setTag")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setTag(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setTag(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setTag(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setTag(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setTag(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setTag(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setTag_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setTag_value")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getTag")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getTag_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getTag_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.tag;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.tag;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.tag;";
                    }
					
					
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance.tag;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.tag;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.tag;";
                    	}
					}
						
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setStatic")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setStatic(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setStatic(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setStatic(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setStatic(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setStatic(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setStatic(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setStatic_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setStatic_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getStatic")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getStatic_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getStatic_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance._static;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance._static;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance._static;";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance._static;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance._static;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance._static;";
                    	}
					}

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setRigid")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setRigid(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setRigid(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setRigid(";
                    }
                
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code = "pGameObject->setRigid(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setRigid(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setRigid(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setRigid_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setRigid_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getRigid")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getRigid_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getRigid_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.rigidbody;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.rigidbody;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.rigidbody;";
                    }

					}
					else
					{
						if (isThis)
						{
                         	cur_code += "pGameObject->obj_instance.rigidbody;";
                    	}
                   	 	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.rigidbody;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.rigidbody;";
                    	}
					}
				

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setCollider")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setCollider(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setCollider(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setCollider(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setCollider(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setCollider(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setCollider(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setCollider_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setCollider_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getCollider")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getCollider_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getCollider_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.collider;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.collider;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.collider;";
                    }

					}
					else
					{
							
                    	if (isThis)
                    	{
                        	 cur_code += "pGameObject->obj_instance.collider;";
                   	 	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.collider;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.collider;";
                    	}
					}

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_registerScript")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.registerScript(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").registerScript(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").registerScript(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	 cur_code = "pGameObject->registerScript(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->registerScript(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->registerScript(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_registerScript_script";
                cur_action = "comma";
            }
            else if (cur_action == "object_registerScript_script")
            {
                cur_code += "new " + cur_token +"());";
                this->generated_codes.push_back(cur_code);

                cur_action = "end_line";
            }
            else if (cur_action == "object_setText")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setText(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setText(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setText(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setText(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setText(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setText(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setText_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setText_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isVar = true;
                    }
                }

                if (isSuccess)
                {
                    if (isVar)
                    {
                        cur_code += cur_token + ");";
                    }
                    else
                    {
                        cur_code += "\"" + cur_token.substr(1,cur_token.length() - 1) + "\");";
                    }

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getText")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getText_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getText_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.text;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.text;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.text;";
                    }
					
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance.text;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.text;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.text;";
                    	}
					}
					
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_addGameObject")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	this->generated_codes.push_back("___o___ = new GameObject_Scene( );");
                	
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                
                    if (!isVar)
                    {
                        this->generated_codes.push_back("___o___.instance_name = \"" + cur_token.substr(1,cur_token.length() - 1) + "\";");
                    }
                    else
                    {
                        this->generated_codes.push_back("___o___.instance_name = " + cur_token + ";");
                    }
                    
                	}
                	else
                	{
                		if (!isVar)
                   		{
                        	this->generated_codes.push_back("___o___->instance_name = \"" + cur_token.substr(1,cur_token.length() - 1) + "\";");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("___o___->instance_name = " + cur_token + ";");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "scene_addGameObject_name";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_name")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (!isVar)
                    {
                        this->generated_codes.push_back("___o___.obj_instance = ObjectLoader.findGameObjectWithName(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    }
                    else
                    {
                        this->generated_codes.push_back("___o___.obj_instance = ObjectLoader.findGameObjectWithName(" + cur_token + ");");
                    }
                    
                	}
                	else
                	{
                		if (!isVar)
                    	{
                        	this->generated_codes.push_back("___o___->obj_instance = ObjectLoader.findGameObjectWithName(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("___o___->obj_instance = ObjectLoader.findGameObjectWithName(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "scene_addGameObject_px";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_px")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                     if (isNum)
                     {
                         isSuccess = true;
                     }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	this->generated_codes.push_back("___o___.pos_x = " + cur_token + ";");
                    }
                    else
                    {
                    	this->generated_codes.push_back("___o___->pos_x = " + cur_token + ";");
					}
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_comma_param = "scene_addGameObject_py";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_py")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	this->generated_codes.push_back("___o___.pos_y = " + cur_token + ";");
                    	this->generated_codes.push_back("HApplication.getActiveScene( ).loadGameObject(___o___);");
                	}	
                	else
                	{
                    	this->generated_codes.push_back("___o___->pos_y = " + cur_token + ";");
                    	this->generated_codes.push_back("HApplication::getActiveScene( )->loadGameObject(___o___);");
					}
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_destroyGameObject")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(gameObject.instance_name);");
                    }
                    else if (!isVar)
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    }
                    else
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(" + cur_token + ");");
                    }
                    
                	}
                	else
                	{
                	    if (isThis)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(gameObject.instance_name);");
                    	}
                    	else if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_activate")
            {
                 bool isSuccess = false;
                 bool isVar = false;
					
                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication.getActiveScene().endScene( ); HApplication.loadScene(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication.getActiveScene().endScene( ); HApplication.loadScene(" + cur_token + ");");
                    	}
                	}
                	else
                	{
                		if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene()->endScene( ); HApplication::loadScene(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene()->endScene( ); HApplication::loadScene(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "if")
            {
                 bool isSuccess = false;
                 list<Variable>::iterator cur_var;

                 for(cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token)
                        {
                            isSuccess = true;
                            break;
                        }
                }


                if (isSuccess)
                {
                    cur_code = "if (" + cur_token;
                    def_var = *cur_var;
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "if_symbol";
            }
            else if (cur_action == "if_symbol")
            {
                if (cur_token == ">" || cur_token == "<" || cur_token == ">=" || cur_token == "<=")
                {
                    if (def_var.var_type == "string")
                    {
                        errors.push_back("Error : Invalid condition check for string inside if!");
                    }
                    else
                    {
                        cur_code += " " + cur_token + " ";
                    }
                }
                else if (cur_token == "=")
                {
                    cur_code += " " + cur_token + " ";
                }

                cur_action = "if_cmp_val";
            }
            else if (cur_action == "if_cmp_val")
            {
                bool isSuccess = false;
                list<Variable>::iterator cur_var;

                 for(cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == def_var.var_type)
                        {
                            isSuccess = true;
                            break;
                        }
                }
				
				if (!isSuccess)
				{
					if (def_var.var_type == "string")
					{
						if (cur_token.substr(0,1) == "@")
						{
							isSuccess = true;
						}
					}
					else
					{
						bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
						
					}
				}
					
                if (isSuccess)
                {
                    cur_code += cur_token + ") {";
                    this->generated_codes.push_back(cur_code);
    				token++;
                    parseIf(variable_list, token,jump_points);
                }
                else
                {
                    this->errors.push_back("Error : Invalid type combination inside if!");
                }

                cur_action = "";
            }
            else if (cur_action == "goto")
            {
                bool isSuccess = false;

                for(list<string>::iterator pnt = jump_points.begin();pnt != jump_points.end();pnt++)
                {
                    if (cur_token == *pnt)
                    {
                        isSuccess = true;
                        break;
                    }
                }

                if (isSuccess)
                {
                    if (this->gen_code != 0x1)  this->generated_codes.push_back("goto " + cur_token + ";");
                }
                else
                {
                   errors.push_back("Error : Point not found in goto!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "navigation_start")
            {
                bool isSuccess = false;

                if (cur_token.substr(0,1) == "@")
                {   
                    isSuccess = true;
            	}

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	cur_code = "NavigationManager.registerNavigation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
                    }
                    else
                    {
                    	cur_code = "NavigationManager::registerNavigation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "navigation_start_object_name";
                cur_action = "comma";
			}
            else if (cur_action == "navigation_start_object_name")
            {
            	 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code += "gameObject,";
                    }
                    else if (!isVar)
                    {
                    	cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + "),";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject,";
                    	}
                    	else if (!isVar)
                    	{
                    		cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + "),";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "navigation_start_nav_speed";
                cur_action = "comma";
			}
			else if (cur_action == "navigation_start_nav_speed")
			{
				bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	cur_code += cur_token + "));";
                	
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
			}
			else if (cur_action == "animation_start")
			{
				bool isSuccess = false;

                if (cur_token.substr(0,1) == "@")
                {   
                    isSuccess = true;
            	}

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	cur_code = "AnimationManager.registerAnimation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
                    }
                    else
                    {
                    	cur_code = "AnimationManager::registerAnimation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "animation_start_object_name";
                cur_action = "comma";
			}
			else if (cur_action == "animation_start_object_name")
			{
				 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code += "gameObject,";
                    }
                    else if (!isVar)
                    {
                    	cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + "),";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject,";
                    	}
                    	else if (!isVar)
                    	{
                    		cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + "),";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "animation_repeat";
                cur_action = "comma";
			}
			else if (cur_action == "animation_repeat")
			{
				  bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	cur_code += cur_token + "));";
                	
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
			}
            else if (cur_action == "native")
            {
            	if (cur_token.substr(0,1) == "@")
            	{
            		this->generated_codes.push_back(cur_token.substr(1,cur_token.length() - 1));
				}
            	
            	cur_action = "end_line";
			}
        }
    }

    list<string> code_list = this->generated_codes;

    this->generated_codes = list<string>( );

    if (this->gen_code == 1)
    {
        // Windows Code.
		this->generated_codes.push_back("using Runtime;");
        this->generated_codes.push_back("public class " + class_name + " : HeavyScript");
        this->generated_codes.push_back("{");

        for(list<Variable>::iterator var_counter = variable_list.begin();var_counter != variable_list.end();var_counter++)
        {
            if (var_counter->isStatic)
            {
                this->generated_codes.push_back("public static " + var_counter->var_type + " " + var_counter->var_name + ";");
            }
        }

        this->generated_codes.push_back("public override void process(GameObject_Scene gameObject)");
        this->generated_codes.push_back("{");
        this->generated_codes.push_back("GameObject_Scene ___o___ = null;");
    }
    else if (this->gen_code == 2 || this->gen_code == 3)
    {
        // Java Code.
        if (this->gen_code == 3) this->generated_codes.push_back("package bin;");
        
        this->generated_codes.push_back("import jruntime.*;");
        this->generated_codes.push_back("public class " + class_name + " extends HeavyScript");
        this->generated_codes.push_back("{");

        for(list<Variable>::iterator var_counter = variable_list.begin();var_counter != variable_list.end();var_counter++)
        {
            if (var_counter->isStatic)
            {
                this->generated_codes.push_back("public static " + var_counter->var_type + " " + var_counter->var_name + ";");
            }
        }

        this->generated_codes.push_back("public void process(GameObject_Scene gameObject)");
        this->generated_codes.push_back("{");
        this->generated_codes.push_back("GameObject_Scene ___o___ = null;");
    }
    else if (this->gen_code == 4)
    {
    	// Native Windows / C++ Code.
    	// Generate header file.
		string header_file_nm = dir + "\\" + class_name + ".h";
    	ofstream header_stm(header_file_nm.c_str());
    	
    	header_stm << "#ifndef " << class_name << "_H" << endl;
    	header_stm << "#define " << class_name << "_H" << endl << endl;
    	
    	header_stm << "#include<HeavyEngine.h>" << endl;
    	
    	for(list<string>::iterator cur_header = header_files.begin();cur_header != header_files.end();cur_header++)
    	{
    		header_stm << "#include \"" + *cur_header + ".h\"" << endl;
		}
    	
    	header_stm << "class " << class_name << " {" << endl;
    	header_stm << "public:" << endl;
    	header_stm << "void process(void *);" << endl;
    	header_stm << "};" << endl;
		 
    	for(list<Variable>::iterator var_counter = variable_list.begin();var_counter != variable_list.end();var_counter++)
        {
            if (var_counter->isStatic)
            {
                header_stm << "extern " << var_counter->var_type << " " << var_counter->var_name << ";" << endl;
            }
        }
    	
    	header_stm << "#endif" << endl;
    	
    	header_stm.close();
    	
    	// Generate source file.
    	
    	this->generated_codes.push_back("#include \"" + class_name + ".h\"");
    	
        for(list<Variable>::iterator var_counter = variable_list.begin();var_counter != variable_list.end();var_counter++)
        {
            if (var_counter->isStatic)
            {
                this->generated_codes.push_back(var_counter->var_type + " " + var_counter->var_name + ";");
            }
        }

        this->generated_codes.push_back("public void " + class_name + "::process(void * gameObject)");
        this->generated_codes.push_back("{");
        this->generated_codes.push_back("GameObject_Scene * ___o___ = NULL;");
        this->generated_codes.push_back("GameObject_Scene * pGameObject = (GameObject_Scene *) gameObject;");
	}

    for(list<string>::iterator cp_code = code_list.begin();cp_code != code_list.end( );cp_code++ )
    {
        this->generated_codes.push_back(*cp_code);
    }

    if (errors.size() > 0)
    {
        return false;
    }

    this->generated_codes.push_back("}");
    this->generated_codes.push_back("}");

    return true;
}

list<string> Parser::getCodes()
{
    return generated_codes;
}

list<string> Parser::getErrors ( )
{
    return errors;
}

void Parser::parseIf( list<Variable> & var_list, list<string>::iterator & token , list<string> jump_points )
{
    string cur_action;
    string cur_code;
    string cur_comma_param;
    list<Variable> variable_list; // Static variable list.
    Variable def_var;
	
	for(list<Variable>::iterator var = var_list.begin();var != var_list.end();var++)
	{
		if (var->isStatic)
		{
			variable_list.push_back(*var);
		}
	}
	
    for(;token != tokens.end( );token++)
    {
        string cur_token = *token;

        if (cur_action == "")
        {
            if (cur_token == "var")
            {
                cur_action = "var";
            }
            else if (cur_token == "set")
            {
                cur_action = "set";
            }
            else if (cur_token == "add")
            {
                cur_action = "add";
            }
            else if (cur_token == "sub")
            {
                cur_action = "sub";
            }
            else if (cur_token == "mul")
            {
                cur_action = "mul";
            }
            else if (cur_token == "div")
            {
                cur_action = "div";
            }
            else if (cur_token == "if")
            {
                cur_action = "if";
            }
            else if (cur_token == "goto")
            {
                cur_action = "goto";
            }
            else if (cur_token == "object_setX")
            {
                cur_action = "object_setX";
            }
            else if (cur_token == "object_setY")
            {
                cur_action = "object_setY";
            }
            else if (cur_token == "object_getX")
            {
                cur_action = "object_getX";
            }
            else if (cur_token == "object_getY")
            {
                cur_action = "object_getY";
            }
            else if (cur_token == "object_setTag")
            {
                cur_action = "object_setTag";
            }
            else if (cur_token == "object_getTag")
            {
                cur_action = "object_getTag";
            }
            else if (cur_token == "object_setStatic")
            {
                cur_action = "object_setStatic";
            }
            else if (cur_token == "object_getStatic")
            {
                cur_action = "object_getStatic";
            }
            else if (cur_token == "object_setRigid")
            {
                cur_action = "object_setRigid";
            }
            else if (cur_token == "object_getRigid")
            {
                cur_action = "object_getRigid";
            }
            else if (cur_token == "object_setCollider")
            {
                cur_action = "object_setCollider";
            }
            else if (cur_token == "object_getCollider")
            {
                cur_action = "object_getCollider";
            }
            else if (cur_token == "object_registerScript")
            {
                cur_action = "object_registerScript";
            }
            else if (cur_token == "object_setText")
            {
                cur_action = "object_setText";
            }
            else if (cur_token == "object_getText")
            {
                cur_action = "object_getText";
            }
            else if (cur_token == "scene_addGameObject")
            {
                cur_action = "scene_addGameObject";
            }
            else if (cur_token == "scene_destroyGameObject")
            {
                cur_action = "scene_destroyGameObject";
            }
            else if (cur_token == "scene_activate")
            {
                cur_action = "scene_activate";
            }
            else if (cur_token == "native")
            {
            	cur_action = "native";
			}
			else if (cur_token == "navigation_start")
			{
				cur_action = "navigation_start";
			}
			else if (cur_token == "animation_start")
			{
				cur_action = "animation_start";
			}
			else if (cur_token == "end")
			{
				return;
			}
            else
            {
                if (cur_token.substr(cur_token.length() - 1,1) == ":")
                {
                    bool isSuccess = true;

                    for(list<string>::iterator jmp_pnt = jump_points.begin();jmp_pnt != jump_points.end();jmp_pnt++)
                    {
                        if (*jmp_pnt == cur_token)
                        {
                            isSuccess = false;
                            break;
                        }
                    }

                    if (isSuccess)
                    {
                       if (this->gen_code != 0x2 && this->gen_code != 0x3) this->generated_codes.push_back(cur_token);
                        jump_points.push_back(cur_token.substr(0,cur_token.length() - 1));
                    }
                    else
                    {
                        errors.push_back("Error : Point already defined (" + cur_token + ")!");
                    }
                }
                else
                {
                    errors.push_back("Error : Unknown Token (" + cur_token + ")!");
                }
            }
        }
        else
        {
            if (cur_action == "end_line")
            {
                if (cur_token != ";")
                {
                    errors.push_back("Error : End of line expected.");
                }

                cur_action = "";
            }
            else if (cur_action == "comma")
            {
                if (cur_token != ",")
                {
                    errors.push_back("Error : Expected comma in the line.");
                }

                cur_action = cur_comma_param;
            }
            else if (cur_action == "var")
            {
                if (cur_token == "int" || cur_token == "string")
                {
                    if (gen_code == 1 || gen_code == 4)
                    {
                        cur_code = cur_token + " ";
                    }
                    else if (gen_code == 2 || gen_code == 3)
                    {
                        if (cur_token == "string")
                        {
                            cur_code = "String ";
                        }
                        else
                        {
                            cur_code = cur_token + " ";
                        }
                    }

                    def_var = Variable( );
                    def_var.var_type = cur_token;
                }
                else
                {
                    errors.push_back("Error : Invalid type for variable . (expected int or string).");
                }

                cur_action = "var_name";
            }
            else if (cur_action == "var_name")
            {
                bool isSucces = true;

                for(list<Variable>::iterator cur_var = variable_list.begin();cur_var != variable_list.end();cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        isSucces = false;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code += cur_token + " = ";

                    if (def_var.var_type == "string")
                    {
                        cur_code += "\"\";";
                    }
                    else
                    {
                        cur_code += "0;";
                    }

                    def_var.var_name = cur_token;

                    variable_list.push_back(def_var);
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Variable name is already used.");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "add")
            {
                bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " += ";
                }
                else
                {
                    errors.push_back("Error : Variable in add not defined.");
                }

                cur_comma_param = "add_var";
                cur_action = "comma";
            }
            else if (cur_action == "add_var")
            {
                if (def_var.var_type == "int")
                {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token)
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in add statement.");
                      }
                }
                else if (def_var.var_type == "string")
                {
                    bool isSucces = false;
                    bool isVar = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token)
                            {
                                isSucces = true;
                                isVar = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                         if (cur_token.substr(0,1) == "@")
                         {
                             isSucces = true;
                         }
                      }

                      if (isSucces)
                      {
                          if (!isVar) cur_code += "\"" + cur_token.substr(1,cur_token.length( ) - 1) + "\";"; else cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in add statement.");
                      }
                }

                cur_action = "end_line";
            }
            else if (cur_action == "set")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token)
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    errors.push_back("Error : Variable in set not defined.");
                }

                cur_comma_param = "set_var";
                cur_action = "comma";
            }
            else if (cur_action == "set_var")
            {
                if (def_var.var_type == "int")
                {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type == "int")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in set statement.");
                      }
                }
                else if (def_var.var_type == "string")
                {
                    bool isSucces = false;
                    bool isVar = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                            {
                                isSucces = true;
                                isVar = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                         if (cur_token.substr(0,1) == "@")
                         {
                             isSucces = true;
                         }
                      }

                      if (isSucces)
                      {
                          if (!isVar) cur_code += "\"" + cur_token.substr(1,cur_token.length( ) - 1) + "\";"; else cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in set statement.");
                      }
                }

                cur_action = "end_line";
            }
            else if (cur_action == "sub")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " -= ";
                }
                else
                {
                    errors.push_back("Error : Variable in sub not defined.");
                }

                cur_comma_param = "sub_var";
                cur_action = "comma";
            }
            else if (cur_action == "sub_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in sub statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "mul")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " *= ";
                }
                else
                {
                    errors.push_back("Error : Variable in mul not defined.");
                }

                cur_comma_param = "mul_var";
                cur_action = "comma";
            }
            else if (cur_action == "mul_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool  isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in mul statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "div")
            {
               bool isSucces = false;

                for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                {
                    if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                    {
                        def_var = *cur_var;
                        isSucces = true;
                        break;
                    }
                }

                if (isSucces)
                {
                    cur_code = cur_token + " /= ";
                }
                else
                {
                    errors.push_back("Error : Variable in div not defined.");
                }

                cur_comma_param = "div_var";
                cur_action = "comma";
            }
            else if (cur_action == "div_var")
            {
                    bool isSucces = false;

                      for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                      {
                            if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                            {
                                isSucces = true;
                                break;
                            }
                      }

                      if (!isSucces)
                      {
                          bool isSuccess = true;

                          for(int cnt = 0;cnt < cur_token.length();cnt++)
                          {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSucces = false;
                                  isSuccess = false;
                                  break;
                              }
                          }

                          if (isSuccess)
                          {
                              isSucces = true;
                          }
                      }

                      if (isSucces)
                      {
                          cur_code += cur_token + ";";
                          this->generated_codes.push_back(cur_code);
                      }
                      else
                      {
                          this->errors.push_back("Error : Variable or value expected in div statement.");
                      }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setX")
            {
                bool isSuccess = false;
                bool isVar = false;
                bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code = "gameObject.pos_x = ";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_x = ";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_x = ";
                    }
                    
                	}
                	else
                	{
                		 if (isThis)
                    	 {
                        	 cur_code = "pGameObject->pos_x = ";
                    	 }
                    	 else if (!isVar)
                    	 {
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_x = ";
                    	 }
                    	 else
                    	 {
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_x = ";
                     	 }
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setX_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setX_value")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code += cur_token + ";";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Value or variable expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setY")
            {
                bool isSuccess = false;
                bool isVar = false;
                bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code = "gameObject.pos_y = ";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_y = ";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_y = ";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code = "pGameObject->pos_y = ";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_y = ";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_y = ";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setY_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setY_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                        if (isNum)
                        {
                            isSuccess = true;
                        }
                }

                if (isSuccess)
                {
                    cur_code += cur_token + ";";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    errors.push_back("Error : Value or variable expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getX")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getX_name";
                cur_action = "comma";
            }
            else if (cur_action == "object_getX_name")
            {
                  bool isSuccess = false;
                  bool isVar = false;
                  bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                		
                    if (isThis)
                    {
                        cur_code += "gameObject.pos_x;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_x;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_x;";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject->pos_x;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_x;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_x;";
                    	}
					}
					

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getY")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getY_name";
                cur_action = "comma";
            }
            else if (cur_action == "object_getY_name")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.pos_y;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").pos_y;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").pos_y;";
                    }
				
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->pos_y;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->pos_y;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->pos_y;";
                    	}		
					}
				
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setTag")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setTag(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setTag(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setTag(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setTag(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setTag(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setTag(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setTag_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setTag_value")
            {
                bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getTag")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getTag_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getTag_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.tag;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.tag;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.tag;";
                    }
					
					
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance.tag;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.tag;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.tag;";
                    	}
					}
						
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setStatic")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setStatic(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setStatic(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setStatic(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setStatic(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setStatic(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setStatic(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setStatic_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setStatic_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getStatic")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getStatic_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getStatic_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance._static;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance._static;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance._static;";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance._static;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance._static;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance._static;";
                    	}
					}

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setRigid")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setRigid(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setRigid(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setRigid(";
                    }
                
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code = "pGameObject->setRigid(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setRigid(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setRigid(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setRigid_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setRigid_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getRigid")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getRigid_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getRigid_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.rigidbody;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.rigidbody;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.rigidbody;";
                    }

					}
					else
					{
						if (isThis)
						{
                         	cur_code += "pGameObject->obj_instance.rigidbody;";
                    	}
                   	 	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.rigidbody;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.rigidbody;";
                    	}
					}
				

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_setCollider")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setCollider(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setCollider(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setCollider(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setCollider(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setCollider(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setCollider(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setCollider_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setCollider_value")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + ");";
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getCollider")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getCollider_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getCollider_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
					
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.collider;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.collider;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.collider;";
                    }

					}
					else
					{
							
                    	if (isThis)
                    	{
                        	 cur_code += "pGameObject->obj_instance.collider;";
                   	 	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.collider;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.collider;";
                    	}
					}

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_registerScript")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.registerScript(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").registerScript(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").registerScript(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	 cur_code = "pGameObject->registerScript(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->registerScript(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->registerScript(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_registerScript_script";
                cur_action = "comma";
            }
            else if (cur_action == "object_registerScript_script")
            {
                cur_code += "new " + cur_token +"());";
                this->generated_codes.push_back(cur_code);

                cur_action = "end_line";
            }
            else if (cur_action == "object_setText")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code = "gameObject.setText(";
                    }
                    else if (!isVar)
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").setText(";
                    }
                    else
                    {
                        cur_code = "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").setText(";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                         	cur_code = "pGameObject->setText(";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->setText(";
                    	}
                    	else
                    	{
                        	cur_code = "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->setText(";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "object_setText_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_setText_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isVar = true;
                    }
                }

                if (isSuccess)
                {
                    if (isVar)
                    {
                        cur_code += cur_token + ");";
                    }
                    else
                    {
                        cur_code += "\"" + cur_token.substr(1,cur_token.length() - 1) + "\");";
                    }

                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "object_getText")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (isSuccess)
                {
                    cur_code = cur_token + " = ";
                }
                else
                {
                    this->errors.push_back("Error : Variable not found!");
                }

                cur_comma_param = "object_getText_value";
                cur_action = "comma";
            }
            else if (cur_action == "object_getText_value")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                         cur_code += "gameObject.obj_instance.text;";
                    }
                    else if (!isVar)
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\").obj_instance.text;";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + ").obj_instance.text;";
                    }
					
					}
					else
					{
						if (isThis)
                    	{
                         	cur_code += "pGameObject->obj_instance.text;";
                    	}
                    	else if (!isVar)
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\")->obj_instance.text;";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + ")->obj_instance.text;";
                    	}
					}
					
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_addGameObject")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	this->generated_codes.push_back("___o___ = new GameObject_Scene( );");
                	
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (!isVar)
                    {
                        this->generated_codes.push_back("___o___.instance_name = \"" + cur_token.substr(1,cur_token.length() - 1) + "\";");
                    }
                    else
                    {
                        this->generated_codes.push_back("___o___.instance_name = " + cur_token + ";");
                    }
                    
                	}
                	else
                	{
                		if (!isVar)
                   		{
                        	this->generated_codes.push_back("___o___->instance_name = \"" + cur_token.substr(1,cur_token.length() - 1) + "\";");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("___o___->instance_name = " + cur_token + ";");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "scene_addGameObject_name";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_name")
            {
                 bool isSuccess = false;
                 bool isVar = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (!isVar)
                    {
                        this->generated_codes.push_back("___o___.obj_instance = ObjectLoader.findGameObjectWithName(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    }
                    else
                    {
                        this->generated_codes.push_back("___o___.obj_instance = ObjectLoader.findGameObjectWithName(" + cur_token + ");");
                    }
                    
                	}
                	else
                	{
                		if (!isVar)
                    	{
                        	this->generated_codes.push_back("___o___->obj_instance = ObjectLoader.findGameObjectWithName(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("___o___->obj_instance = ObjectLoader.findGameObjectWithName(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "scene_addGameObject_px";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_px")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                     if (isNum)
                     {
                         isSuccess = true;
                     }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	this->generated_codes.push_back("___o___.pos_x = " + cur_token + ";");
                    }
                    else
                    {
                    	this->generated_codes.push_back("___o___->pos_x = " + cur_token + ";");
					}
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_comma_param = "scene_addGameObject_py";
                cur_action = "comma";
            }
            else if (cur_action == "scene_addGameObject_py")
            {
                 bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	this->generated_codes.push_back("___o___.pos_y = " + cur_token + ";");
                    	this->generated_codes.push_back("HApplication.getActiveScene( ).loadGameObject(___o___);");
                	}	
                	else
                	{
                    	this->generated_codes.push_back("___o___->pos_y = " + cur_token + ";");
                    	this->generated_codes.push_back("HApplication::getActiveScene( )->loadGameObject(___o___);");
					}
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_destroyGameObject")
            {
                 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(gameObject.instance_name);");
                    }
                    else if (!isVar)
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    }
                    else
                    {
                        this->generated_codes.push_back("HApplication.getActiveScene( ).destroyGameObject(" + cur_token + ");");
                    }
                    
                	}
                	else
                	{
                	    if (isThis)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(gameObject.instance_name);");
                    	}
                    	else if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene( )->destroyGameObject(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "scene_activate")
            {
                 bool isSuccess = false;
                 bool isVar = false;
					
                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication.getActiveScene().endScene( ); HApplication.loadScene(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication.getActiveScene().endScene( ); HApplication.loadScene(" + cur_token + ");");
                    	}
                	}
                	else
                	{
                		if (!isVar)
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene()->endScene( ); HApplication::loadScene(\"" + cur_token.substr(1,cur_token.length() - 1) + "\");");
                    	}
                    	else
                    	{
                        	this->generated_codes.push_back("HApplication::getActiveScene()->endScene( ); HApplication::loadScene(" + cur_token + ");");
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "if")
            {
                 bool isSuccess = false;
                 list<Variable>::iterator cur_var;

                 for(cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token)
                        {
                            isSuccess = true;
                            break;
                        }
                }


                if (isSuccess)
                {
                    cur_code = "if (" + cur_token;
                    def_var = *cur_var;
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_action = "if_symbol";
            }
            else if (cur_action == "if_symbol")
            {
                if (cur_token == ">" || cur_token == "<" || cur_token == ">=" || cur_token == "<=")
                {
                    if (def_var.var_type == "string")
                    {
                        errors.push_back("Error : Invalid condition check for string inside if!");
                    }
                    else
                    {
                        cur_code += " " + cur_token + " ";
                    }
                }
                else if (cur_token == "=")
                {
                    cur_code += " " + cur_token + " ";
                }

                cur_action = "if_cmp_val";
            }
            else if (cur_action == "if_cmp_val")
            {
                bool isSuccess = false;
                list<Variable>::iterator cur_var;

                 for(cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == def_var.var_type)
                        {
                            isSuccess = true;
                            break;
                        }
                }
				
				if (!isSuccess)
				{
					if (def_var.var_type == "string")
					{
						if (cur_token.substr(0,1) == "@")
						{
							isSuccess = true;
						}
					}
					else
					{
						bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
						
					}
				}
					
                if (isSuccess)
                {
                    cur_code += cur_token + ") {";
                    this->generated_codes.push_back(cur_code);
    				token++;
                    parseIf(variable_list, token,jump_points);
                }
                else
                {
                    this->errors.push_back("Error : Invalid type combination inside if!");
                }

                cur_action = "";
            }
            else if (cur_action == "goto")
            {
                bool isSuccess = false;

                for(list<string>::iterator pnt = jump_points.begin();pnt != jump_points.end();pnt++)
                {
                    if (cur_token == *pnt)
                    {
                        isSuccess = true;
                        break;
                    }
                }

                if (isSuccess)
                {
                    if (this->gen_code != 0x1)  this->generated_codes.push_back("goto " + cur_token + ";");
                }
                else
                {
                   errors.push_back("Error : Point not found in goto!");
                }

                cur_action = "end_line";
            }
            else if (cur_action == "navigation_start")
            {
                bool isSuccess = false;

                if (cur_token.substr(0,1) == "@")
                {   
                    isSuccess = true;
            	}

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	cur_code = "NavigationManager.registerNavigation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
                    }
                    else
                    {
                    	cur_code = "NavigationManager::registerNavigation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "navigation_start_object_name";
                cur_action = "comma";
			}
            else if (cur_action == "navigation_start_object_name")
            {
            	 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code += "gameObject,";
                    }
                    else if (!isVar)
                    {
                    	cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + "),";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject,";
                    	}
                    	else if (!isVar)
                    	{
                    		cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + "),";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "navigation_start_nav_speed";
                cur_action = "comma";
			}
			else if (cur_action == "navigation_start_nav_speed")
			{
				bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	cur_code += cur_token + "));";
                	
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
			}
			else if (cur_action == "animation_start")
			{
				bool isSuccess = false;

                if (cur_token.substr(0,1) == "@")
                {   
                    isSuccess = true;
            	}

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                    	cur_code = "AnimationManager.registerAnimation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
                    }
                    else
                    {
                    	cur_code = "AnimationManager::registerAnimation(new " + cur_token.substr(1,cur_token.length() - 1) +  "(";
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "animation_start_object_name";
                cur_action = "comma";
			}
			else if (cur_action == "animation_start_object_name")
			{
				 bool isSuccess = false;
                 bool isVar = false;
                 bool isThis = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type == "string")
                        {
                            isSuccess = true;
                            isVar = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    if (cur_token.substr(0,1) == "@")
                    {
                        isSuccess = true;
                    }
                    else if (cur_token == "this")
                    {
                        isSuccess = true;
                        isThis = true;
                    }
                }

                if (isSuccess)
                {
                	if (gen_code == 1 || gen_code == 2 || gen_code == 3)
                	{
                	
                    if (isThis)
                    {
                        cur_code += "gameObject,";
                    }
                    else if (!isVar)
                    {
                    	cur_code += "HApplication.getActiveScene( ).findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    }
                    else
                    {
                        cur_code += "HApplication.getActiveScene( ).findGameObject(" + cur_token + "),";
                    }
                    
                	}
                	else
                	{
                		if (isThis)
                    	{
                        	cur_code += "pGameObject,";
                    	}
                    	else if (!isVar)
                    	{
                    		cur_code += "HApplication::getActiveScene( )->findGameObject(\"" + cur_token.substr(1,cur_token.length() - 1) + "\"),";
                    	}
                    	else
                    	{
                        	cur_code += "HApplication::getActiveScene( )->findGameObject(" + cur_token + "),";
                    	}
					}
                }
                else
                {
                    this->errors.push_back("Error : Object name not found!");
                }

                cur_comma_param = "animation_repeat";
                cur_action = "comma";
			}
			else if (cur_action == "animation_repeat")
			{
				  bool isSuccess = false;

                 for(list<Variable>::iterator cur_var = variable_list.begin( );cur_var != variable_list.end( );cur_var++)
                 {
                        if (cur_var->var_name == cur_token && cur_var->var_type != "string")
                        {
                            isSuccess = true;
                            break;
                        }
                }

                if (!isSuccess)
                {
                    bool isNum = true;

                     for(int cnt = 0;cnt < cur_token.length();cnt++)
                      {
                              string cur_dig = cur_token.substr(cnt,1);

                              if (cur_dig != "0" && cur_dig != "1" && cur_dig != "2" && cur_dig != "3" && cur_dig != "4" && cur_dig != "5" && cur_dig != "6" && cur_dig != "7" && cur_dig != "8" && cur_dig != "9")
                              {
                                  isSuccess = false;
                                  isNum = false;
                                  break;
                              }
                        }

                    if (isNum)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                	cur_code += cur_token + "));";
                	
                    this->generated_codes.push_back(cur_code);
                }
                else
                {
                    this->errors.push_back("Error : Value or Variable Expected!");
                }

                cur_action = "end_line";
			}
            else if (cur_action == "native")
            {
            	if (cur_token.substr(0,1) == "@")
            	{
            		this->generated_codes.push_back(cur_token.substr(1,cur_token.length() - 1));
				}
            	
            	cur_action = "end_line";
			}
        }
    }
}
