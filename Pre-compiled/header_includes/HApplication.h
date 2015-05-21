#ifndef HAPPLICATION_H

#define HAPPLICATION_H

#include<SDL.h>
#include<list>

#include "HExtra.h"
#include "Scene.h"
#include "Vector2.h"

typedef void(*OnCollisionHandler)(GameObject_Scene *, GameObject_Scene *);

class HApplication
{
public:

	static void loadScene(Scene &);

	static Scene_ptr getActiveScene();

	static void setSize(int , int );

	static Rect getSize();

	static Window getWindowHandle();

	static void Initialize(std::string);

	static Vector2 getMousePosition();

	static Canvas getCanvas();

	static void Exit();
	
	static void start();

	static std::list<OnCollisionHandler> getCollisionHandlers();

	static std::list<OnKeyDown> getKeyboardHandlers();
	
	static std::list<OnMouseDown> getMouseDownHandlers();

	static std::list<OnMouseMove> getMouseMoveHandlers();

	static void registerKeyboardHandler(OnKeyDown );
	
	static void registerMouseDownHandler(OnMouseDown);

	static void registerMouseMoveHandler(OnMouseMove);

	static void registerCollisionHandler(OnCollisionHandler);
};

#endif