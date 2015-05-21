#include "HApplication.h"

static Scene * cur_scene;
static Window mainWindow;
static Canvas canvas;
static Vector2 mousePosition;
static std::list<OnKeyDown> keydown_handlers;
static std::list<OnMouseDown> mousedown_handlers;
static std::list<OnMouseMove> mousemove_handlers;
static std::list<OnCollisionHandler> collisionHandlers;

void HApplication::loadScene(Scene & newScene)
{
	if (cur_scene)
	{
		cur_scene->endScene();
		delete cur_scene;
	}

	cur_scene = new Scene;

	*cur_scene = newScene;

	if (!cur_scene->isRunning) cur_scene->startScene(SDL_GetWindowSurface(mainWindow));
	newScene.startScene(SDL_GetWindowSurface(mainWindow));
}

Scene_ptr HApplication::getActiveScene()
{
	return cur_scene;
}

void HApplication::setSize(int width, int height)
{
	if (mainWindow)
	{
		SDL_SetWindowSize(mainWindow, width, height);
	}
}

Rect HApplication::getSize()
{
	int width = 0;
	int height = 0;

	if (mainWindow)
	{
		SDL_GetWindowSize(mainWindow, &width, &height);
	}

	return {width, height,0};
}

Window HApplication::getWindowHandle()
{
	return mainWindow;
}

void HApplication::Initialize(std::string project_name)
{
	// Initialize and load stuffs.
	SDL_Init(SDL_INIT_EVERYTHING);
	IMG_Init(IMG_INIT_PNG);
	TTF_Init();

	mainWindow = SDL_CreateWindow(project_name.c_str(), 100, 100, 600, 640, SDL_WINDOW_SHOWN | SDL_WINDOW_OPENGL);

	if (!mainWindow)
	{
		SDL_ShowSimpleMessageBox(SDL_MESSAGEBOX_ERROR, "Error", "Game window creation failed!", NULL);
		Exit();
		return;
	}
}

void HApplication::start()
{
	while (true)
	{
		if (cur_scene)
		{
			if (cur_scene->isRunning)
			{
				cur_scene->updateScene();
			}
			else
			{
				delete cur_scene;
				cur_scene = NULL;
			}
		}
	}
}

Vector2 HApplication::getMousePosition()
{
	Vector2 vec;

	SDL_GetMouseState(&vec.x, &vec.y);

	return vec;
}

Canvas HApplication::getCanvas()
{
	return SDL_GetWindowSurface(mainWindow);
} 

void HApplication::Exit()
{
	SDL_DestroyWindow(mainWindow);
	exit(0x1);
}

void HApplication::registerCollisionHandler(OnCollisionHandler handler)
{
	collisionHandlers.push_back(handler);
}

void HApplication::registerKeyboardHandler(OnKeyDown handler)
{
	keydown_handlers.push_back(handler);
}

void HApplication::registerMouseDownHandler(OnMouseDown handler)
{
	mousedown_handlers.push_back(handler); 
}

void HApplication::registerMouseMoveHandler(OnMouseMove handler)
{
	mousemove_handlers.push_back(handler);
}

std::list<OnCollisionHandler> HApplication::getCollisionHandlers()
{
	return collisionHandlers;
}

std::list<OnKeyDown> HApplication::getKeyboardHandlers()
{
	return keydown_handlers;
}

std::list<OnMouseDown> HApplication::getMouseDownHandlers()
{
	return mousedown_handlers;
}

std::list<OnMouseMove> HApplication::getMouseMoveHandlers()
{
	return mousemove_handlers;
}