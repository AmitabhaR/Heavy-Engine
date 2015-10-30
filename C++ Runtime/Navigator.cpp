#include "Navigator.h"
#include<math.h>

using namespace std;

static Vector2 getPointElementAt(list<Vector2> & point_list,int index)
{
	register int counter = 0; 

	for (register std::list<Vector2>::iterator cur_point = point_list.begin(); cur_point != point_list.end(); cur_point++,counter++)
		if (index == counter) return *cur_point;
}

static void setPointElementAt(list<Vector2> & point_list, Vector2 & value , int index)
{
	register int counter = 0;

	for (register std::list<Vector2>::iterator cur_point = point_list.begin(); cur_point != point_list.end(); cur_point++, counter++)
		if (index == counter) { *cur_point = value; break; }
}


Navigator::Navigator(GameObject_Scene * baseObject, int navigation_speed)
{
	this->baseObject = baseObject;
	this->navigation_speed = navigation_speed;
}

void Navigator::addPoint(Vector2 point)
{
	if (!isRunning) nav_points.push_back(point);
}

void Navigator::deletePoint(int count)
{
	if (count > -1 && count < nav_points.size( ))
	{
		int cntr = 0;

		for (std::list<Vector2>::iterator cur_point = nav_points.begin(); cur_point != nav_points.end(); cur_point++,cntr++)
		{
			if (cntr == count)
			{
				this->nav_points.erase(cur_point);
			}
		}
	}
}

void Navigator::start()
{
	if (!isRunning)
	{
		isRunning = true;
		current_frame = 0;

		if (current_frame < nav_points.size() && current_frame + 1 < nav_points.size())
		{
			makePath(getPointElementAt(nav_points, current_frame), getPointElementAt(nav_points, current_frame + 1));
			baseObject->pos_x = pos_x;
			baseObject->pos_y = pos_y;
		}
	}
}

bool Navigator::isNavigating()
{
	return isRunning;
}

void Navigator::stop()
{
	isRunning = false;
}

void Navigator::cameraUpdatePoints(Vector2 pos)
{
	for (int cntr = 0; cntr < nav_points.size(); cntr++)
	{
		Vector2 nav_point = getPointElementAt(nav_points,cntr);

		nav_point.x -= pos.x;
		nav_point.y -= pos.y;

		setPointElementAt(nav_points,nav_point,cntr);
	}
}

void Navigator::makePath(Vector2 begin_point, Vector2 end_point)
{
	deltaY = (end_point.y - begin_point.y);
	deltaX = (end_point.x - begin_point.x);

	if (abs(deltaX) >= abs(deltaY)) slope = deltaY / deltaX;
	else slope = deltaX / deltaY;

	total_updates = (int)(((float)sqrt((double)pow(abs(deltaX), 2) + pow(abs(deltaY), 2))) / navigation_speed);
	update_counter = 0;

	pos_x = begin_point.x;
	pos_y = begin_point.y;
}

void Navigator::update()
{
	if (current_frame < nav_points.size( ))
		if (this->baseObject->obj_instance.img.baseImage != NULL)
			if (update_counter == total_updates)
			{
				current_frame++;

				if (current_frame < nav_points.size() && current_frame + 1 < nav_points.size()) makePath(getPointElementAt(nav_points, current_frame), getPointElementAt(nav_points, current_frame + 1));
				else stop();
			}
			else
			{
				if (abs(deltaX) >= abs(deltaY))
				{
					baseObject->pos_x = pos_x;
					baseObject->pos_y = (int)(slope * (pos_x - getPointElementAt(nav_points, current_frame).x) + getPointElementAt(nav_points, current_frame).y);
					pos_x += navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
				}
				else
				{
					baseObject->pos_y = pos_y;
					baseObject->pos_x = (int)(slope * (pos_y - getPointElementAt(nav_points, current_frame).y) + getPointElementAt(nav_points, current_frame).x);
					pos_y += navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
				}

				update_counter++;
			}
		else;
	else stop();
}