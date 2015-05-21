#include "Navigator.h"
#include<math.h>

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

		if (current_frame < nav_points.size( ))
		{
			int cntr = 0;
			Vector2 point;

			for (std::list<Vector2>::iterator cur_point = nav_points.begin(); cur_point != nav_points.end(); cur_point++, cntr++)
			{
				if (cntr == current_frame)
				{
					point = *cur_point;
					break;
				}
			}

			if (point.x - baseObject->pos_x == 0)
			{
				isVertical = true;
			}
			else
			{
				deltaX = point.x - baseObject->pos_x;
				deltaY = point.y - baseObject->pos_y;
				delta_error = abs(point.y - baseObject->pos_y / point.x - baseObject->pos_x);
				isVertical = false;
				error = 0;
			}
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

void Navigator::update()
{
	if (current_frame < nav_points.size( ))
	{
		if (this->baseObject->obj_instance.img.baseImage != NULL)
		{
			int cntr = 0;
			Vector2 point;

			for (std::list<Vector2>::iterator cur_point = nav_points.begin(); cur_point != nav_points.end(); cur_point++, cntr++)
			{
				if (cntr == current_frame)
				{
					point = *cur_point;
					break;
				}
			}

			if (baseObject->pos_x + (baseObject->obj_instance.img.Width / 2) > point.x && baseObject->pos_x < point.x + 10 && baseObject->pos_y + (baseObject->obj_instance.img.Height / 2) > point.y && baseObject->pos_y < point.y + 10)
			{
				current_frame++;

				if (current_frame < nav_points.size( ))
				{
					if (point.x - baseObject->pos_x == 0)
					{
						isVertical = true;
					}
					else
					{
						deltaX = point.x - baseObject->pos_x;
						deltaY = point.y - baseObject->pos_y;
						delta_error = abs(point.y - baseObject->pos_y / point.x - baseObject->pos_x);
						isVertical = false;
						error = 0;
					}
				}
			}
			else
			{
				if (!isVertical)
				{
					if (error >= 0.5f)
					{
						baseObject->pos_y += (point.y < baseObject->pos_y) ? -navigation_speed : navigation_speed;
						error /= (deltaX + navigation_speed * deltaY * deltaX + deltaY + navigation_speed);
					}
					else
					{
						baseObject->pos_x += (point.x < baseObject->pos_x) ? -navigation_speed : navigation_speed;
						error += delta_error;
					}
				}
				else
				{
					baseObject->pos_y += (point.y < baseObject->pos_y) ? -navigation_speed : navigation_speed;
				}
			}
		}
	}
	else
	{
		stop();
	}
}