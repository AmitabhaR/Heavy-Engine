#ifndef NAVIGATOR_H

#define NAVIGATOR_H

#include<list>
#include "Vector2.h"
#include "GameObject_Scene.h"

class Navigator
{
private:
	bool isRunning = false;
	std::list<Vector2> nav_points;
	int current_frame = 0;
	GameObject_Scene * baseObject;
	int navigation_speed = 0;
	float delta_error = 0;
	bool isVertical = false;
    float error = 0;
	float deltaX = 0;
	float deltaY = 0;

public:
	Navigator(GameObject_Scene * , int );

	void addPoint(Vector2 );

	void deletePoint(int );

	void start();

    bool isNavigating();

	void stop();

	void update();
};

typedef Navigator * Navigator_ptr;

#endif