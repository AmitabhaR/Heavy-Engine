#ifndef CAMERA_H

#define CAMERA_H

#include "Vector2.h"

class Camera
{
public:
	static void TranslateCamera(Vector2 );

	static void RotateCamera(float );

	static Vector2 getCameraPosition();
	
	static float getCameraRotation();
};

#endif