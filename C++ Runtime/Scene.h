#ifndef SCENE_H

#define SCENE_H

#include<SDL.h>

#include "GameObject_Scene.h"

struct DrawableGameObject
{
	int depth;
	int index;
};

class Scene
{
private:	

	std::list<DrawableGameObject> sortedArray;
	std::list<GameObject_Scene_ptr> object_array;
	SDL_Surface * canvas;

	bool checkSorted(std::list<DrawableGameObject> *);

	void sortElements(std::list<DrawableGameObject> *);

	void makeSorting();

public:
	int A = 0, R = 0, G = 0, B = 0;
	int speed = 1;
	int gravity = 0;
	std::string name = "";
	bool isRunning = false;

	void startScene(SDL_Surface *);

	void updateScene();

	void drawScene();

	void endScene();

	bool loadGameObject(GameObject_Scene_ptr);

	void destroyGameObject(std::string);

	GameObject_Scene *  findGameObject(std::string);

	std::list<GameObject_Scene_ptr> findGameObject(int);

	std::list<GameObject_Scene_ptr> getAllGameObjects();
};

typedef Scene* Scene_ptr;

#endif