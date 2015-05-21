#include "SceneManager.h"

static std::list<Scene> scene_array;

void SceneManager::addScene(Scene scene)
{
	scene_array.push_back(scene);
}

Scene SceneManager::getScene(std::string scene_name)
{
	for (std::list<Scene>::iterator cur_scene = scene_array.begin(); cur_scene != scene_array.end(); cur_scene++)
	{
		if (cur_scene->name == scene_name)
		{
			return *cur_scene;
		}
	}
}