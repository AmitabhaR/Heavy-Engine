#ifndef OBJECT_MANAGER_H

#define OBJECT_MANAGER_H

#include "GameObject.h"
#include <list>

class ObjectManager
{
public:

	static void loadObject(std::string , std::string , std::string , int , bool , bool , bool , bool ,int , int );

	static std::list<GameObject> findGameObjectWithTag(int );

	static GameObject findGameObjectWithName(std::string );
};

#endif