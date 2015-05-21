
#ifndef RESOURCE_MANAGER_H

#define RESOURCE_MANAGER_H

#include "HExtra.h"
#include<string>

class ResourceManager
{
public:
	static Image findImage(std::string);

	static File getResourceAsStream(std::string);

	static std::string getResource(std::string);
};

#endif