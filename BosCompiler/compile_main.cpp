/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/

#include "compile_main.hpp"
#include "compile_scanner.hpp"
#include "compile_parser.hpp"

Compiler::Compiler( string in , string out , int gen_code )
{
    this->in_file = in;
    this->out_file = out;
    this->gen_code = gen_code;
}

bool Compiler::startCompile( )
{
    Scanner scanner( in_file );

    if (!scanner.start())
    {
        list<string> error_list = scanner.getErrors();
        list<string>::iterator errors = error_list.begin( );

        if (error_list.size() > 0)
        {
            for(;errors != error_list.end();errors++)
            {
                cout << *errors << endl;
            }
        }

        return false;
    }
    else
    {
    	#if TESTING
    	
        list<string> tok_list = scanner.getTokens();

        for(list<string>::iterator tok = tok_list.begin( );tok != tok_list.end();tok++)
        {
            cout << *tok << endl;
        } 
        
        #endif

        // Call parser for generating native code.
        Parser parser(scanner.getTokens() , gen_code);

        if (!parser.parse(out_file.substr(out_file.find_last_of("\\") + 1,out_file.find_last_of(".") - (out_file.find_last_of("\\") + 1)),out_file.substr(0,out_file.find_last_of("\\"))))
        {
            list<string> errors = parser.getErrors( );

            for(list<string>::iterator error = errors.begin( );error != errors.end();error++)
            {
                cout << *error << endl;
            }

            return false;
        }
        else
        {
            list<string> gen_codes = parser.getCodes( );
            ofstream out_stm(out_file.c_str( ));

            for(list<string>::iterator code = gen_codes.begin( );code != gen_codes.end();code++)
            {
                out_stm << *code << endl;
            }

            return true;
        }
    }

    return false;
}
