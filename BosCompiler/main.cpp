/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/



#include <iostream>
#include <fstream>
#include "compile_main.hpp"

#define PLATFORM_WINDOWS 0x1
#define PLATFORM_JAVA 0x2
#define PLATFORM_JAVA_ME 0x3
#define PLATFORM_WINDOWS_NATIVE 0x4


using namespace std;

int main(int argc , char * argv[])
{
    if (argc < 2)
    {
        cout << "No arguments passed!" << endl;
        return 0;
    }

    string cur_action = "";
    string in_file = "";
    string out_file = "";
    int gen_code = 0;

    for(int cnt = 1;cnt < argc;cnt++)
    {
        string cur_arg(argv[cnt]);

        if (cur_action == "")
        {
            if (cur_arg == "-p")
            {
                cur_action = "gen_code";
            }
            else if (cur_arg == "-o")
            {
                cur_action = "output_file";
            }
            else
            {
                ifstream stm(cur_arg.c_str( ));

                if (!stm.is_open())
                {
                    cout << cur_arg << " not found!" << endl;
                    return 0;
                }
                else
                {
                    in_file = cur_arg;
                }
            }
        }
        else
        {
            if (cur_action == "gen_code")
            {
                if (cur_arg == "1")
                {
                    gen_code = PLATFORM_WINDOWS;
                }
                else if (cur_arg == "2")
                {
                    gen_code = PLATFORM_JAVA;
                }
                else if ( cur_arg == "3")
                {
                	gen_code = PLATFORM_JAVA_ME;
				}
				else if ( cur_arg == "4")
				{
					gen_code = PLATFORM_WINDOWS_NATIVE;
				}
                else
                {
                    cout << cur_arg << " is not a valid platform id." << endl;
                    return 0;
                }

                cur_action = "";
            }
            else if (cur_action == "output_file")
            {
                out_file = cur_arg;
                cur_action = "";
            }
        }
    }


    Compiler compiler(in_file,out_file,gen_code);

    if (!compiler.startCompile( ))
    {
        cout << "Compile failed!" << endl;
        return 0;
    }
    else
    {
        cout << "Compile successfull!" << endl;
        return 1;
    }


    return 1;
}
