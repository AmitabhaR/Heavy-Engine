
#ifndef GAMEOBJECT_H

#define GAMEOBJECT_H

#include "HExtra.h"
#include <string>

struct GameObject
{
	std::string name;
	std::string text;
	int tag;
	HImage img;
	bool _static;
	bool physics;
	bool rigidbody;
    bool collider;
    std::string font_name;
	int font_size;
	Color color;
};

#endif