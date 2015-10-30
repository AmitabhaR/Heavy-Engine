
#ifndef SCENE_MANAGER_H

#define SCENE_MANAGER_H

#include "Scene.h"
#include<list>
#include<string>

class SceneManager
{

public:
	static void addScene(Scene);

	static Scene getScene(std::string );
};

#endif