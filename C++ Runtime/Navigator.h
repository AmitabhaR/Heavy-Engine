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
	float slope = 0;
	float deltaX = 0, deltaY = 0;
	int pos_x = 0, pos_y = 0;
 	int update_counter = 0, total_updates = 0;

	void makePath(Vector2, Vector2);

public:
	Navigator(GameObject_Scene * , int );

	void addPoint(Vector2 );

	void deletePoint(int );

	void start();

    bool isNavigating();

	void stop();

	void update();

	void cameraUpdatePoints(Vector2);
};

typedef Navigator * Navigator_ptr;

#endif