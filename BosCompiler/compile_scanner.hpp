/*
	 BosCompiler v1.1 , Licensed under GNU GPL v2.0 . 
	 For more information , visit http://www.gnu.org/licenses/gpl-2.0.html .
	 Developed by Reo Studio @ 2015 - 2016
*/

#ifndef COMPILE_SCANNER_HPP

#define COMPILE_SCANNER_HPP

#include<iostream> // For string class.
#include<list> // For std::list class.
#include<fstream> // For filestream.

using namespace std;

/*
    Note : This class is for scanning all the tokens present in a file.

*/

class Scanner
{
public:

    Scanner(string);

    bool start( );
    list<string> getTokens( );
    list<string> getErrors( );

private:
    list<string> token_list;
    list<string> error_list;
    string file_name;
};

#endif // COMPILE_SCANNER_HPP
