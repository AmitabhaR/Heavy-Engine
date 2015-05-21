/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/

//#define TESTING // Needed for testing and printing a token.

#ifndef COMPILE_MAIN_HPP

#define COMPILE_MAIN_HPP

#include <iostream> // For string class.

using namespace std;

/*
    Note : This class consists of the the compile start function.
*/

class Compiler
{
public:
    Compiler( string , string , int);

    bool startCompile(); // Returns compile success or failure.

private:

    int gen_code;
    string out_file;
    string in_file;
};

#endif // COMPILE_MAIN_HPP
