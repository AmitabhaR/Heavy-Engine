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

#define PLATFORM_WINDOWS 0x1
#define PLATFORM_JAVA 0x2
#define PLATFORM_JAVA_ME 0x3
#define PLATFORM_WINDOWS_NATIVE 0x4
#define PLATFORM_LINUX_NATIVE 0x5
#define PLATFORM_GCW_ZERO_NATIVE 0x6
//#define PLATFORM_WINDOWS_STORE 0x7

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
